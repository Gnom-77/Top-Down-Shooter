using TMPro;
using UnityEngine;

public class RecordInMainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _record;
    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        _record.text = ("Рекорд:\n") + highScore.ToString();
    }

}
