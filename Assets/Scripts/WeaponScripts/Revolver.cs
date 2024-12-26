//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Revolver : MonoBehaviour, IWeapon
{
    public WeaponSO WeaponInfo { get; set;}
    [SerializeField] BulletSO bulletInfo;
    [SerializeField] Transform firingPoint;
    [SerializeField] Animator _animator;
    bool isHoldingTrigger = false;

    void Awake(){
        if (WeaponInfo == null){
            Debug.Log("Pistol weapon info missing. Loading resource.");
            WeaponInfo = Resources.Load<WeaponSO>("ScriptableObjects/RevolverSO");
        }
        print(WeaponInfo);

        if(WeaponInfo.bulletInfo == null){
            Debug.Log("Pistol bullet info (part of pistol weapon info) missing. Loading pistol bullet info resource.");
            bulletInfo = Resources.Load<BulletSO>("ScriptableObjects/RevolverBulletSO");
            //WeaponInfo.bulletInfo = bulletInfo;
        }
        else {
            Debug.Log("Pistol bullet info (part of pistol bullet info) already exists.");
            bulletInfo = WeaponInfo.bulletInfo;
        }

        if (firingPoint == null){
            Debug.Log("FiringPoint not assigned. Finding...");
            print(firingPoint.transform.position. x + "    " + firingPoint.transform.position.y);
        }
    }

    void Start(){
        if (!_animator){
            _animator = GetComponent<Animator>();
        }
        WeaponInfo.lastFireTime = WeaponInfo.fireRate;
        //PlayWeaponSounds.ReceiveAudioSource(GetComponent<AudioSource>());
    }

    void Update(){

        if (!HasAmmo() && WeaponInfo.currentReserveAmmo != 0 && !WeaponInfo.isReloading){
            HandlePrimaryAttackInputCancel();
            HandleReloadStart();
        }
    }

    public void PrimaryAttack(){

        Bullet instantiatedBullet = Instantiate(WeaponInfo.bulletPrefab, firingPoint.transform.position, transform.rotation).GetComponent<Bullet>();
        instantiatedBullet.SetupBulletParameters(bulletInfo.projectileSpeed, bulletInfo.size, WeaponInfo.damage, bulletInfo.lifeTime);
        --WeaponInfo.currentAmmo;
        
    }

    public void SecondaryAttack(){
        //no op
    }

    public bool CanFire(){
        return WeaponInfo.fireRate <= Time.time - WeaponInfo.lastFireTime;
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

        if( WeaponInfo.ammoInClip - ammoBeforeReload <= WeaponInfo.currentReserveAmmo){
            WeaponInfo.currentReserveAmmo -= WeaponInfo.ammoInClip - ammoBeforeReload;
            WeaponInfo.currentAmmo = WeaponInfo.ammoInClip;
        }
        else {
            WeaponInfo.currentAmmo += WeaponInfo.currentReserveAmmo;
            WeaponInfo.currentReserveAmmo = 0;
        }
        WeaponInfo.isReloading = false;
    }


    public void HandlePrimaryAttackInput()
    {
        if(!HasAmmo() || WeaponInfo.isReloading || isHoldingTrigger) return;
        isHoldingTrigger = true;
        _animator.SetBool("isTriggerHeld", true);
    }

    public void HandlePrimaryAttackInputCancel(){
        WeaponInfo.isFiring = false;
        isHoldingTrigger = false;
        _animator.SetBool("isTriggerHeld", false);
        _animator.SetBool("isFiring", false);
    }

    public void HandleTriggerHoldEnd(){
        WeaponInfo.isFiring = true;
        _animator.SetTrigger("FireTrigger");
        _animator.SetBool("isFiring", true);
    }

    public void HandleFiringAnimationEnd()
    {
        isHoldingTrigger = false;
        WeaponInfo.isFiring = false;
        _animator.SetBool("isTriggerHeld", false);
        _animator.SetBool("isFiring", false);
        
    }

    public void HandleReloadStart()
    {
        if(WeaponInfo.currentReserveAmmo == 0 || WeaponInfo.currentAmmo == WeaponInfo.ammoInClip || WeaponInfo.isReloading) 
        return;
        
        if(WeaponInfo.isFiring) WeaponInfo.isFiring = false;
        
        WeaponInfo.isReloading = true;
        _animator.SetBool("isFiring", false);
        _animator.ResetTrigger("FireTrigger");
        _animator.SetTrigger("ReloadTrigger");
    }

    public void HandleReloadEnd()
    {
        Reload();
        
    }
}
