using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _offsetZ;
    [SerializeField] private float _speed;

    private Vector3 _newPosition;

    private void FixedUpdate()
    {
        _newPosition = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z - _offsetZ);
        transform.position = Vector3.MoveTowards(transform.position, _newPosition, _speed * Time.fixedDeltaTime);
    }
}