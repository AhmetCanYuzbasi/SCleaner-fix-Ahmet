using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipmentManager : MonoBehaviour
{
    [SerializeField] GameObject _equipmentPrefab;
    [SerializeField] Transform _grenadeSpawnPoint;
    int _equipmentCount = 3;
    IEquipment _equipment;
    

    void Awake(){
        if(!_equipmentPrefab)
          _equipmentPrefab = Instantiate(Resources.Load("WeaponPrefabs/GrenadePrefab") as GameObject);
        else
            _equipmentPrefab = Instantiate(_equipmentPrefab);

        if(!_grenadeSpawnPoint)
        _grenadeSpawnPoint = GameObject.FindGameObjectWithTag("GrenadeSpawnPoint").transform;
            
    }
    void Start()
    {
        _equipment = _equipmentPrefab.GetComponent<IEquipment>();
        _equipmentPrefab.transform.parent = transform;
        _equipmentPrefab.transform.position = _grenadeSpawnPoint.position;
        _equipmentPrefab.SetActive(false);
    }

    public void HandleEquipmentInput(InputAction.CallbackContext ctx){
        if(ctx.performed){
            _equipmentPrefab.SetActive(true);
            _equipment.SetupEquipmentParameters(transform.localRotation, transform.right);
            _equipmentPrefab.transform.parent = null;
            _equipment.UseEquipment();
        }
    }

}
