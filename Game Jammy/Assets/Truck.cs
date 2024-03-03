using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Building;

public class Truck : MonoBehaviour, IDamageable
{
    public int health;
    public AudioSource onHitSound;

   public void TakeDamage(int Damage)
    {
        health -= Damage;
        onHitSound.pitch = Random.Range(0.5f, 1.5f);
        onHitSound.Play();
        if (health <= 0)
        {

        }
    }
}
