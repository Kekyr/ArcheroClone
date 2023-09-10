using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private Player _player;

    private List<Enemy> _enemies = new List<Enemy>();

    private Enemy _nearestEnemy;

    public Enemy NearestEnemy => _nearestEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.GetComponent<EnemyHealth>().Dying += Remove;
            _enemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Remove(enemy);
        }
    }

    private void Update()
    {
        if (_enemies.Count > 0)
            FindNearest();
    }

    private void FindNearest()
    {
        float _enemyDistance;
        float minDistance = 100;
        Enemy _minDistanceEnemy = null;

        foreach (Enemy enemy in _enemies)
        {
            _enemyDistance = (enemy.gameObject.transform.position - _player.transform.position).magnitude;

            if (_enemyDistance < minDistance)
            {
                minDistance = _enemyDistance;
                _minDistanceEnemy = enemy;
            }
        }

        if (_nearestEnemy != _minDistanceEnemy)
        {
            _nearestEnemy?.ChangeMark();
            _minDistanceEnemy.ChangeMark();
            _nearestEnemy = _minDistanceEnemy;
        }
    }

    private void Remove(Enemy enemy)
    {
        if (enemy == _nearestEnemy)
        {
            _nearestEnemy.ChangeMark();
            _nearestEnemy = null;
        }

        enemy.GetComponent<EnemyHealth>().Dying -= Remove;
        _enemies.Remove(enemy);
    }
}