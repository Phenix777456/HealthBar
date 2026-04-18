using System;
using UnityEngine;

public sealed class Health : MonoBehaviour
{
    [SerializeField] private float _max = 100f;

    public event Action<float, float> HealthChanged;

    public float Max => _max;
    public float Current { get; private set; }

    private void Awake()
    {
        Current = _max;
    }

    public void ApplyDamage(float amount)
    {
        if (amount <= 0f) 
            return;

        SetHealth(Current - amount);
    }

    public void ApplyHeal(float amount)
    {
        if (amount <= 0f)
            return;

        SetHealth(Current + amount);
    }

    private void SetHealth(float value)
    {
        float previous = Current;
        Current = Mathf.Clamp(value, 0f, _max);

        if (Mathf.Approximately(previous, Current) == false)
            HealthChanged?.Invoke(Current, _max);
    }
}
