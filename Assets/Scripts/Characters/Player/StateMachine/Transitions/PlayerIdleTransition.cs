using UnityEngine;

public class PlayerIdleTransition : PlayerTransition
{
    [SerializeField] private EnemyDetector _enemyDetector;

    private void Update()
    {
        if (InputManager.Direction == Vector3.zero && _enemyDetector.NearestEnemy == null)
        {
            NeedTransit = true;
        }
    }
}