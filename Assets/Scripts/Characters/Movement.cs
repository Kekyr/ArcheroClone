using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction, float speed)
    {
        _rigidbody.MovePosition(_rigidbody.position + (direction * speed * Time.fixedDeltaTime));
    }
}
