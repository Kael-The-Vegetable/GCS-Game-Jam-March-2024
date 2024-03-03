using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public CharacterController controller;
    public Animator animator;
    public float speed;
    public float rotateSpeed;
    public int maxHealth;
    public float stompCooldown;
    public Transform _groundCheck;
    public LayerMask groundmask;
    public Attack stomp;
    public Attack punch;

    private float _currentCooldown;
    private int _currentHealth;
    private bool _isGrounded = false;
    private bool _canJump = false;
    private bool _alive;
    private bool _attacking = false;

    private CharacterStates _currentState;
    private enum CharacterStates
    {
        Idle,
        Walking,
        Stomping,
        Punching,
        Jumping
    }
    
    private Vector3 _moveDirection;
    private Vector3 _lookDirection;
    private Vector3 _velocity;
    private Vector3 _lastVelocity;
    private Vector3 _downwardVel;

    void HandleMovement()
    {
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        _moveDirection = IsoNormalize(_moveDirection);
        _velocity = _moveDirection * speed * Time.deltaTime;

        if (_moveDirection != Vector3.zero)
        { _lookDirection = _moveDirection; }
        Quaternion rotation = Quaternion.LookRotation(_lookDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

        _currentState = CharacterStates.Walking;

        if (!_isGrounded)
        {
            Gravity();
        }
        else
        {
            _downwardVel = Vector3.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
        _alive = true;
    }
    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.Raycast(_groundCheck.position, Vector2.down, 0.3f, groundmask);

        if (_alive) // if alive then you can do all this.
        {
            if (_attacking == false)
            {
                HandleMovement();
            }

            switch (_currentState)
            {
                case CharacterStates.Walking:
                   
                    animator.SetBool("Walking", true);
                    break;
                case CharacterStates.Jumping:
                    break;
                case CharacterStates.Stomping:
                    break;
                case CharacterStates.Punching:
                    break;
                default:
                    animator.SetBool("Walking", false);
                    break;
            }


            // if the character is grounded and the cooldown is 0, then the character can jump
            _canJump = _isGrounded && _currentCooldown == 0;
           
            

            if (Input.GetButtonDown("Stomp"))
            {
                stomp.OnAttack();
            }
            if (Input.GetButtonDown("Punch"))
            {
                punch.OnAttack();
            }

           

            if (_currentHealth <= 0)
            {
                _alive = false;
            }
        }

        


        



        controller.Move(_velocity);
        _lastVelocity = _velocity;
    }
    private void Gravity()
    {
        float gravity = WorldManager.Global.Gravity;
        _downwardVel = Vector3.up * gravity * Time.deltaTime;
        // _downwardVel = (_downwardVel) * Time.deltaTime;
        _velocity += _downwardVel;
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