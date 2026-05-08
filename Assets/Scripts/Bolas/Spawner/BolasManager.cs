using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolasManager : MonoBehaviour
{
    public static BolasManager Instance;

    private int pelletsRemaining = 0;



    private void Awake()
    {
        Instance = this;
    }

    public void RegisterPellet()
    {
        pelletsRemaining++;
    }

    public void PelletEaten()
    {
        pelletsRemaining--;

        if (pelletsRemaining <= 0)
        {
            RespawnAllPellets();
            GameManager.instance.addLevelCompleted();
        }

    }

    void RespawnAllPellets()
    {
        // Busca el spawner y vuelve a generar las bolas
        FindObjectOfType<BolasSpawner>().Respawn();
    }

    public void ResetCounter()
    {
        pelletsRemaining = 0;
    }
}
