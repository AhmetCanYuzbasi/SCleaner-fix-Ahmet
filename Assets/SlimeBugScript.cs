using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBugScrpt : MonoBehaviour, IEnemy
{
    public SplashSlime splashSlime;
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
}
