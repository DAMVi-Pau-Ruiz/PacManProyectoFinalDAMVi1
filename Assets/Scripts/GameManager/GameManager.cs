using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject pacmanPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField]
    int vidasIniciales = 3;

    private int vidasActuales;

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
        {
            StartCoroutine(RespawnPacman());
        }
        else
        {
            Debug.Log("Has perdido");
        }
    }

    public void KillPacman(GameObject pacman)
    {
        Destroy(pacman);
        StartCoroutine(RespawnPacman());
    }

    private IEnumerator RespawnPacman()
    {
        yield return new WaitForSeconds(1f); // tiempo antes de reaparecer
        Instantiate(pacmanPrefab, spawnPoint.position, Quaternion.identity);
    }
}
