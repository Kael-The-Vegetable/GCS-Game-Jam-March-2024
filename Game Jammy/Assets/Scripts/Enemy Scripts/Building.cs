using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Building : MonoBehaviour, IDamageable
{
    public int maxHealth;
    private int _currentHealth;
    public AudioSource damageSound;
    public Mesh destroyedMesh;
    private MeshFilter _filter;
    public enum Damaged
    {
        Undamaged,
        Damaged,
        Destroyed
    }
    public Damaged state;
    void Start()
    {
        damageSound = GetComponent<AudioSource>();
        damageSound.clip = (AudioClip)Resources.Load("BuildingDamage/DebrisHit" + Random.Range(1, 2).ToString());
        
        _filter = GetComponent<MeshFilter>();
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
        if (state != Damaged.Destroyed)
        {
            _currentHealth -= damage;
            Debug.Log($"Current Health {_currentHealth}");
            damageSound.pitch = Random.Range(0.5f, 1.5f);
            state = Damaged.Damaged;
            damageSound.Play();
            if (damage > 10)
            {
                CameraShaker.Invoke(new Vector3(2, 2, 2), new Vector3(2, 2, 2));
            }

            if (_currentHealth <= 0)
            {
                state = Damaged.Destroyed;
                _filter.mesh = destroyedMesh;
                transform.position = transform.position - new Vector3(0.5f, 6, 0.5f); // realign new position
            }
        }
    }
}
