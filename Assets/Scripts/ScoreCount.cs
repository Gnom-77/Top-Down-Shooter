using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    public static int Count;

    private void Start()
    {
        Count = 0;
    }

    private void Update()
    {
        _score.text = Count.ToString();
    }


}
