using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMain : MonoBehaviour
{
    PlayerInput playerInput;
    InputActionAsset playerActionAsset;
    InputActionMap playerActionMap;
    public UnitInfoSO playerInfo;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    PlayerDash playerDash;
    WeaponManager weaponManager;

    void Awake(){
        playerInput = GetComponent<PlayerInput>();
        playerActionAsset = playerInput.actions;
        playerActionMap = playerActionAsset.FindActionMap("Gameplay");
        playerActionMap.Enable();

        if (playerInfo == null){
            Debug.Log("Player info is null, creating");
            playerInfo = ScriptableObject.CreateInstance<UnitInfoSO>();
        }
        playerInfo.Init();

        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        playerDash = GetComponent<PlayerDash>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        
        playerHealth.Init(playerInfo);

        playerMovement.Init(playerInfo);
        playerMovement.ReceiveInputAction(playerActionMap.FindAction("Movement"));

        playerDash.Init(playerInfo);

        weaponManager.ReceivePrimaryAttackInputAction(playerActionMap.FindAction("Shooting"));
        weaponManager.ReceiveSecondaryAttackInputAction(playerActionMap.FindAction("Secondary Shooting"));


    }

    
   
}
