using UnityEngine;

public class DetectHits : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision2D){
        /*IDamageable damageable = collision2D.gameObject.GetComponent<IDamageable>();
        IEnemy enemy = collision2D.gameObject.GetComponent<IEnemy>();

        damageable?.TakeDamage(_damage);
        enemy?.SetProvoked();

        Destroy(gameObject);

        if (collision2D.gameObject.CompareTag("Hitbox")){
            print("hitbox hit");
        }*/

        Destroy(gameObject);
    }

    
}
