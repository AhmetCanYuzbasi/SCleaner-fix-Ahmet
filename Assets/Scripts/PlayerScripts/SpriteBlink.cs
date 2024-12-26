using System.Collections;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void OnEnable(){
        PlayerHealth.onPlayerHit += Blink;
        
    }

    void OnDisable(){
        PlayerHealth.onPlayerHit -= Blink;
    }
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator Blink(){
        int currentBlinkCount = 0;
        while (currentBlinkCount < 4){
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.enabled = true;
            ++currentBlinkCount;
            yield return new WaitForSeconds(0.25f);
        }
    }

}
