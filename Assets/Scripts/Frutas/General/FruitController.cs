using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    [SerializeField]
    protected int puntos = 200;

    [SerializeField]
    protected float duration = 8;

    public int GetPuntos()
    {
        return puntos;
    }

    public float GetDuration()
    {
        return duration;
    }
}
