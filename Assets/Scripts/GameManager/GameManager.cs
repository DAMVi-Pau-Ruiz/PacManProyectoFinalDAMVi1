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
    private bool modoDiabloActivo = false;
    private Coroutine modoDiabloCoroutine;
    public GameObject currentPacman;
    private Coroutine invisGhostsCoroutine;
    private int scoreActual;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        vidasActuales = vidasIniciales;
        LivesController.instance.UpdateLives(vidasActuales);
        Debug.Log("Dificultad actual: " + instance.currentDifficulty);
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
    }

    public void ModoDiabloActivado(float tiempo)
    {
        if (modoDiabloCoroutine != null)
            StopCoroutine(modoDiabloCoroutine);

        modoDiabloCoroutine = StartCoroutine(ModoDiabloRutina(tiempo));
    }

    private IEnumerator ModoDiabloRutina(float tiempo)
    {
        modoDiabloActivo = true;
        yield return new WaitForSeconds(tiempo);
        modoDiabloActivo = false;
    }

    public bool IsModoDiabloActivo()
    {
        return modoDiabloActivo;
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

    public void ResetScore()
    {
        scoreActual = 0;
    }

    public int getScoreActual()
    {
        return scoreActual;
    }

    /* MAQUINA ESTADOS DIFICULTADES */
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }

    public Difficulty currentDifficulty;
    public void SetEasy()
    {
        currentDifficulty = Difficulty.Easy;
    }

    public void SetNormal()
    {
        currentDifficulty = Difficulty.Normal;
    }

    public void SetHard()
    {
        currentDifficulty = Difficulty.Hard;
    }

    /*VALOR VELOCIDAD FANTASMAS DEPENDIENDO DE DIFICULTAD*/

    public float GetGhostSpeed()
    {
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                return 1.5f;

            case Difficulty.Normal:
                return 2f;

            case Difficulty.Hard:
                return 2.5f;
        }

        return 3f;
    }

    /*VALOR VELOCIDAD PACMAN DEPENDIENDO DE DIFICULTAD*/
    public float GetPacmanSpeed()
    {
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                return 3f;

            case Difficulty.Normal:
                return 3f;

            case Difficulty.Hard:
                return 2f;
        }

        return 5f;
    }

}