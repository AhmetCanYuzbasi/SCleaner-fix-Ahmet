using System;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IWeapon
{
    public WeaponSO WeaponInfo { get; set; }
    private BulletSO bulletInfo;
    [SerializeField] private Transform firingPoint;
    List<Bullet> bullets = new List<Bullet>();
    bool _hasReloadStarted;
    [SerializeField] Animator _animator;
    [SerializeField] RuntimeAnimatorController _runtimeAnimatorController;
    
    

    void OnEnable()
    {
        Bullet.OnBulletDestroyed += HandleBulletDestroyed;
    }

    void OnDisable()
    {
        Bullet.OnBulletDestroyed -= HandleBulletDestroyed;
    }

    void HandleBulletDestroyed(Bullet bullet)
    {
        bullets.Remove(bullet);
    }

    void Awake(){
        if (WeaponInfo == null){
            Debug.Log("Shotgun weapon info missing. Loading resource.");
            WeaponInfo = Resources.Load<WeaponSO>("ScriptableObjects/ShotgunSO");
        }
        
        if(WeaponInfo.bulletInfo == null){
            Debug.Log("Shotgun bullet info (part of shotgun weapon info) missing. Loading shotgun bullet info resource.");
            bulletInfo = Resources.Load<BulletSO>("ScriptableObjects/ShotgunBulletInfo");
            //WeaponInfo.bulletInfo = bulletInfo;
        }
        else {
            Debug.Log("Shotgun bullet info (part of shotgun weapon info) already exists.");
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
            _runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AnimatorControllers/Shotgun_AC");
            _animator.runtimeAnimatorController = _runtimeAnimatorController;
        }
        WeaponInfo.Init();
    }

    public void ReceiveFiringPoint(Transform firingPoint){
        this.firingPoint = firingPoint;
    }

    public void PrimaryAttack()
    {
        
        int k = 1;
        for (int i = 1; i <= WeaponInfo.pelletCount; ++i){
            Bullet bullet;
            float offset;

            if (i == 1){
            bullet = Instantiate(WeaponInfo.bulletPrefab, firingPoint.transform.position, transform.rotation).GetComponent<Bullet>();
            }

            else if (i <= ( (WeaponInfo.pelletCount + 1)/2) ){
            offset = (i-1) * 5f;
            bullet = Instantiate(WeaponInfo.bulletPrefab, firingPoint.transform.position, Quaternion.Euler(0, 0, firingPoint.rotation.eulerAngles.z + offset)).GetComponent<Bullet>();
            } 
            
            else{
            offset = (WeaponInfo.pelletCount - i + 1) * 5f;
            bullet = Instantiate(WeaponInfo.bulletPrefab, firingPoint.transform.position, Quaternion.Euler(0, 0, firingPoint.rotation.eulerAngles.z - offset)).GetComponent<Bullet>();
            ++k;
            }

            bullet.enabled = false;
            if (bullet != null){
                Debug.Log(bulletInfo.lifeTime);
                bullet.SetupBulletParameters(bulletInfo.projectileSpeed, bulletInfo.size, WeaponInfo.damage, bulletInfo.lifeTime);
                bullets.Add(bullet);
            }  
        } 

        foreach(Bullet bullet in bullets){
            bullet.enabled = true;
        } 

        
    }

    public void SecondaryAttack()
    {
        //no op
    }

    
    public bool HasAmmo(){
        if (WeaponInfo.currentAmmo == 0){
            return false;
        }
        return true;
    }

    public void Reload()
    {   
        
        ++WeaponInfo.currentAmmo;
        --WeaponInfo.currentReserveAmmo;
        
        if(WeaponInfo.currentAmmo < WeaponInfo.roundCapacity){
            _animator.SetTrigger("ReloadLoopTrigger");
            return;
        }
        HandleReloadEnd();
    }

    public void HandlePrimaryAttackInput(){

        if(!HasAmmo()) return;
        
        if(_hasReloadStarted && HasAmmo() || WeaponInfo.isReloading && HasAmmo()){
            _hasReloadStarted = false;
            WeaponInfo.isReloading = false;
            _animator.SetBool("hasReloadStarted", false);
            _animator.ResetTrigger("ReloadLoopTrigger");
        } 
        
        //_animator.SetBool("hasReloaded", false);
        WeaponInfo.isFiring = true;
        _animator.SetBool("isFiring", true);
    }

    public void HandlePrimaryAttackInputCancel(){
        WeaponInfo.isFiring = false;
        _animator.SetBool("isFiring", false);
    }
    public void HandleFiringAnimationEnd(){

    }

    public void HandleReloadStart()
    {
        if(WeaponInfo.currentReserveAmmo == 0 || WeaponInfo.currentAmmo == WeaponInfo.roundCapacity || WeaponInfo.isReloading) 
        return;
        
        WeaponInfo.isFiring = false;
        _animator.SetBool("isFiring", false);

        _animator.SetBool("hasReloaded", false);
        _hasReloadStarted = true;
        _animator.SetBool("hasReloadStarted", true);
       
    }

    public void HandleReloadLoopStart(){

        if(!WeaponInfo.isReloading){
        _animator.SetBool("hasReloadStarted", false);
        WeaponInfo.isReloading = true;
        _animator.SetBool("isReloading", true);
        }

    }

    public void HandleReloadStartEnd(){
        _animator.SetBool("hasReloadStarted", false);
    }

    public void HandleReloadEnd()
    {
        WeaponInfo.isReloading = false;
        _animator.SetBool("isReloading", false);
        _animator.SetBool("hasReloaded", true);
    }

    public void HandleSecondaryAttackInput()
    {
        throw new NotImplementedException();
    }

    public void HandleSecondaryAttackInputCancel()
    {
        throw new NotImplementedException();
    }

    public void ResetWeaponState(){
        WeaponInfo.isFiring = false;
        WeaponInfo.isReloading = false;
        _animator.SetBool("isFiring", false);
        _animator.SetBool("isReloading", false);
        _animator.SetBool("hasReloadStarted", false);
        _animator.SetBool("hasReloaded", false);
        _animator.ResetTrigger("ReloadTrigger");
        _animator.ResetTrigger("ReloadLoopTrigger");
        _animator.ResetTrigger("ReloadCancelTrigger");
        //_animator.Play("Idle");
        _animator.SetTrigger("WeaponSwitchTrigger");
        GetComponent<SpriteRenderer>().sprite = WeaponInfo.sprite;
        
    }
}
