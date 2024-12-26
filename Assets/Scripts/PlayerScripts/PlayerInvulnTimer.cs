using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvulnTimer : MonoBehaviour
{
    float _invulnTimer = 2f;
    bool hurtInvuln = false;
    bool dashInvuln = false;

    void OnEnable(){
        PlayerHealth.onPlayerDamaged += TickInvulnTimerForHurt;
        PlayerDash.onDashTriggered += TickInvulnTimerForDash;
    }   

    void OnDisable(){
        PlayerHealth.onPlayerDamaged -= TickInvulnTimerForHurt;
        PlayerDash.onDashTriggered -= TickInvulnTimerForDash;
    }

    IEnumerator TickInvulnTimerForHurt(UnitInfoSO playerInfo){
        playerInfo.isInvuln = true;
        yield return new WaitForSeconds(_invulnTimer);
        playerInfo.isInvuln = false;
    }

    IEnumerator TickInvulnTimerForDash(UnitInfoSO playerInfo, float dashDuration){
        if (hurtInvuln) yield break;
        playerInfo.isInvuln = true;
        yield return new WaitForSeconds(dashDuration);
        playerInfo.isInvuln = false;
    }
    

}
