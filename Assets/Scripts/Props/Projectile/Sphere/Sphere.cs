using UnityEngine;

public class Sphere : Projectile
{
    [SerializeField] private int _damage;

    public int Damage => _damage;

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}