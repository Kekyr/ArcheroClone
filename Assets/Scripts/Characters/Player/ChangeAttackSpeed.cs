using UnityEngine;

public class ChangeAttackSpeed : MonoBehaviour
{
    public readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");

    [SerializeField] private PlayerStatsSO _playerStatsSO;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.SetFloat(AttackSpeed, _playerStatsSO.AttackSpeed);
    }

    private void OnEnable()
    {
        _playerStatsSO.AttackSpeedChanged += OnAttackSpeedChanged;
    }

    private void OnDisable()
    {
        _playerStatsSO.AttackSpeedChanged -= OnAttackSpeedChanged;
    }

    private void OnAttackSpeedChanged(float newAttackSpeed)
    {
        _animator.SetFloat(AttackSpeed, newAttackSpeed);
    }
}