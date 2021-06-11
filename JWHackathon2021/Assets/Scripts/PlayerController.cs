using Assets.Scripts.Lib;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1, 100)]
    public float jumpForce = 7;
    public float speed = 5;

    public float attackDuration;
    public float timeBetweenAttacks;

    private Rigidbody _rigidBody;

    public LayerMask groundLayers;

    public ActorState currentState;

    private SphereCollider _collider;

    private bool _isJumping = false;

    private Timer _attackDurationTimer;
    private Timer _timeBetweenAttacksTimer;

    private Animator _animator;

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

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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

        _animator.SetInteger("State", (int)currentState);
    }

    private void FixedUpdate()
    {
        var movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        transform.Translate(movementVector * speed * Time.deltaTime);


    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(_collider.bounds.center,
            new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z),
            _collider.radius * 0.9f,
            groundLayers);
    }
}
