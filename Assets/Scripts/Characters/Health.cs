using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    public event UnityAction<float> HealthChanged;

    protected virtual void OnHealthChanged(float value)
    {
        UnityAction<float> healthChanged = HealthChanged;
        healthChanged?.Invoke(value);
    }

    [SerializeField] protected float MaxHealth;

    protected float CurrentHealth;

    public float HealthPoints => CurrentHealth;
    public float Ratio => CurrentHealth / MaxHealth;

    public abstract void ApplyDamage(float damage);
}