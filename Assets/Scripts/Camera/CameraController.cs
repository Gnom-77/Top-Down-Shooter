using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _minBorder;
    [SerializeField] private Vector2 _maxBorder; 


    private float _cameraHalfWidth;
    private float _cameraHalfHeight;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
        _cameraHalfHeight = _camera.orthographicSize;
        _cameraHalfWidth = _camera.aspect * _cameraHalfHeight;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = _player.position;

        float clampedX = Mathf.Clamp(newPosition.x, _minBorder.x + _cameraHalfWidth, _maxBorder.x - _cameraHalfWidth);
        float clampedY = Mathf.Clamp(newPosition.y, _minBorder.y + _cameraHalfHeight, _maxBorder.y - _cameraHalfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
