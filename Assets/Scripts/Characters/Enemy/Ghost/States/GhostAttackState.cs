using System.Collections;
using UnityEngine;

public class GhostAttackState : State
{
    [SerializeField] private PoolSO _spherePool;
    [SerializeField] private SFXSO _sfx;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _rotationSpeed;

    private Audio _audio;
    private ObjectPooler _objectPool;

    private void Awake()
    {
        _audio = GetComponentInChildren<Audio>();
        _objectPool = FindObjectOfType<ObjectPooler>();
    }

    private void Start()
    {
        _objectPool.AddPool(_spherePool);
    }

    private void OnEnable()
    {
        Vector3 targetPosition = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        StartCoroutine(RotateToPlayer(newRotation, direction));
    }

    private void SpawnProjectile(Vector3 moveDirection, Vector3 spawnPosition)
    {
        Projectile projectile = _objectPool.SpawnFromPool(_spherePool.Tag, spawnPosition, _spherePool.Prefab.transform.rotation);
        projectile.gameObject.GetComponent<SphereMovementData>().Init(moveDirection);
        _audio.Play(_sfx);
    }

    private IEnumerator RotateToPlayer(Quaternion newRotation, Vector3 direction)
    {
        while (transform.rotation != newRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
            yield return null;
        }

        SpawnProjectile(direction, _spawnPosition.position);
    }
}
