using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour, IEnemy, IDamageable
{
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

    public void SetProvoked()
    {
        throw new System.NotImplementedException();
    }


    public void TakeDamage(int amount)
    {
        print("I have taken damage!");
    }

    public void TakeDamage(IEnemy attacker)
    {
        throw new System.NotImplementedException();
    }
}
