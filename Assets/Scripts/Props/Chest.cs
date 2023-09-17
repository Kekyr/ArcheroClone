using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    [SerializeField] private SFXSO _sfx;
    [SerializeField] private AbilityManager _chestScreen;

    private Animator _animator;
    private Audio _audio;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponentInChildren<Audio>();
    }

    private void Start()
    {
        _animator.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _audio.Play(_sfx);
            _animator.enabled = true;
        }
    }

    private void OnChestOpened()
    {
        _chestScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnAfterChestOpened()
    {
        Destroy(gameObject);
    }
}