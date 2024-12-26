//using TreeEditor;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{    
    IEnemy script;
    
    void Start()
    {
        script = GetComponentInParent<IEnemy>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            //script.PlayerIsDetected();
            script.SpecialAbility(other.GetComponent<PlayerMovement>());
            //splashSlime.Splash();
        } 
    }

     void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            //script.PlayerIsOutOfChaseRange();
        } 
        
    } 
}
