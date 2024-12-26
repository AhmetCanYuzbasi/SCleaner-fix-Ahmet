using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionAttack : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D collision){
        IDamageable health = collision.gameObject.GetComponent<IDamageable>();
        if (health != null){
            health.TakeDamage(5);
        }
    }
}
