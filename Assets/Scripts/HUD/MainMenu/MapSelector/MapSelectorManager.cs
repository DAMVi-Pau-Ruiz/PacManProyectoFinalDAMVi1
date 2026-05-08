using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectorManager : MonoBehaviour
{

    public AudioClip buttonClick;

    public void PlayLevel1()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        SelectMapAndGoLogin("Level1");
    }

    public void PlayLevel2()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        SelectMapAndGoLogin("Level2");
    }

    public void PlayLevel3()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        SelectMapAndGoLogin("Level3");
    }

    public void PlayLevel4()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        SelectMapAndGoLogin("Level4");
    }

    void SelectMapAndGoLogin(string levelName)
    {
        // Guardas el mapa elegido
        PlayerPrefs.SetString("SelectedMap", levelName);

        // (Opcional) guardas dificultad si la tienes aquí
        // PlayerPrefs.SetInt("Difficulty", difficultyValue);

        // Vas al login
        SceneManager.LoadScene("LoginUser");
    }
}