using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvulnTimer : MonoBehaviour
{
    public UnitInfoSO dummy;
    float _invulnTimer = 2f;

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
        if (playerInfo.isInvuln) yield break;
        playerInfo.isInvuln = true;
        yield return new WaitForSeconds(dashDuration);
        playerInfo.isInvuln = false;
    }
    

}
