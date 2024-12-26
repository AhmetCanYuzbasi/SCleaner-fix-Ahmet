using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
   
   bool isTriggered = false;

    void OnTriggerExit2D(Collider2D collision){
        // collision.gameObject.CompareTag("Player") && 
        if (collision.transform.position.y - transform.position.y < 0.001f && !isTriggered){
            isTriggered = true;
            EnemySpawnManager.instance.StartCoroutine(EnemySpawnManager.instance.SpawnEnemies());
        }

    }
}
