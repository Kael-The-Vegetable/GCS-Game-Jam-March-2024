using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public CharacterController controller;
    public float speed;
    public int maxHealth;
    public float stompCooldown;
    public ParticleSystem walkingParticles;

    private float _currentCooldown;
    private int _currentHealth;
    private bool _isGrounded = false;
    private bool _canStomp = false;
    private bool _alive;
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private Vector3 _lastVelocity;
    private Vector3 _downwardVel;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
        _alive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (_alive) // if alive then you can do all this.
        {
            // if the character is grounded and the cooldown is 0, then the character can jump
            _canStomp = _isGrounded && _currentCooldown == 0;
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            if (_canStomp)
            {
                Stomp();
            }

            Debug.Log(controller.isGrounded);
            _moveDirection = IsoNormalize(_moveDirection);
            if (_moveDirection.magnitude == 1 && controller.isGrounded)
            {
                Debug.Log("Play");
                walkingParticles.Play();
            }
            else
            {
                walkingParticles.Stop();
            }

            _velocity = _moveDirection * speed * Time.deltaTime;
            transform.LookAt(transform.position + _moveDirection);

            if (_currentHealth <= 0)
            {
                _alive = false;
            }
        }

        if (!controller.isGrounded)
        {
            Gravity();
        }
        else
        {
            
            _downwardVel = Vector3.zero;
        }


        controller.Move(Vector3.Slerp(_lastVelocity, _velocity, 0.5f));
        _lastVelocity = _velocity;
    }
    private void Gravity()
    {
        float gravity = WorldManager.Global.Gravity;
        _downwardVel = Vector3.up * gravity * Time.deltaTime;
        _downwardVel = (controller.velocity + _downwardVel) * Time.deltaTime;
    }


    void Stomp()
    {
        _currentCooldown = stompCooldown;
    }

    Vector3 IsoNormalize(Vector3 dir)
    {
        return (Quaternion.Euler(0, 45, 0) * dir).normalized;
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