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
    public Material destroyedMaterial;
    private MeshFilter _filter;
    private MeshRenderer _renderer;
    public enum Damaged
    {
        Undamaged,
        Damaged,
        Destroyed
    }
    public enum SizeCategory
    {
        Small,
        Medium,
        Large
    }
    public Damaged state;
    public SizeCategory size;
    void Start()
    {
        damageSound = GetComponent<AudioSource>();
        damageSound.clip = (AudioClip)Resources.Load("BuildingDamage/DebrisHit" + Random.Range(1, 2).ToString());
        
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
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
                _renderer.material = destroyedMaterial;
                switch (size)
                {
                    case SizeCategory.Small:
                        transform.position = transform.position - new Vector3(0, 1.52f, 0); // realign new position
                        break;
                    case SizeCategory.Medium:
                        transform.position = transform.position - new Vector3(0, 2, 0); // realign new position
                        break;
                    case SizeCategory.Large:
                        transform.position = transform.position - new Vector3(0.4f, 6, 0.4f); // realign new position
                        break;
                }
                
            }
        }
    }
}
