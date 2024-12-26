using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionAttack : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D col){
        GameObject parentObject = null;
        if(col.CompareTag("PlayerHitbox") && col.transform.parent.gameObject != null){
            parentObject = col.transform.parent.gameObject;
        }
        print(col);
        IDamageable damageable = null;
        if (parentObject != null){
            damageable = parentObject.GetComponent<IDamageable>();
        }

        damageable?.TakeDamage(5); 
    }
}
