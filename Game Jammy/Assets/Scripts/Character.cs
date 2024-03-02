using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public CharacterController controller;
    public float speed { get; private set; }
    public int maxHealth { get; private set; }
    public float jumpCooldown { get; private set; }

    private float _currentCooldown;
    private int _currentHealth;
    private Vector3 _moveDirection;
    private bool _isGrounded = false;
    private bool _canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;

    }
    // Update is called once per frame
    void Update()
    {

        // if the character is grounded and the cooldown is 0, then the character can jump
        _canJump = _isGrounded && _currentCooldown == 0;

        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        controller.Move(_moveDirection * speed * Time.deltaTime);

    }


    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }
}

public interface IDamageable
{
    void TakeDamage(int damage);
}