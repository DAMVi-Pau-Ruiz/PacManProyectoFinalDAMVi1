using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostsSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject blinky;

    [SerializeField]
    GameObject pinky;

    [SerializeField]
    GameObject inky;

    //[SerializeField]
    //GameObject clyde;

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

    private float currentTimeBetweenGhosts;
    private bool blinkySpawned = false;
    private bool pinkySpawned = false;
    private bool inkySpawned = false;
    //private bool clydeSpawned = false;

    private void Update()
    {
        currentTimeBetweenGhosts += Time.deltaTime;

        if (!blinkySpawned && currentTimeBetweenGhosts >= timeBetweenGhosts * 0)
        {
            Spawn(blinky);
            blinkySpawned = true;
            Destroy(fBlinky);
        }
        else if (!pinkySpawned && currentTimeBetweenGhosts >= timeBetweenGhosts * 1)
        {
            Spawn(pinky);
            pinkySpawned = true;
            Destroy(fPinky);
        }
        else if (!inkySpawned && currentTimeBetweenGhosts >= timeBetweenGhosts * 2)
        {
            Spawn(inky);
            inkySpawned = true;
            Destroy(fInky);
        }
        //else if (!clydeSpawned && currentTimeBetweenGhosts >= timeBetweenGhosts * 3)
        //{
        //    Spawn(clyde);
        //    clydeSpawned = true;
        //    Destroy(fClyde);
        //}
    }
    private void Spawn(GameObject ghostPrefab)
    {
        Instantiate(ghostPrefab, ghostsSpawner.transform.position, Quaternion.identity);
    }
}
