using UnityEngine;

public class PlayerAttackTransition : PlayerTransition
{
    [SerializeField] private EnemyDetector _enemyDetector;

    private void Update()
    {
        if (InputManager.Direction == Vector3.zero && _enemyDetector.NearestEnemy != null)
        {
            NeedTransit = true;
        }
    }
}