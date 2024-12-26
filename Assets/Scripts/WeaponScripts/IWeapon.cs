using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    WeaponSO WeaponInfo{get;set;}
    public void Reload();
    public void HandlePrimaryAttackInput();
    public void HandlePrimaryAttackInputCancel();
    public void HandleSecondaryAttackInput();
    public void HandleSecondaryAttackInputCancel();
    public void HandleFiringAnimationEnd();
    public void HandleReloadStart();
    public void HandleReloadEnd();
    public void PrimaryAttack();
    public void SecondaryAttack();
    public bool HasAmmo();
    public void ReceiveFiringPoint(Transform firingPoint);
    public void ResetWeaponState();
    
}
