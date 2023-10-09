using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public GameObject Settings;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void SettingsButton()
    {
        Settings.SetActive(true);
    }

    public void CloseSettings()
    {
        Settings.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
