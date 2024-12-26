using UnityEngine;
using UnityEngine.InputSystem;

    //This class handles input related to the weapon functionality
    //such as shooting, reloading. It only takes the input and
    //relegates the actual functionality to the weapon itself

public class WeaponManager : MonoBehaviour, IDisable
{
    public delegate void OnLeftMouseButtonClick(AudioClip gunSound);
    public static event OnLeftMouseButtonClick onLeftMouseButtonClicked;
    InputAction _primaryAttackAction;
    [SerializeField] Transform weaponPosition;
    [SerializeField] GameObject weaponPrefab;
    bool _isMouseHeld;
    IWeapon currentWeapon;
    

    void OnEnable(){
        PlayerHealth.onPlayerDeath += DisableScript;
    }

    void OnDisable(){
        PlayerHealth.onPlayerDeath -= DisableScript;
    }

    void Awake(){

        if (weaponPrefab == null)
            weaponPrefab = Instantiate(Resources.Load("WeaponPrefabs/PistolPrefab")) as GameObject;

        else 
            weaponPrefab = Instantiate(weaponPrefab);
        
    }

    void Start(){
        
        currentWeapon = weaponPrefab.GetComponent<IWeapon>();
        weaponPrefab.transform.parent = transform;
        weaponPrefab.transform.position = weaponPosition.position;
        weaponPrefab.transform.localPosition += currentWeapon.WeaponInfo.offset;

    }   


    public void Update(){

        /*if(_primaryAttackAction.IsPressed() && currentWeapon.HasAmmo() && !currentWeapon.WeaponInfo.isReloading && !currentWeapon.WeaponInfo.isFiring){
            currentWeapon.HandlePrimaryAttackInput();
        }*/

        if(_primaryAttackAction.IsPressed()){
            _isMouseHeld = true;
            currentWeapon.HandlePrimaryAttackInput();
        } 
        else if (_isMouseHeld && !_primaryAttackAction.IsPressed()){
            _isMouseHeld = false;
            currentWeapon.HandlePrimaryAttackInputCancel();
        }
        
        /*else if(currentWeapon.WeaponInfo.isFiring && (!_primaryAttackAction.IsPressed() || currentWeapon.WeaponInfo.isReloading)){
            currentWeapon.HandlePrimaryAttackInputCancel();
        }*/
            
    }

    public void HandleSecondaryAttackInput(){
        currentWeapon.SecondaryAttack();
    }


    public void DisableScript()
    {
        _primaryAttackAction.Disable();
        enabled = false;
    }

    
    public void WeaponSwap(IWeapon weapon){
            currentWeapon = weapon;
            //currentWeapon = weapons[1];
    }

    public void TriggerReload(InputAction.CallbackContext ctx){
        if (ctx.performed){
            currentWeapon.HandleReloadStart();
        }
    }

    public void ReceivePrimaryAttackInputAction(InputAction action){
        _primaryAttackAction = action;
    }

    
}