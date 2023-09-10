using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerStatsSO _playerStats;
    [SerializeField] private int _damage;
    [SerializeField] private int _xpCount;
    [SerializeField] private Mark _mark;

    private PlayerHealth _playerHealth;
    private EnemyHealth _health;

    public int Damage => _damage;
    public Player Target => _player;
    public int XPCount => _xpCount;

    private void Awake()
    {
        _playerHealth = _player.gameObject.GetComponent<PlayerHealth>();
        _health = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        ChangeMark();
    }

    public void ChangeMark()
    {
        if(_mark!=null)
        _mark.gameObject.SetActive(!_mark.gameObject.activeSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            _playerHealth.ApplyDamage(_damage);
        }

        if (other.gameObject.TryGetComponent<Shuriken>(out Shuriken shuriken))
        {
            _health.ApplyDamage(_playerStats.Damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            _playerHealth.ApplyDamage(_damage);
        }
    }
}