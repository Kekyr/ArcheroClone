using UnityEngine;

[CreateAssetMenu(fileName = "new PoolSO", menuName = "PoolSO/Create new PoolSO")]
public class PoolSO : ScriptableObject
{
    [SerializeField] private string _tag;
    [SerializeField] private Projectile _prefab;
    [SerializeField] private int _numberPerInstance;

    public string Tag => _tag;
    public Projectile Prefab => _prefab;
    public int NumberPerInstance => _numberPerInstance;
}