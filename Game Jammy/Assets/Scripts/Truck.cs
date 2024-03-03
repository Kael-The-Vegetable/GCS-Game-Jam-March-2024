using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;
using static Building;

public class Truck : MonoBehaviour, IDamageable
{
    public int health;
    public AudioSource onHitSound;
    public GameObject explosion;

   public void TakeDamage(int Damage)
    {
        health -= Damage;
        onHitSound.pitch = Random.Range(0.5f, 1.5f);
        onHitSound.Play();
        if (health <= 0)
        {
            GameObject explosionInstance = Instantiate(explosion);
            explosionInstance.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
