using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject pacmanPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField] int vidasIniciales = 3;

    private int vidasActuales;
    private Coroutine modoDiabloCoroutine;
    public GameObject currentPacman;
    private Coroutine invisGhostsCoroutine;
    private int scoreActual;
    private PacMan_Controller pacman;

    public enum Difficulty { Easy, Normal, Hard }
    public Difficulty currentDifficulty = Difficulty.Normal;


    public bool cerezaComida, fresaComida, platanoComido, manzanaComida, sandiaComida, atupComida, fresaInvComida;

    private MongoDBManager mongo;

    // --- ESTADÍSTICAS ---
    public float duracion;
    public int bolasComidas;
    public int fantasmasComidos;
    public int nivelesCompletados;

    private float tiempoInicio;

    public PlayerDataSaver saver;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        instance = this;

        if (PlayerPrefs.HasKey("difficulty"))
        {
            currentDifficulty = (Difficulty)PlayerPrefs.GetInt("difficulty");
        }

        AplicarDificultad();

        saver = GetComponent<PlayerDataSaver>();
    }

    private void Start()
    {
        vidasActuales = vidasIniciales;
        LivesController.instance.UpdateLives(vidasActuales);

        pacman = FindObjectOfType<PacMan_Controller>();

        /*FRUTAS BLOQUEADAS AL EMPEZAR JUEGO*/

        cerezaComida = false;
        fresaComida = false;
        platanoComido = false;
        manzanaComida = false;
        sandiaComida = false;
        atupComida = false;
        fresaInvComida = false;

        if (mongo == null)
        {
            mongo = FindObjectOfType<MongoDBManager>();
        }

        tiempoInicio = Time.time;

    }

    public void PacmanDied()
    {
        vidasActuales--;
        LivesController.instance.UpdateLives(vidasActuales);

        if (vidasActuales > 0)
        {
            StartCoroutine(RespawnPacman());
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private IEnumerator RespawnPacman()
    {
        yield return new WaitForSeconds(1f);

        GameObject newPacman = Instantiate(pacmanPrefab, spawnPoint.position, Quaternion.identity);

        pacman = newPacman.GetComponent<PacMan_Controller>();
    }

    public void ModoDiabloActivado(float tiempo)
    {
        if (modoDiabloCoroutine != null)
            StopCoroutine(modoDiabloCoroutine);

        modoDiabloCoroutine = StartCoroutine(ModoDiabloRutina(tiempo));
    }

    private IEnumerator ModoDiabloRutina(float tiempo)
    {
        pacman.state = PacMan_Controller.PacManState.DIABLO;
        yield return new WaitForSeconds(tiempo);
        pacman.state = PacMan_Controller.PacManState.NORMAL;
    }

    public Transform GetPacman()
    {
        return currentPacman != null ? currentPacman.transform : null;
    }

    public void AddVida()
    {
        if (!(vidasActuales + 1 > 10))
        {
            vidasActuales++;
            LivesController.instance.UpdateLives(vidasActuales);
        }
    }

    public void ActivarInvisibilidadFantasmas(float duracion)
    {
        if (invisGhostsCoroutine != null)
            StopCoroutine(invisGhostsCoroutine);

        invisGhostsCoroutine = StartCoroutine(InvisibilidadFantasmasRutina(duracion));
    }

    private IEnumerator InvisibilidadFantasmasRutina(float duracion)
    {
        GhostsController[] ghosts = FindObjectsOfType<GhostsController>();

        foreach (var g in ghosts)
            g.SetInvisible(true);

        yield return new WaitForSeconds(duracion);

        ghosts = FindObjectsOfType<GhostsController>();
        foreach (var g in ghosts)
            g.SetInvisible(false);
    }

    public void AddScore(int puntos)
    {
        scoreActual += puntos;
    }

    public int getScoreActual()
    {
        return scoreActual;
    }

    public void ResetScore()
    {
        scoreActual = 0;
    }

    private void AplicarDificultad()
    {
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                vidasIniciales = 3;
                break;
            case Difficulty.Normal:
                vidasIniciales = 2;
                break;
            case Difficulty.Hard:
                vidasIniciales = 1;
                break;
        }
    }

    public PacMan_Controller GetPacmanScript()
    {
        return pacman;
    }

    private void Update()
    {
        duracion = Time.time - tiempoInicio;
    }

    public async Task GuardarSesion()
    {
        string player = PlayerPrefs.GetString("username", "AAA");

        if (mongo != null)
        {
            await mongo.SaveSession(player, duracion, bolasComidas, fantasmasComidos, nivelesCompletados);
        }
        else
        {
            Debug.LogError("MongoDBManager no encontrado en la escena");
        }
    }

    public void addPelletEaten()
    {
        bolasComidas++;
    }

    public void addGhostEaten()
    {
        fantasmasComidos++;
    }

    public void addLevelCompleted()
    {
        nivelesCompletados++;
    }

    void EndGame(int puntuacion)
    {
        string nombre = PlayerPrefs.GetString("username", "AAA");

        saver.GuardarDatos(nombre, puntuacion);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameOver")
        {
            Debug.Log("Entramos en GameOver, buscando saver por tag...");

            GameObject saverObj = GameObject.FindGameObjectWithTag("Saver");

            if (saverObj != null)
            {
                PlayerDataSaver saver = saverObj.GetComponent<PlayerDataSaver>();

                if (saver != null)
                {
                    string nombre = PlayerPrefs.GetString("username", "AAA");
                    int puntuacion = getScoreActual();

                    saver.GuardarDatos(nombre, puntuacion);

                    Debug.Log("Datos guardados correctamente desde GameManager.");
                }
                else
                {
                    Debug.LogError("El objeto con tag 'Saver' NO tiene PlayerDataSaver.");
                }
            }
            else
            {
                Debug.LogError("No se encontró ningún objeto con tag 'Saver' en GameOver.");
            }
        }
    }

}
