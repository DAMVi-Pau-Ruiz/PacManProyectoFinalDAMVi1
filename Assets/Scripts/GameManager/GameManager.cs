using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        instance = this;

        if (PlayerPrefs.HasKey("difficulty"))
        {
            currentDifficulty = (Difficulty)PlayerPrefs.GetInt("difficulty");
        }

        AplicarDificultad();
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
    }

    public void PacmanDied()
    {
        vidasActuales--;
        LivesController.instance.UpdateLives(vidasActuales);

        if (vidasActuales > 0)
            StartCoroutine(RespawnPacman());
        else
            SceneManager.LoadScene("GameOver");
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

}
