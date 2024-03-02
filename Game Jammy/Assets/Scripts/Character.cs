using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public CharacterController controller;
    public float speed;
    public int maxHealth;
    public float jumpCooldown;

    private float _currentCooldown;
    private int _currentHealth;
    private Vector3 _moveDirection;
    private bool _isGrounded = false;
    private bool _canJump = false;
    private bool _alive;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
        _alive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (_alive) // if alive then you an do all this.
        {
            
            // if the character is grounded and the cooldown is 0, then the character can jump
            _canJump = _isGrounded && _currentCooldown == 0;

            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            controller.Move(_moveDirection * speed * Time.deltaTime);

            if (_currentHealth <= 0)
            {
                _alive = false;
            }
        }
    }

    


    public void TakeDamage(int damage)
    {
        // take X amount of damage when this is called.
        _currentHealth -= damage;
    }
}

public interface IDamageable
{
    void TakeDamage(int damage);
}