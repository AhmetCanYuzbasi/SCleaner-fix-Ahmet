using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : BaseState
{   
    Transform target;

    Vector2 targetDirection;
    public ChaseState(BugEnemy enemy, GameObject player, Rigidbody2D rb2D, NavMeshAgent agent) : base(enemy, player, rb2D, agent){
        target = player.GetComponent<Transform>();
    }
    public override void OnEnter()
    {
        //no op
    }


    public override void StateUpdate()
    {
        RotateTowardsPlayer();
        agent.SetDestination(target.position);
    }

    private void RotateTowardsPlayer(){
        
        CalcTargetDirection();
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        enemy.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void CalcTargetDirection(){
        targetDirection = (target.position - enemy.transform.position).normalized;
    }
}
