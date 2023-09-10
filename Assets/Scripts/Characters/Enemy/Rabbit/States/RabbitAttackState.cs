using System.Collections;
using UnityEngine;

public class RabbitAttackState : State
{
    [SerializeField] private PoolSO _spherePool;
    [SerializeField] private SFXSO _sfx;
    [SerializeField] private Sphere _projectilePrefab;
    [SerializeField] private Transform _forward;
    [SerializeField] private Transform _rightDiagonal;
    [SerializeField] private Transform _leftDiagonal;
    [SerializeField] private int _minNumberOfAttack;
    [SerializeField] private int _maxNumberOfAttack;
    [SerializeField] private float _timeBetweenAttack;
    [SerializeField] private float _rotationSpeed;

    private WaitForSeconds _waitForSeconds;
    private bool _completed;
    private Audio _audio;
    private ObjectPooler _objectPool;

    public bool Completed => _completed;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_timeBetweenAttack);
        _audio = GetComponentInChildren<Audio>();
        _objectPool = FindObjectOfType<ObjectPooler>();
    }

    private void Start()
    {
        _objectPool.AddPool(_spherePool);
    }

    private void OnEnable()
    {
        int numberOfAttack = Random.Range(_minNumberOfAttack, _maxNumberOfAttack);
        StartCoroutine(SpawnProjectiles(numberOfAttack));
    }

    private void OnDisable()
    {
        _completed = false;
    }

    private void SpawnProjectile(Vector3 moveDirection, Vector3 spawnPosition)
    {
        Projectile projectile = _objectPool.SpawnFromPool(_spherePool.Tag, spawnPosition, _spherePool.Prefab.transform.rotation);
        projectile.GetComponent<SphereMovementData>().Init(moveDirection);
    }

    private IEnumerator SpawnProjectiles(int numberOfAttack)
    {
        for (int i = 0; i < numberOfAttack; i++)
        {
            Vector3 targetPosition = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion newRotation = Quaternion.LookRotation(direction);

            while (transform.rotation != newRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
                yield return null;
            }

            _audio.Play(_sfx);
            SpawnProjectile(transform.forward, _forward.position);
            SpawnProjectile(transform.forward + transform.right, _rightDiagonal.position);
            SpawnProjectile(transform.forward - transform.right, _leftDiagonal.position);
            yield return _waitForSeconds;
        }

        _completed = true;
    }
}
