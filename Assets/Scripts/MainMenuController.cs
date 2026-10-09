using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour 
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Debug.Log("»грок вышел из игры!");
        Application.Quit();
    }
    
}
