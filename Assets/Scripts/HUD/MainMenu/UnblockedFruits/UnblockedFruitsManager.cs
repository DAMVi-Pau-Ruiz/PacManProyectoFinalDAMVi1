using UnityEngine;

public class UnblockedFruitsManager : MonoBehaviour
{
    public GameObject color;
    public GameObject bn;

    public enum Tipo { Cereza, Fresa, Platano, Manzana, Sandia, Atup, FresaInv }
    public Tipo tipo;

    void Start()
    {
        bool activa = false;

        if (tipo == Tipo.Cereza) activa = GameManager.instance.cerezaComida;
        if (tipo == Tipo.Fresa) activa = GameManager.instance.fresaComida;
        if (tipo == Tipo.Platano) activa = GameManager.instance.platanoComido;
        if (tipo == Tipo.Manzana) activa = GameManager.instance.manzanaComida;
        if (tipo == Tipo.Sandia) activa = GameManager.instance.sandiaComida;
        if (tipo == Tipo.Atup) activa = GameManager.instance.atupComida;
        if (tipo == Tipo.FresaInv) activa = GameManager.instance.fresaInvComida;

        color.SetActive(activa);
        bn.SetActive(!activa);
    }
}