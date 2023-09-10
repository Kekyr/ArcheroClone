using UnityEngine;

public class BossDied : MonoBehaviour
{
    [SerializeField] private LevelView _levelView;
    [SerializeField] private EnemyHealthView _enemyHealthView;
    [SerializeField] private EnemyHealth _enemyHealth;

    private void OnEnable()
    {
        _enemyHealth.Dying += OnBossDied;
    }

    private void OnDisable()
    {
        _enemyHealth.Dying -= OnBossDied;
    }

    private void OnBossDied(Enemy enemy)
    {
        _enemyHealthView.gameObject.SetActive(false);
        _levelView.gameObject.SetActive(true);
    }
}