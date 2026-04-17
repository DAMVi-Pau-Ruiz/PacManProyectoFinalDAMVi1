using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject pacmanPrefab;
    [SerializeField] Transform spawnPoint;

    private void Awake()
    {
        instance = this;
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
