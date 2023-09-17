using UnityEngine;

[RequireComponent(typeof(ShurikenMovementData))]
public class Shuriken : Projectile
{
    private ShurikenMovementData _shurikenMovementData;

    private void Awake()
    {
        _shurikenMovementData = GetComponent<ShurikenMovementData>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _shurikenMovementData.ChangeReturnStatus();
    }
}