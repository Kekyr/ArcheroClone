using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [SerializeField] private SFXSO _sfx;

    private LevelingUp _leveling;
    private PlayerHealth _playerHealth;
    private Audio _audio;

    private void Awake()
    {
        _leveling = GetComponent<LevelingUp>();
        _playerHealth = GetComponent<PlayerHealth>();
        _audio = GetComponentInChildren<Audio>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ShurikenMovementData>(out ShurikenMovementData shuriken))
        {
            if (shuriken.IsReturning)
            {
                shuriken.gameObject.SetActive(false);
                shuriken.ChangeReturnStatus();
            }
        }
        else if (other.gameObject.TryGetComponent<XP>(out XP xp))
        {
            Destroy(other.gameObject);
            _leveling.AddXp();
            _audio.Play(_sfx);

        }
        else if(other.gameObject.TryGetComponent<Sphere>(out Sphere projectile))
        {
            _playerHealth.ApplyDamage(projectile.Damage);
        }
    }
}