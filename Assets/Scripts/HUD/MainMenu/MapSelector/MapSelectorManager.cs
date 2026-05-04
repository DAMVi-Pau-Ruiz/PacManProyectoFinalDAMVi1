using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectorManager : MonoBehaviour
{
    public void PlayLevel1()
    {
        SelectMapAndGoLogin("Level1");
    }

    public void PlayLevel2()
    {
        SelectMapAndGoLogin("Level2");
    }

    public void PlayLevel3()
    {
        SelectMapAndGoLogin("Level3");
    }

    public void PlayLevel4()
    {
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