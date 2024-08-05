using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Transform PlayerTransform { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Метод для установки ссылки на игрока
    public void RegisterPlayer(Transform playerTransform)
    {
        PlayerTransform = playerTransform;
    }


}
