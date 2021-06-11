using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private Slider _slider;
    private int _maxHealth;
    private int _currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _slider.maxValue = _maxHealth;
        _slider.value = _currentHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        //_slider.maxValue = maxHealth;
        _maxHealth = maxHealth;
    }

    public void SetCurrentHealth(int currentHealth)
    {
        _currentHealth = currentHealth;
        //_slider.value = currentHealth;
    }
}
