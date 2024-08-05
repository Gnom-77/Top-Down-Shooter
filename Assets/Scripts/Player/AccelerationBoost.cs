using UnityEngine;

public class AccelerationBoost : MonoBehaviour
{
    [SerializeField] private float _accelerationMagnitude;
    [SerializeField] private float _boostTime;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _speedBonus;

    private float _oldVelocity;
    private float _timer;
    private bool _isBoosting;

    private void Start()
    {
        _speedBonus.SetActive(false);
        _isBoosting = false;
        _oldVelocity = _playerMovement.Velocity;
    }

    private void Update()
    {
        if (_isBoosting)
        {
            StartBonus();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MoveBoost"))
        {
            _timer = _boostTime;
            _isBoosting = true;
            _playerMovement.Velocity = _oldVelocity * _accelerationMagnitude;
            Destroy(collision.gameObject);
            _speedBonus.SetActive(true);
        }
    }

    private void StartBonus()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _playerMovement.Velocity = _oldVelocity;
            _isBoosting = false;
            _speedBonus.SetActive(false);
        }
    }
}
