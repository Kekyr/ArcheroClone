using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAttackState : PlayerState
{
    public readonly int IsAttacking = Animator.StringToHash("IsAttacking");

    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] private PoolSO _shurikenPool;
    [SerializeField] private SFXSO _sfx;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _betweenShurikenOffsetX;

    [Header("Spawn positions")]
    [SerializeField] private Transform _forward;
    [SerializeField] private Transform _backward;
    [SerializeField] private Transform _rightDiagonal;
    [SerializeField] private Transform _leftDiagonal;
    [SerializeField] private Transform _right;
    [SerializeField] private Transform _left;

    private Animator _animator;
    private WaitForSeconds _multiShotDelay;
    private Vector3 _modifiedTargetPosition;
    private Vector3 _direction;
    private Quaternion _newRotation;
    private Rigidbody _rigidbody;
    private Audio _audio;
    private ObjectPooler _objectPool;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audio = GetComponentInChildren<Audio>();
        _multiShotDelay = new WaitForSeconds(_playerStatsSO.MultiShotDelay);
        _objectPool = FindObjectOfType<ObjectPooler>();
    }

    private void Start()
    {
        _objectPool.AddPool(_shurikenPool);
    }

    private void OnEnable()
    {
        _rigidbody.velocity = Vector3.zero;
        _animator.SetBool(IsAttacking, true);
    }

    private void OnDisable()
    {
        _animator.SetBool(IsAttacking, false);
    }

    private void Update()
    {
        if (_enemyDetector.NearestEnemy != null)
        {
            RotateToEnemy(_enemyDetector.NearestEnemy);
        }
    }

    private void RotateToEnemy(Enemy enemy)
    {
        _modifiedTargetPosition = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
        _direction = (_modifiedTargetPosition - transform.position).normalized;
        _newRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _newRotation, _rotationSpeed * Time.deltaTime);
    }

    public void OnThrow()
    {
        _audio.Play(_sfx);
        StartCoroutine(SpawnShurikens(_forward.position, _direction, _playerStatsSO.ForwardCount));
        StartCoroutine(SpawnShurikens(_backward.position, -_direction, _playerStatsSO.BackwardCount));
        StartCoroutine(SpawnShurikens(_right.position, transform.right, _playerStatsSO.SideCount));
        StartCoroutine(SpawnShurikens(_left.position, -transform.right, _playerStatsSO.SideCount));
        StartCoroutine(SpawnShurikens(_rightDiagonal.position, transform.right + transform.forward, _playerStatsSO.DiagonalCount));
        StartCoroutine(SpawnShurikens(_leftDiagonal.position, -transform.right + transform.forward, _playerStatsSO.DiagonalCount));
    }

    private void SpawnShuriken(Vector3 moveDirection, Vector3 spawnPosition)
    {
        Projectile shuriken = _objectPool.SpawnFromPool(_shurikenPool.Tag, spawnPosition, _shurikenPool.Prefab.gameObject.transform.rotation);
        shuriken.GetComponent<ShurikenMovementData>().Init(moveDirection, transform);
    }

    private IEnumerator SpawnShurikens(Vector3 position, Vector3 direction, int count)
    {
        Vector3 currentSpawnPosition = position;
        Vector3 rightSpawnPosition = position;
        Vector3 leftSpawnPosition = position;

        int nextShurikenIndex;
        int divider = 2;

        if (count > 0)
        {
            for (int i = 1; i <= count; i++)
            {
                SpawnShuriken(direction, currentSpawnPosition);

                if (_playerStatsSO.MultiShotCount > 0)
                {
                    for (int j = 0; j < _playerStatsSO.MultiShotCount; j++)
                    {
                        yield return _multiShotDelay;
                        SpawnShuriken(direction, currentSpawnPosition);
                    }
                }

                nextShurikenIndex = i + 1;

                if (nextShurikenIndex % divider == 0)
                {
                    rightSpawnPosition.x += _betweenShurikenOffsetX;
                    currentSpawnPosition = rightSpawnPosition;
                }
                else
                {
                    leftSpawnPosition.x -= _betweenShurikenOffsetX;
                    currentSpawnPosition = leftSpawnPosition;
                }
            }
        }
    }
}