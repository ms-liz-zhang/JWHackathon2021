using Assets.Scripts.Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ActorState State { get; set; }

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        State = ActorState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetInteger("State", (int)State);
    }
}
