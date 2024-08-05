using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManeger : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }
}
