using System;
using UnityEngine;
 
public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
 
    public float Current { get; private set; }
    public float Max => maxHealth;
 
    public event Action<float, float> Changed; // Event triggered when health changes
    public event Action Died; // Event triggered when health reaches zero
 
    private void Awake()
    {
        Current = maxHealth;
    }
 
    public void TakeDamage(float amount) // Damage method
    {
        if (Current <= 0f) return; // If already dead, do nothing
 
        Current = Mathf.Max(Current - amount, 0f); 
        Changed?.Invoke(Current, maxHealth);
 
        if (Current <= 0f) Died?.Invoke(); // Trigger the Died event if health reaches zero
    }
 
    public void Heal(float amount) // Healing method (future implementation)
    {
        if (Current <= 0f) return; // If already dead, do nothing
 
        Current = Mathf.Min(Current + amount, maxHealth);
        Changed?.Invoke(Current, maxHealth);
    }
 
    [ContextMenu("Take Damage")]
    private void TakeDamage()// This method is for testing purposes in the Unity Editor. It allows you to simulate taking damage by calling this method from the context menu of the Health component in the Inspector.
    {
        TakeDamage(5f);
    }
}
 