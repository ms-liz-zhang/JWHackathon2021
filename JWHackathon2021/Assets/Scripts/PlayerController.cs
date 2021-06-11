using Assets.Scripts.Lib;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Range(1, 100)]
    public float jumpForce = 7;
    public float speed = 5;

    public float attackDuration;
    public float timeBetweenAttacks;

    private Rigidbody _rigidBody;
    public HealthController healthController;
    public int maxHealth;
    private int _currentHealth;

    public LayerMask groundLayers;

    public ActorState currentState;

    private SphereCollider _collider;

    private bool _isJumping = false;

    private Timer _attackDurationTimer;
    private Timer _timeBetweenAttacksTimer;

    private Animator _animator;
    private bool _isColliding;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _rigidBody = GetComponent<Rigidbody>();

        _attackDurationTimer = new Timer(attackDuration);
        _attackDurationTimer.Start();
        _timeBetweenAttacksTimer = new Timer(timeBetweenAttacks);
        _timeBetweenAttacksTimer.Start();
        // can immedietly attack
        _timeBetweenAttacksTimer.Update(timeBetweenAttacks);
        currentState = ActorState.Idle;

        healthController.SetMaxHealth(maxHealth);
        healthController.SetCurrentHealth(maxHealth);
        _currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
        _isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        _isColliding = false;
        healthController.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        if (IsGrounded())
        {
            _isJumping = false;
        }

        if (Input.GetButton("Jump") && !_isJumping)
        {
            _isJumping = true;
            var jumpMovement = Vector3.up * jumpForce;
            _rigidBody.AddForce(jumpMovement, ForceMode.Impulse);
        }

        _timeBetweenAttacksTimer.Update(Time.deltaTime);
        _attackDurationTimer.Update(Time.deltaTime);

        if (_timeBetweenAttacksTimer.HasTimeElapsed())
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                _attackDurationTimer.Start();

                currentState = ActorState.Attacking;
            }

            if (_attackDurationTimer.HasTimeElapsed())
            {
                currentState = ActorState.Idle;
                _timeBetweenAttacksTimer.Reset();
                _attackDurationTimer.Stop();
                _attackDurationTimer.Reset();
            }
        }

        if (_currentHealth <= 0)
        {
            SceneManager.LoadScene(1);
        }

        _animator.SetInteger("State", (int)currentState);
    }

    private void FixedUpdate()
    {
        var movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        transform.position += movementVector * speed * Time.deltaTime;


        if (movementVector.x < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if (movementVector.x > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(_collider.bounds.center,
            new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z),
            _collider.radius * 0.9f,
            groundLayers);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isColliding)
            return;

        var enemyAttack = other.gameObject.GetComponent<AttackController>();

        if (enemyAttack == null)
            return;

        _isColliding = true;

        _currentHealth -= enemyAttack.Damage;
        healthController.SetCurrentHealth(_currentHealth);
    }
}
