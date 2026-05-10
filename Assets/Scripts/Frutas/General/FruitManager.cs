using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] frutas;

    [SerializeField]
    Transform fruitsSpawn;

    [SerializeField]
    float timeBetweenFruits = 10;

    [SerializeField]
    public AudioClip fruitEaten;

    private float currentTimeBetweenFruits;
    private int fruitIndex;
    private bool fruitSpawned = false;

    private void Update()
    {
        if (!fruitSpawned)
        {
            currentTimeBetweenFruits += Time.deltaTime;
            fruitIndex = GenerateRandomFruit();
        }
        if (currentTimeBetweenFruits >= timeBetweenFruits && !fruitSpawned)
        {
            fruitSpawned = true;
            Instantiate(frutas[fruitIndex], fruitsSpawn.transform.position, Quaternion.identity);
            currentTimeBetweenFruits = 0;
        }
    }

    private int GenerateRandomFruit()
    {
        int index = Random.Range(0, frutas.Length);
        return index;
    }

    public void FrutaComida()
    {
        AudioManager.Instance.PlaySFX(fruitEaten);
        fruitSpawned = false;
    }
}
