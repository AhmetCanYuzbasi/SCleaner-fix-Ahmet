using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMain : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] InputActionAsset playerActionAsset;
    [SerializeField] InputActionMap playerActionMap;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerDash playerDash;
    [SerializeField] PlayerAim playerAim;
    public UnitInfoSO playerInfo;
    public WeaponManager weaponManager;


    void OnEnable(){
        PlayerHealth.onPlayerDeath += DisableInput;
        PauseMenu.OnGamePaused += DisableInput;
        PauseMenu.OnGameResumed += EnableInput;
    }

    void OnDisable(){
        PlayerHealth.onPlayerDeath -= DisableInput;
        PauseMenu.OnGamePaused -= DisableInput;
        PauseMenu.OnGameResumed -= EnableInput;
    }


    void Awake(){
        //playerInput = GetComponent<PlayerInput>();
        playerActionAsset = playerInput.actions;
        playerActionMap = playerActionAsset.FindActionMap("Gameplay");
        playerActionMap.Enable();

        if (playerInfo == null){
            Debug.Log("Player info is null, creating");
            //playerInfo = ScriptableObject.CreateInstance<UnitInfoSO>();
            playerInfo = Resources.Load<UnitInfoSO>("Scriptable Objects/PlayerInfo");
        }
        playerInfo.Init();

        /*playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        playerDash = GetComponent<PlayerDash>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        */
        
        playerHealth.Init(playerInfo);

        playerMovement.Init(playerInfo);
        playerMovement.ReceiveInputAction(playerActionMap.FindAction("Movement"));

        playerDash.Init(playerInfo);

        weaponManager.ReceivePrimaryAttackInputAction(playerActionMap.FindAction("Shooting"));


    }
    
    void DisableInput(){
        playerAim.enabled = false;
        playerActionMap.Disable();
    }

    void EnableInput(){
        playerAim.enabled = true;
        playerActionMap.Enable();
    }
       
}
