using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class HealthBarSlider : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private float _smoothTime;

    private float _targetValue;
    private float _velocity;

    private void OnEnable()
    {
        _health.HealthChanged += HandleHealthChanged;
        RefreshLabel(_targetValue);
    }

    private void Start()
    {
        _targetValue = NormalizedHealth();
        StartCoroutine(ChangeHealth());
    }

    private void OnDisable()
    {
        _health.HealthChanged -= HandleHealthChanged;
    }

    private IEnumerator ChangeHealth()
    {
        while(Mathf.Approximately(_slider.value, _targetValue) == false)
        {
            _slider.value = Mathf.SmoothDamp(_slider.value, _targetValue, ref _velocity, _smoothTime);
            RefreshLabel(_slider.value);
            yield return null;
        }

        _slider.value = _targetValue;
        RefreshLabel(_slider.value);
    }

    private void HandleHealthChanged(float current, float max)
    {
        _targetValue = current / max;
        StartCoroutine(ChangeHealth());
    }

    private void RefreshLabel(float normalized)
    {
        if (_label == null)
            return;

        float currentHp = normalized * _health.Max;
        float maxHp = _health.Max;
        int currentRounded = Mathf.RoundToInt(currentHp);
        int maxRounded = Mathf.RoundToInt(maxHp);
        _label.text = currentRounded.ToString() + " / " + maxRounded.ToString();
    }

    private float NormalizedHealth()
    {
        if (_health.Max <= 0f)
            return 0f;

        Debug.Log(_health.Current / _health.Max);

        return _health.Current / _health.Max;
    }
}
