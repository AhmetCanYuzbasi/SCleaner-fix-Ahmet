using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterPistolScript : MonoBehaviour, IWeapon
{

    public WeaponSO WeaponInfo { get; set;}
    private BulletSO bulletInfo;
    [SerializeField] Transform firingPoint;
    [SerializeField] Animator _animator;
    [SerializeField] RuntimeAnimatorController _runtimeAnimatorController;

    void Awake(){
        if (WeaponInfo == null){
            Debug.Log("Pistol weapon info missing. Loading resource.");
            WeaponInfo = Resources.Load<WeaponSO>("ScriptableObjects/PistolSO");
        }
         
        if(WeaponInfo.bulletInfo == null){
            Debug.Log("Pistol bullet info (part of pistol weapon info) missing. Loading pistol bullet info resource.");
            bulletInfo = Resources.Load<BulletSO>("ScriptableObjects/PistolBulletSO");
        }
        else {
            Debug.Log("Pistol bullet info (part of pistol bullet info) already exists.");
            bulletInfo = WeaponInfo.bulletInfo;
        }

        if (firingPoint == null){
            Debug.Log("FiringPoint not assigned. Finding...");
            firingPoint = transform.Find("FiringPoint");    
        }

        if (!_animator){
            _animator = GetComponent<Animator>();
        }
        
        if (_animator.runtimeAnimatorController){
            print("Runtime animator controller is null, fetching.");
            _runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AnimatorControllers/Pistol_AC");
            _animator.runtimeAnimatorController = _runtimeAnimatorController;
        }
        WeaponInfo.Init();
    }

    public void ReceiveFiringPoint(Transform firingPoint){
        this.firingPoint = firingPoint;
    }


    public void PrimaryAttack(){
        
        if (!HasAmmo() || WeaponInfo.isReloading) return;
        
        Bullet instantiatedBullet = Instantiate(WeaponInfo.bulletPrefab, firingPoint.transform.position, transform.rotation).GetComponent<Bullet>();
        instantiatedBullet.SetupBulletParameters(bulletInfo.projectileSpeed, bulletInfo.size, WeaponInfo.damage, bulletInfo.lifeTime);
        --WeaponInfo.currentAmmo;
    }

    public void SecondaryAttack(){
        //no op
    }
    public bool HasAmmo(){
        if (WeaponInfo.currentAmmo > 0){
            return true;
        }
        return false;
    }

    public void Reload()
    {
        int ammoBeforeReload = WeaponInfo.currentAmmo;
        WeaponInfo.currentAmmo = 0;
        WeaponInfo.isReloading = true;
        
        if(WeaponInfo.roundCapacity - ammoBeforeReload <= WeaponInfo.currentReserveAmmo){
            WeaponInfo.currentReserveAmmo -= WeaponInfo.roundCapacity - ammoBeforeReload;
            WeaponInfo.currentAmmo = WeaponInfo.roundCapacity;
        }
        else{
            WeaponInfo.currentAmmo += WeaponInfo.currentReserveAmmo;
            WeaponInfo.currentReserveAmmo = 0;
        }

        WeaponInfo.isReloading = false;
    }

    public void HandlePrimaryAttackInput()
    {
        if(!HasAmmo() || WeaponInfo.isReloading || WeaponInfo.isFiring) return;

        WeaponInfo.isFiring = true;
        _animator.SetBool("isFiring", true);
    }

    public void HandlePrimaryAttackInputCancel(){
        WeaponInfo.isFiring = false;
        _animator.SetBool("isFiring", false);
    }

    public void HandleReloadStart(){

        if(WeaponInfo.currentReserveAmmo == 0 || WeaponInfo.currentAmmo == WeaponInfo.roundCapacity || WeaponInfo.isReloading) 
        return;

        WeaponInfo.isFiring = false;
        
        _animator.SetBool("isFiring", WeaponInfo.isFiring);
        WeaponInfo.isReloading = true;
        _animator.SetTrigger("ReloadTrigger");
    }

    public void HandleReloadEnd(){
        WeaponInfo.isReloading = false;
    }

    public void HandleFiringAnimationEnd()
    {
        //no op
    }

    public void HandleSecondaryAttackInput()
    {
        throw new System.NotImplementedException();
    }

    public void HandleSecondaryAttackInputCancel()
    {
        throw new System.NotImplementedException();
    }

    void IWeapon.HandleSecondaryAttackInputCancel()
    {
        throw new System.NotImplementedException();
    }
    public void ResetWeaponState(){
        WeaponInfo.isFiring = false;
        WeaponInfo.isReloading = false;
        _animator.SetBool("isFiring", false);
        _animator.ResetTrigger("ReloadTrigger");
        //_animator.Play("Idle");
        _animator.SetTrigger("WeaponSwitchTrigger");
        GetComponent<SpriteRenderer>().sprite = WeaponInfo.sprite;
        
    }
}
