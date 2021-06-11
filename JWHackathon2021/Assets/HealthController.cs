using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxHealth(int maxHealth)
    {
        _slider.maxValue = maxHealth;
    }

    public void SetCurrentHealth(int currentHealth)
    {
        _slider.value = currentHealth;
    }
}
