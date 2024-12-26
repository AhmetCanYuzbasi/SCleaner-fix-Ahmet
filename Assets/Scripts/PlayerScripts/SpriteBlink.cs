using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    readonly int blinkCount = 5;

    void OnEnable(){
        PlayerHealth.onPlayerHitInvuln += Blink;
        
    }

    void OnDisable(){
        PlayerHealth.onPlayerHitInvuln -= Blink;
    }
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /* void Blink(){
        int currentBlinkCount = 0;
        float waitTimer = 0f;
        Debug.Log("blinking");
        while (currentBlinkCount < blinkCount){
            spriteRenderer.enabled = false;
            while (waitTimer < 20f){
                waitTimer += Time.deltaTime;
                Debug.Log(waitTimer);
            }
            spriteRenderer.enabled = true;
            ++currentBlinkCount;
            waitTimer = 0f;
        }
    }
    */

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
