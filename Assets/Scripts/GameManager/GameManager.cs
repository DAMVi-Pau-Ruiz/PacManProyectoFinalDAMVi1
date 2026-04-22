using UnityEngine;
using System.Collections;

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

    private void Awake()
    {
        instance = this;
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
            Debug.Log("Has perdido");
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
}