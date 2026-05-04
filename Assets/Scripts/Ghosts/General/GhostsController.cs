using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostsController : MonoBehaviour
{
    [SerializeField]
    int puntos = 200;

    [SerializeField]
    protected Sprite scaredSprite;
 
    protected SpriteRenderer sr;
    private bool isInvisible = false;



    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public int GetPuntos()
    {
        return puntos;
    }

    public void SetInvisible(bool invisible)
    {
        isInvisible = invisible;
        sr.enabled = !invisible;
    }

    public bool IsInvisible()
    {
        return isInvisible;
    }

}
