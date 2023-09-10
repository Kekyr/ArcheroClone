using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;

    private WaitForSeconds _waitForSeconds;
    private bool _exited;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_delay);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            _exited = false;
            StartCoroutine(DamagePlayer(playerHealth));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            _exited = true;
        }
    }

    private IEnumerator DamagePlayer(PlayerHealth playerHealth)
    {
        while (_exited == false)
        {
            playerHealth.ApplyDamage(_damage);
            yield return _waitForSeconds;
        }
    }
}
