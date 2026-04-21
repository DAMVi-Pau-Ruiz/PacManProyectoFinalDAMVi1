using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public float speed = 2f;
    public float duration = 20f; // tiempo total de créditos

    private float timer = 0f;

    void Update()
    {
        // mover créditos
        transform.position += Vector3.up * speed * Time.deltaTime;

        // temporizador
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            SceneManager.LoadScene("MainMenu");
            
        }
    }
}