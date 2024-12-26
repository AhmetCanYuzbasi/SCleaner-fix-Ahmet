using System.Security;
using UnityEngine;

public class BugHealth : MonoBehaviour, IDamageable, IHealth
{
    private UnitInfoSO bugStats;

    DamageFlash _damageFlash;

    void Awake(){
        if (bugStats == null){
            bugStats = ScriptableObject.CreateInstance<UnitInfoSO>();
        }
        if (_damageFlash == null){
            _damageFlash = GetComponent<DamageFlash>();
        }
    }
    void Start()
    {
        bugStats.health = bugStats.maxHealth;
    }

    public void TakeDamage(int amount){
        _damageFlash.TriggerDamageFlash();
        bugStats.health -= amount;
        Debug.Log("Current health " + bugStats.health);
        if(bugStats.health <= 0){
            
            Destroy(gameObject);  
        }
    }

    public void TakeDamage(IEnemy attacker){
        
    }

}
