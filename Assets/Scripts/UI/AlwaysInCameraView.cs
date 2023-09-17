using UnityEngine;

public class AlwaysInCameraView : MonoBehaviour
{
    private Camera _camera;
    private float _rotationX = 48;
    private float _rotationZ = 0;
    private float _rotationY = 0;
    private Vector3 _direction;
    private Quaternion _rotationToCamera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _direction = _camera.transform.position - transform.position;
        _direction.x = 0;
        _rotationToCamera = Quaternion.Euler(_rotationX, _rotationY, _rotationZ);
        transform.rotation = _rotationToCamera;
    }
}