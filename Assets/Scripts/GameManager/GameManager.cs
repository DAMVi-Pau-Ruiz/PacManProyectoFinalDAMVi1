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

    public enum Difficulty { Easy, Normal, Hard }
    public Difficulty currentDifficulty = Difficulty.Normal;

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
}