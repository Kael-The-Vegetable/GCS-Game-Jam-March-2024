using UnityEngine;
using UnityEngine.InputSystem;

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
    public Punch punch;
    public WalkingAttack walking;
    public bool attacking = false;
    public CharacterStates currentState;

    public InputAction moveInput;
    public InputAction stompInput;
    public InputAction punchInput;

    private float _currentCooldown;
    private int _currentHealth;
    private bool _isGrounded = false;
    private bool _canJump = false;
    private bool _alive;

    private void OnEnable()
    {
        moveInput.Enable();
        stompInput.Enable();
        punchInput.Enable();
    }

    private void OnDisable()
    {
        moveInput.Disable();
        stompInput.Disable();
        punchInput.Disable();
    }

    public enum CharacterStates
    {
        Idle,
        Walking,
        Stomping,
        Punching
    }

    private Vector2 _inputMoveDirection;
    private Vector3 _moveDirection;
    private Vector3 _lookDirection = new Vector3(1, 0, 1);
    private Vector3 _velocity;
    private Vector3 _lastVelocity;
    private Vector3 _downwardVel;

    void HandleMovement()
    {
        _inputMoveDirection = moveInput.ReadValue<Vector2>();
        _moveDirection = new Vector3(_inputMoveDirection.x, 0, _inputMoveDirection.y).normalized;
        _moveDirection = IsoNormalize(_moveDirection);
        _velocity = _moveDirection * speed * Time.deltaTime;

        if (_moveDirection != Vector3.zero)
        { 
            _lookDirection = _moveDirection;
            currentState = CharacterStates.Walking;
        }

        Quaternion rotation = Quaternion.LookRotation(_lookDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

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
        if (!WorldManager.Global.isPlaying || WorldManager.Global.timeRemaining <= 0)
        { _alive = false; }
        else 
        { _alive = true; }
        if (_alive) // if alive then you can do all this.
        {
            currentState = CharacterStates.Idle;
            if (attacking == false)
            {
                HandleMovement();
                if (stompInput.ReadValue<float>() > 0.5)
                {
                    stomp.OnAttack();
                    currentState = CharacterStates.Stomping;
                }
                else if (punchInput.ReadValue<float>() > 0.5)
                {
                    punch.OnAttack();
                    currentState = CharacterStates.Punching;
                }
            }
            else
            {
                _velocity = Vector3.zero;
            }

            switch (currentState)
            {
                case CharacterStates.Walking:
                    walking.OnAttack();
                    animator.SetBool("Walking", true);
                    break;
                case CharacterStates.Stomping:
                    animator.SetBool("Walking", false);
                    animator.SetBool("Stomp", true);
                    break;
                case CharacterStates.Punching:
                    animator.SetBool("Walking", false);
                    animator.SetBool("Punch", true);
                    break;
                case CharacterStates.Idle:
                    animator.SetBool("Walking", false);
                    break;
            }

            // if the character is grounded and the cooldown is 0, then the character can jump
            _canJump = _isGrounded && _currentCooldown == 0;           

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