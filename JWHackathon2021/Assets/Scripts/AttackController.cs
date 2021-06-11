using Assets.Scripts.Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour, IAttacker
{
    public float attackDuration;
    public float timeBetweenAttacks;

    public EnemyController parent;

    private Collider _attackCollider;

    private Timer _attackDurationTimer;
    private Timer _timeBetweenAttacksTimer;

    public int Damage { get { return 1; } }

    // Start is called before the first frame update
    void Start()
    {
        _attackCollider = GetComponent<Collider>();
        //_attackCollider.enabled = false;
        _attackDurationTimer = new Timer(attackDuration);
        _attackDurationTimer.Start();
        _timeBetweenAttacksTimer = new Timer(timeBetweenAttacks);
        _timeBetweenAttacksTimer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _timeBetweenAttacksTimer.Update(Time.deltaTime);
        _attackDurationTimer.Update(Time.deltaTime);

        if (_timeBetweenAttacksTimer.HasTimeElapsed())
        {
            _attackDurationTimer.Start();
            //_attackCollider.enabled = true;
            parent.State = ActorState.Attacking;

            if (_attackDurationTimer.HasTimeElapsed())
            {
                parent.State = ActorState.Idle;
                //_attackCollider.enabled = false;
                _timeBetweenAttacksTimer.Reset();
                _attackDurationTimer.Stop();
                _attackDurationTimer.Reset();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
