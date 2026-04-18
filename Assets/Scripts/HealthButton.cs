using UnityEngine;
using UnityEngine.UI;

public sealed class HealthButton : MonoBehaviour
{
    public enum ButtonMode { Damage, Heal }

    [SerializeField] private Health _health;
    [SerializeField] private Button _button;
    [SerializeField] private ButtonMode _mode = ButtonMode.Damage;
    [SerializeField] private float _amount = 10f;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        if (_mode == ButtonMode.Damage)
            _health.ApplyDamage(_amount);
        else
            _health.ApplyHeal(_amount);
    }
}
