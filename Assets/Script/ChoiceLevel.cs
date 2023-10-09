using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceLevel : MonoBehaviour
{
    public string levelToLoad1;
    public string levelToLoad2;
    public string levelToLoad3;
    public string transition;
    public string level = "Level1";
    public static ChoiceLevel instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Attention, instance > 1 !");
            return;
        }
        instance = this;
    }
    public void Easy()
    {
        SceneManager.LoadScene(transition);
        level = levelToLoad1;
    }
    public void Normale()
    {
        SceneManager.LoadScene(transition);
        level = levelToLoad2;
    }

    public void Hard()
    {
        SceneManager.LoadScene(transition);
        level = levelToLoad3;
    }
}

