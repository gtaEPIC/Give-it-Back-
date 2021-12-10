using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    // Load the game scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    // Exit the game
    public void ExitGame()
    {
        Application.Quit();
    }
}