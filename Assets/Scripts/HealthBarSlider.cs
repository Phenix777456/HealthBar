using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class HealthBarSlider : MonoBehaviour
{
    public enum DisplayMode { Smooth, Percent }

    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private DisplayMode _mode = DisplayMode.Smooth;
    [SerializeField] private float _smoothTime = 0.3f;

    private float _targetValue;
    private float _velocity;

    private void OnEnable()
    {
        _health.HealthChanged += HandleHealthChanged;
        _slider.value = _targetValue;
        RefreshLabel(_targetValue);
    }

    private void Start()
    {
        _targetValue = NormalizedHealth(); 
    }

    private void OnDisable()
    {
        _health.HealthChanged -= HandleHealthChanged;
    }

    private void Update()
    {
        if (Mathf.Approximately(_slider.value, _targetValue))
            return;

        _slider.value = Mathf.SmoothDamp(_slider.value, _targetValue, ref _velocity, _smoothTime);
        RefreshLabel(_slider.value);
    }

    private void HandleHealthChanged(float current, float max)
    {
        _targetValue = current / max;
    }

    private void RefreshLabel(float normalized)
    {
        if (_label == null)
            return;

        if (_mode == DisplayMode.Percent)
        {
            float percent = normalized * 100f;
            int percentRounded = Mathf.RoundToInt(percent);
            _label.text = percentRounded.ToString() + "%";
        }
        else
        {
            float currentHp = normalized * _health.Max;
            float maxHp = _health.Max;
            int currentRounded = Mathf.RoundToInt(currentHp);
            int maxRounded = Mathf.RoundToInt(maxHp);
            _label.text = currentRounded.ToString() + " / " + maxRounded.ToString();
        }
    }

    private float NormalizedHealth()
    {
        if (_health.Max <= 0f)
            return 0f;

        Debug.Log(_health.Current / _health.Max);

        return _health.Current / _health.Max;
    }
}
