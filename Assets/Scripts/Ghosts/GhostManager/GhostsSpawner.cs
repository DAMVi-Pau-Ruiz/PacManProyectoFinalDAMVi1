using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostsSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject blinky;

    [SerializeField]
    GameObject pinky;

    [SerializeField]
    GameObject inky;

    [SerializeField]
    GameObject clyde;

    [SerializeField]
    GameObject fBlinky;

    [SerializeField]
    GameObject fPinky;

    [SerializeField]
    GameObject fInky;

    [SerializeField]
    GameObject fClyde;

    [SerializeField]
    Transform ghostsSpawner;

    [SerializeField]
    float timeBetweenGhosts = 10;

    [SerializeField]
    float respawnTime = 5f;  // Tiempo de reaparición de los fantasmas (en segundos)

    [SerializeField]
    public AudioClip ghostEaten;

    private float currentTimeBetweenGhosts;
    private bool blinkySpawned = false;
    private bool pinkySpawned = false;
    private bool inkySpawned = false;
    private bool clydeSpawned = false;

    private bool blinkyEaten = false;
    private bool pinkyEaten = false;
    private bool inkyEaten = false;
    private bool clydeEaten = false;

    // Flags para evitar respawn múltiple
    private bool isBlinkyRespawning = false;
    private bool isPinkyRespawning = false;
    private bool isInkyRespawning = false;
    private bool isClydeRespawning = false;

    private void Update()
    {
        currentTimeBetweenGhosts += Time.deltaTime;

        // Condiciones de aparición inicial de los fantasmas
        if (!blinkySpawned && !blinkyEaten && currentTimeBetweenGhosts >= timeBetweenGhosts * 0)
        {
            Spawn(blinky);
            blinkySpawned = true;
            Destroy(fBlinky);
        }
        else if (!pinkySpawned && !pinkyEaten && currentTimeBetweenGhosts >= timeBetweenGhosts * 1)
        {
            Spawn(pinky);
            pinkySpawned = true;
            Destroy(fPinky);
        }
        else if (!inkySpawned && !inkyEaten && currentTimeBetweenGhosts >= timeBetweenGhosts * 2)
        {
            Spawn(inky);
            inkySpawned = true;
            Destroy(fInky);
        }
        else if (!clydeSpawned && !clydeEaten && currentTimeBetweenGhosts >= timeBetweenGhosts * 3)
        {
            Spawn(clyde);
            clydeSpawned = true;
            Destroy(fClyde);
        }

        // Reaparición de los fantasmas después de ser comidos
        if (blinkyEaten && !blinkySpawned && !isBlinkyRespawning)
        {
            Debug.Log("Blinky ha sido comido, iniciando respawn...");
            StartCoroutine(RespawnGhost(blinky, 0));  // Reaparece Blinky
            isBlinkyRespawning = true;  // Marcar que estamos en respawn
        }
        if (pinkyEaten && !pinkySpawned && !isPinkyRespawning)
        {
            Debug.Log("Pinky ha sido comido, iniciando respawn...");
            StartCoroutine(RespawnGhost(pinky, 1));  // Reaparece Pinky
            isPinkyRespawning = true;  // Marcar que estamos en respawn
        }
        if (inkyEaten && !inkySpawned && !isInkyRespawning)
        {
            Debug.Log("Inky ha sido comido, iniciando respawn...");
            StartCoroutine(RespawnGhost(inky, 2));  // Reaparece Inky
            isInkyRespawning = true;  // Marcar que estamos en respawn
        }
        if (clydeEaten && !clydeSpawned && !isClydeRespawning)
        {
            Debug.Log("Clyde ha sido comido, iniciando respawn...");
            StartCoroutine(RespawnGhost(clyde, 3));  // Reaparece Clyde
            isClydeRespawning = true;  // Marcar que estamos en respawn
        }
    }

    private void Spawn(GameObject ghostPrefab)
    {
        Instantiate(ghostPrefab, ghostsSpawner.transform.position, Quaternion.identity);
    }

    public void MarkGhostAsEaten(string ghostName)
    {
        Debug.Log($"Fantasma {ghostName} marcado como comido");
        switch (ghostName)
        {
            case "Blinky(Clone)":
                AudioManager.Instance.PlaySFX(ghostEaten);
                blinkyEaten = true;
                blinkySpawned = false;
                break;
            case "Pinky(Clone)":
                AudioManager.Instance.PlaySFX(ghostEaten);
                pinkyEaten = true;
                pinkySpawned = false;
                break;
            case "Inky(Clone)":
                AudioManager.Instance.PlaySFX(ghostEaten);
                inkyEaten = true;
                inkySpawned = false;
                break;
            case "Clyde(Clone)":
                AudioManager.Instance.PlaySFX(ghostEaten);
                clydeEaten = true;
                clydeSpawned = false;
                break;
        }
    }

    private IEnumerator RespawnGhost(GameObject ghostPrefab, int index)
    {
        Debug.Log($"Reapareciendo el fantasma {ghostPrefab.name} en {index}...");
        yield return new WaitForSeconds(respawnTime);

        // Reaparecer al fantasma
        switch (index)
        {
            case 0:
                Debug.Log("Blinky reapareció");
                Spawn(blinky);
                blinkyEaten = false;
                blinkySpawned = true;
                isBlinkyRespawning = false;  // Liberar el flag de respawn
                break;
            case 1:
                Debug.Log("Pinky reapareció");
                Spawn(pinky);
                pinkyEaten = false;
                pinkySpawned = true;
                isPinkyRespawning = false;  // Liberar el flag de respawn
                break;
            case 2:
                Debug.Log("Inky reapareció");
                Spawn(inky);
                inkyEaten = false;
                inkySpawned = true;
                isInkyRespawning = false;  // Liberar el flag de respawn
                break;
            case 3:
                Debug.Log("Clyde reapareció");
                Spawn(clyde);
                clydeEaten = false;
                clydeSpawned = true;
                isClydeRespawning = false;  // Liberar el flag de respawn
                break;
        }
    }
}