using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBugScript : MonoBehaviour, IEnemy
{
    public EnemySO EnemyInfo{get;set;}
    public GoInvisible goInvisible;

    public BoxCollider2D SpawnArea { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    // Start is called before the first frame update

    void Start()
    {
        goInvisible = GetComponent<GoInvisible>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpecialAbility(PlayerMovement playerMovement)
    {
        goInvisible.SpecialAbility();
    }

    public void SetProvoked()
    {
        throw new System.NotImplementedException();
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void PlayerIsDetected()
    {
        throw new System.NotImplementedException();
    }

    public void PlayerIsOutOfChaseRange()
    {
        throw new System.NotImplementedException();
    }

    public void PlayerOutOfRange()
    {
        throw new System.NotImplementedException();
    }

    public void PlayerWithinAttackRange()
    {
        throw new System.NotImplementedException();
    }

    public void PlayerOutOfAttackRange()
    {
        throw new System.NotImplementedException();
    }

    public Coroutine TriggerCoroutine(IEnumerator coroutine)
    {
        throw new System.NotImplementedException();
    }

    public void CancelCoroutine(Coroutine corutine){
        StopCoroutine(corutine);
    }
}
