using TMPro;
using UnityEngine;

public sealed class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TMP_Text _label;

    private void OnEnable()
    {
        _health.HealthChanged += HandleHealthChanged;
        Refresh(_health.Current, _health.Max);
    }

    private void OnDisable()
    {
        _health.HealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(float current, float max)
    {
        Refresh(current, max);
    }

    private void Refresh(float current, float max)
    {
        _label.text = $"{current:0} / {max:0}";
    }
}
