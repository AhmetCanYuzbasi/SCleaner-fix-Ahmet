using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBugScript : MonoBehaviour, IEnemy
{
    public GoInvisible goInvisible;
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
}
