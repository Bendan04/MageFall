
using UnityEngine;
using UnityEngine.UI;
 
public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Slider slider;
 
    private void OnEnable()
    {
        health.Changed += UpdateBar; //UPdate when health changes
    }
 
    private void OnDisable()
    {
        health.Changed -= UpdateBar; //Disable when change happens
    }
 
    private void Start()
    {
        UpdateBar(health.Current, health.Max); //Update bar at start
    }
 
    private void UpdateBar(float current, float max)
    {
        slider.value = current / max; //Update slider value
    }
}
 