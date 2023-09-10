using System.Collections.Generic;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private XP _xpPrefab;
    [SerializeField] private float _minPositionOffset;
    [SerializeField] private float _maxPositionOffset;

    private List<XP> _xps = new List<XP>();
    private Vector3 _offset;
    private XP _xp;

    public void SpawnXPS(Enemy enemy)
    {
        for (int i = 0; i < enemy.XPCount; i++)
        {
            _offset.x = Random.Range(_minPositionOffset, _maxPositionOffset);
            _offset.z = Random.Range(_minPositionOffset, _maxPositionOffset);

            _xp = Instantiate(_xpPrefab, enemy.gameObject.transform.position + _offset, Quaternion.identity, transform);
            
            _enemyManager.AllEnemiesDied += _xp.MoveToPlayer;

            _xp.Init(_player.transform);
            _xps.Add(_xp);
        }

        enemy.GetComponent<EnemyHealth>().Dying -= SpawnXPS;
    }
}
