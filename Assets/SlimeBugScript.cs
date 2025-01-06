using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBugScrpt : MonoBehaviour, IEnemy
{
    public EnemySO EnemyInfo{get;set;}
    public SplashSlime splashSlime;

    public BoxCollider2D SpawnArea { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    // Start is called before the first frame update
    void Start()
    {
        splashSlime = GetComponent<SplashSlime>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpecialAbility(PlayerMovement playerMovement)
    {
        
        splashSlime.Splash(playerMovement);
        
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
