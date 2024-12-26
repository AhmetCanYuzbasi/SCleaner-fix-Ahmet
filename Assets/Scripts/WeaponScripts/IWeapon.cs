using System;
using System.Collections;


//This interface defines the most common methods
//and properties a weapon should possess
//any new weapon MUST implement this interface 

public interface IWeapon 
{
    WeaponSO WeaponInfo{get;set;}
    public void Reload();
    public void HandlePrimaryAttackInput();
    public void HandlePrimaryAttackInputCancel();
    public void HandleFiringAnimationEnd();
    public void HandleReloadStart();
    public void HandleReloadEnd();
    public void PrimaryAttack();
    public void SecondaryAttack();
    public bool CanFire();
    public bool HasAmmo();
    
}
