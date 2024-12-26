using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealth, ISetup
{
    public UnitInfoSO playerInfo;

    //Event for invoking PlayerInvulnTimer script for i-frames
    //when player takes damage
    public delegate IEnumerator OnPlayerDamaged(UnitInfoSO info);
    public static OnPlayerDamaged onPlayerDamaged;

    //Event for invoking SpriteBlink script for sprite blinking during i-frames
    //when player takes damage (referred to as player being hit)
    
    public delegate IEnumerator OnPlayerHitInvulnEventHandler();
    public static OnPlayerHitInvulnEventHandler onPlayerHitInvuln;


    //Event for invoking player death related scripts like animations, sounds etc.
    //when player's health hits 0

    public delegate void OnPlayerDeath();
    public static OnPlayerDeath onPlayerDeath;

    void OnEnable(){
        PlayerHitbox.onPlayerHitTakeDamage += TakeDamage;   
    }

    void OnDisable(){
        PlayerHitbox.onPlayerHitTakeDamage -= TakeDamage;
    }


    public void Init(UnitInfoSO info){
        playerInfo = info;
    }

    public void TakeDamage(IEnemy attacker){
        /* onPlayerDamaged?.Invoke(playerStats.isInvuln);
        playerStats.health -= 5;
        if (playerStats.health <= 0){
            playerStats.isAlive = false;
        } */
    }

    public void TakeDamage(int amount){

        if (playerInfo.isInvuln) return;
        if (!playerInfo.isAlive) return;

        playerInfo.health -= amount;
        if (playerInfo.health <= 0){
            playerInfo.isAlive = false;
            GetComponent<SpriteRenderer>().sprite = playerInfo.DefeatSprite;
            onPlayerDeath?.Invoke();
            PlayPlayerSounds.PlayAudio(playerInfo.OnDefeatSFX);
            return;
        }
        PlayPlayerSounds.PlayAudio(playerInfo.OnHitSFX);
        //coroutine for invuln event
        StartCoroutine(onPlayerDamaged?.Invoke(playerInfo));
        //coroutine for sprite blink
        StartCoroutine(onPlayerHitInvuln?.Invoke());
    }
}
