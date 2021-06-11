using Assets.Scripts.Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ActorState State { get; set; }

    public int maxHealth;
    public int currentHealth;

    public HealthController healthController; 

    private Animator _animator;
    private bool _isColliding;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        State = ActorState.Idle;
        currentHealth = maxHealth;
        healthController.SetMaxHealth(maxHealth);
        healthController.SetCurrentHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        _isColliding = false;
        _animator.SetInteger("State", (int)State);

        healthController.SetCurrentHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isColliding)
            return;

        var playerAttack = other.gameObject.GetComponent<PlayerAttackController>();

        if (playerAttack == null)
            return;

        _isColliding = true;

        currentHealth -= playerAttack.Damage;
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
