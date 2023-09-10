using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public event UnityAction AllEnemiesDied;

    [SerializeField] private XPManager _xpManager;
    [SerializeField] private float _enemiesDiedDelay;
    [SerializeField] private Exit _exit;

    private List<Enemy> _enemies;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _enemies = new List<Enemy>();
        _enemies.AddRange(GetComponentsInChildren<Enemy>());

        foreach (Enemy enemy in _enemies)
        {
            enemy.GetComponent<EnemyHealth>().Dying += _xpManager.SpawnXPS;
            enemy.GetComponent<EnemyHealth>().Dying += RemoveEnemy;
        }
    }

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_enemiesDiedDelay);
    }

    private void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);

        enemy.GetComponent<EnemyHealth>().Dying -= RemoveEnemy;

        if (_enemies.Count == 0)
            StartCoroutine(EnemiesDied());
    }

    private IEnumerator EnemiesDied()
    {
        yield return _waitForSeconds;
        AllEnemiesDied?.Invoke();
        yield return _waitForSeconds;
        _exit.gameObject.SetActive(true);
    }
}
