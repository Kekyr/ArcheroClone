using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGhostAttackState : State
{
    [SerializeField] private PoolSO _spherePool;
    [SerializeField] private SFXSO _sfx;
    [SerializeField] private Sphere _projectilePrefab;
    [SerializeField] private List<Transform> _spawnPositions = new List<Transform>();
    [SerializeField] private int _minNumberOfAttack;
    [SerializeField] private int _maxNumberOfAttack;
    [SerializeField] private int _diagonalType;
    [SerializeField] private int _forwardType;
    [SerializeField] private float _timeBetweenAttack;
    [SerializeField] private float _rotationSpeed;

    private WaitForSeconds _waitForSeconds;
    private bool _completed;
    private bool _isRight;
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
        int numberOfAttack = UnityEngine.Random.Range(_minNumberOfAttack, _maxNumberOfAttack);
        bool isForward = Convert.ToBoolean(UnityEngine.Random.Range(0, 1));
        StartCoroutine(SpawnProjectiles(numberOfAttack, isForward));
    }

    private void OnDisable()
    {
        _completed = false;
    }

    private void SpawnProjectile(Vector3 moveDirection, Vector3 spawnPosition)
    {
        Projectile projectile = _objectPool.SpawnFromPool(_spherePool.Tag,spawnPosition,_spherePool.Prefab.transform.rotation);
        projectile.GetComponent<SphereMovementData>().Init(moveDirection);
    }

    private IEnumerator SpawnProjectiles(int numberOfAttack, bool isForward)
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

            foreach (Transform spawnPosition in _spawnPositions)
            {
                SpawnForwardProjectile(spawnPosition.position);
            }

            yield return _waitForSeconds;
        }

        _completed = true;
    }

    private void SpawnForwardProjectile(Vector3 spawnPosition)
    {
        SpawnProjectile(transform.forward, spawnPosition);
    }

    private void SpawnDiagonalProjectile(Vector3 spawnPosition)
    {
        Vector3 moveDirection;

        if (_isRight)
        {
            moveDirection = transform.forward + transform.right;
            _isRight = false;
        }
        else
        {
            moveDirection = transform.forward - transform.right;
            _isRight = true;
        }

        SpawnProjectile(moveDirection, spawnPosition);
    }
}