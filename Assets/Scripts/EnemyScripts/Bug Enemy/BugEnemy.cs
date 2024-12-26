using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class BugEnemy : MonoBehaviour, IEnemy
{
    
    StateMachine _stateMachine;
    GameObject _player;
    Rigidbody2D _rb2D;
    NavMeshAgent _agent;
    Transform _groundTransform;
    BoxCollider2D _spawnArea;
    UnitBoolsSO infoSO;
    UnitInfoSO _playerInfo;
    

    void Awake(){
        if (infoSO == null){
            infoSO = ScriptableObject.CreateInstance<UnitBoolsSO>();
        }
    }

    void Start()
    {

        //Initialize fields
        string description1 = "roam to stop predicate";
        string description2 = "stop to roam predicate";
        infoSO.Init();


        //Initialize components
        _stateMachine = new StateMachine();
        _rb2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerInfo = _player.GetComponent<PlayerMain>().playerInfo;        
        //_groundTransform = GameObject.FindGameObjectWithTag("Ground").GetComponent<Transform>();
        _spawnArea = GameObject.FindGameObjectWithTag("Spawn Area").GetComponent<BoxCollider2D>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    


        //Initialize states
        //RoamState roamState = new RoamState(this, _player, _rb2D);
        RoamState roamState = new RoamState(this, _player, _rb2D, _agent, _groundTransform, _spawnArea);
        ChaseState chaseState = new ChaseState(this, _player, _rb2D, _agent);
        //ChaseState chaseState = new ChaseState(this, _player, _rb2D);
        //AttackState attackState = new AttackState(this, _player, _rb2D);
        
        //Initialize transitions
        
        
        At(roamState, chaseState, new FuncPredicate( () => infoSO.playerDetected, description1));
        At(chaseState, roamState, new FuncPredicate( () => !infoSO.playerDetected, description2));
        //At(attackState, chaseState, new FuncPredicate( () => !playerInAttackRange));

        At(roamState, chaseState, new FuncPredicate( () => infoSO.hasBeenAttacked ));

        Any(roamState, new FuncPredicate(() => !_playerInfo.isAlive, "player is dead!"));
        //Any(attackState, new FuncPredicate( () => playerInAttackRange));
        //Any(roamState, new FuncPredicate(() => playerDetected));
        //Any(stopState, new FuncPredicate(() => !playerDetected));


        //Set initial state
        _stateMachine.SetState(roamState);
    }

    void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

    void Update(){
        _stateMachine.Update();
    }

    void FixedUpdate(){
        _stateMachine.FixedUpdate();
    }

    public void PlayerIsDetected(){
        infoSO.playerDetected = true;
        
    }

    public void PlayerIsOutOfChaseRange(){
        infoSO.playerDetected = false;
    }
    public void SetProvoked()
    {
        infoSO.hasBeenAttacked = true;
    }
    public void Attack(){
        //no op
    }

    public void SpecialAbility()
    {
        throw new NotImplementedException();
    }

    public void SpecialAbility(PlayerMovement playerMovement)
    {
        throw new NotImplementedException();
    }
}
