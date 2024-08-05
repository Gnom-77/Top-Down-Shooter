using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _velocity;
    [SerializeField] private float _rotationSpeed = 180f;
    [SerializeField] private Rigidbody2D _playerRb2D;
    [SerializeField] private Camera _camera;

    private Vector2 _direction;
    private Vector2 _mousePosition;
    private bool _isRotating = false;

    private void Start()
    {
        GameManager.Instance.RegisterPlayer(transform);
    }

    private void Update()
    {
        PlayerInput();
    }


    private void FixedUpdate()
    {
        Vector2 movement = _direction.normalized * _velocity;
        _playerRb2D.MovePosition(_playerRb2D.position + movement * Time.fixedDeltaTime);

        if (_isRotating)
        {
            PlayerRotation();
        }

    }


    private void PlayerInput()
    {
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");


        if (Input.GetButton("Fire1"))
        {
            _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _isRotating = true;
        }

    }

    private void PlayerRotation()
    {
        Vector2 lookDirection = _mousePosition - _playerRb2D.position;
        float targetAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        float currentAngle = _playerRb2D.rotation;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, _rotationSpeed * Time.fixedDeltaTime);
        _playerRb2D.rotation = newAngle;

        if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) < 0.1f)
        {
            _isRotating = false;
        }

    }

    public bool IsRotating 
    {
        get { return _isRotating; }
    }

    public float Velocity
    {
        get { return _velocity; }
        set { _velocity = value; }
    }

    //private void PlayerInput()
    //{
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //        _direction.x = -1;
    //    if (Input.GetKey(KeyCode.RightArrow))
    //        _direction.x = 1;
    //    if (Input.GetKey(KeyCode.UpArrow))
    //        _direction.y = 1;
    //    if (Input.GetKey(KeyCode.DownArrow))
    //        _direction.x = -1;
    //}


}
