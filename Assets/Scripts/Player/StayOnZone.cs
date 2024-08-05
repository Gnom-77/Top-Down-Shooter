using UnityEngine;

public class StayOnZone : MonoBehaviour
{
    [SerializeField] float _slowingDown = 0.6f;
    [SerializeField] float _upSpeedValue;
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] private GameObject _lossGameWindow;
    [SerializeField] private GameObject _speedBonus;
    

    private float _defaultVelocity;
    private float _upVelocity;

    private void Start()
    {
        _defaultVelocity = _playerMovement.Velocity;
        _upVelocity = _defaultVelocity * _upSpeedValue;
        _lossGameWindow.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            Time.timeScale = 0f;
            _lossGameWindow.SetActive(true);
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlowZone"))
        {
            if(!_speedBonus.activeSelf)
            {
                _playerMovement.Velocity = _defaultVelocity * _slowingDown;
            }
            else
            {
                _playerMovement.Velocity = _upVelocity * _slowingDown;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlowZone"))
        {
            _playerMovement.Velocity = _defaultVelocity;
        }
    }
}
