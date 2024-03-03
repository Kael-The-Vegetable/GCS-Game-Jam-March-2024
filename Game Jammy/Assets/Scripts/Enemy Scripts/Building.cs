using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour, IDamageable
{
    public int maxHealth;
    private int _currentHealth;
    public enum Damaged
    {
        Undamaged,
        Damaged,
        Destroyed
    }
    public Damaged state;
    void Start()
    {
        _currentHealth = maxHealth;
        state = Damaged.Undamaged;
    }

    void Update()
    {
        switch(state)
        {
            case Damaged.Undamaged:
                break;
            case Damaged.Damaged:
                break;
            case Damaged.Destroyed:
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        state = Damaged.Damaged;
        if (_currentHealth <= 0)
        {
            state = Damaged.Destroyed;
        }
    }
}
