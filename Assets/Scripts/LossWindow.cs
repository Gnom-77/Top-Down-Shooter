using UnityEngine;
using TMPro;

public class LossWindow : MonoBehaviour
{
    [SerializeField] private GameObject _textInGameProcess;
    [SerializeField] private GameObject _menuButtonInGameProcess;
    [SerializeField] private TMP_Text _textInLossWindow;



    private void OnEnable()
    {
        _textInGameProcess.SetActive(false);
        _menuButtonInGameProcess.SetActive(false);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (ScoreCount.Count > highScore)
        {
            _textInLossWindow.text = "Новый рекорд\n" + ScoreCount.Count.ToString();
            PlayerPrefs.SetInt("HighScore", ScoreCount.Count);
            PlayerPrefs.Save();

        }
        else
        {
            _textInLossWindow.text = ScoreCount.Count.ToString();
        }
    }

    private void OnDisable()
    {
        _textInGameProcess.SetActive(true);
        _menuButtonInGameProcess.SetActive(true);
    }
}
