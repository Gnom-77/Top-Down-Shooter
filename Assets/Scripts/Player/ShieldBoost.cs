using UnityEngine;

public class ShieldBoost : MonoBehaviour
{
    [SerializeField] private float _boostTime;
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject _lossGameWindow;

    private bool _isBoosting;
    private float _timer;

    private void Start()
    {
        _lossGameWindow.SetActive(false);
        _isBoosting = false;
        _shield.SetActive(false);
    }

    private void Update()
    {
        if (_isBoosting)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _isBoosting = false;
                _shield.SetActive(false);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ShieldBoost"))
        {
            _timer = _boostTime;
            _isBoosting = true;
            _shield.SetActive(true);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(!_isBoosting)
            {
                Time.timeScale = 0f;
                _lossGameWindow.SetActive(true);
            }
        }
    }
}
