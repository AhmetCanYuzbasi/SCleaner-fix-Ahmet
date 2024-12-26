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
    
    public delegate IEnumerator OnPlayerHit();
    public static OnPlayerHit onPlayerHit;


    //Event for invoking player death related scripts like animations, sounds etc.
    //when player's health hits 0

    public delegate void OnPlayerDeath();
    public static OnPlayerDeath onPlayerDeath;

    void OnEnable(){
        PlayerHitbox.onPlayerHit += TakeDamage;   
    }

    void OnDisable(){
        PlayerHitbox.onPlayerHit -= TakeDamage;
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

        playerInfo.health -= 5;
        if (playerInfo.health <= 0){
            playerInfo.isAlive = false;
            onPlayerDeath?.Invoke();
            return;
        }
        onPlayerHit?.Invoke();
        StartCoroutine(onPlayerDamaged?.Invoke(playerInfo));
        StartCoroutine(onPlayerHit?.Invoke());
    }
}
