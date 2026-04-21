using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostsController : MonoBehaviour
{
    [SerializeField]
    int puntos = 200;
 
    public int GetPuntos()
    {
        return puntos;
    }
}
