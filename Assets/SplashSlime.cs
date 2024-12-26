using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SplashSlime : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite splashSprite;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Splash(PlayerMovement playerMovement){
        print("Slime splashed!");
        sr.sprite = splashSprite;
        transform.localScale = new Vector3(2,1.2f,2);
        playerMovement.playerInfo.movementSpeed *= 0.5f;

    }
}