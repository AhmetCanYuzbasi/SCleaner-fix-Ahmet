using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuctionAbility : MonoBehaviour
{

    CircleCollider2D _circleCollider2D;
    List<Rigidbody2D> enemyRigidbodies = new List<Rigidbody2D>();
    float pullForce = 1f;


    void OnEnable(){
        //Grenade.OnAbilityTrigger += EnableScript;
    }

    void OnDisable(){
        
    }

    void Awake(){
        Grenade.OnAbilityTrigger += EnableScript;
    }

    void OnDestroy(){
        Grenade.OnAbilityTrigger -= EnableScript;
    }

   

    // Update is called once per frame
    void Update()
    {
        DragEnemiesTowardsCenter();
    }

    void EnableScript(){
        print("enabled");
        enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other){
        print("greetings");

        Rigidbody2D enemyRB;
        IEnemy enemy = other.GetComponent<IEnemy>();
        
        if (enemy != null){
            print("added enemy");
            enemyRB = other.GetComponent<Rigidbody2D>();
            enemyRigidbodies.Add(enemyRB);
        }
        
    }

    void OnTriggerExit2D(Collider2D other){
        print("removed enemy");
        Rigidbody2D enemyRB = other.gameObject.GetComponent<Rigidbody2D>();
        enemyRigidbodies.Remove(enemyRB);
    }

    void DragEnemiesTowardsCenter(){

        if(!enemyRigidbodies.Any()) return;

        print("billions must suck...");
        Vector2 directionToPull;
        Vector2 suctionCenter = transform.position;
        foreach(Rigidbody2D rb2d in enemyRigidbodies){
            directionToPull = (suctionCenter - rb2d.position).normalized;
            rb2d.AddForce(pullForce * directionToPull);
        }
    }
}
