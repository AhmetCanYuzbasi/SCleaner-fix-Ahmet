using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Callbacks;
using UnityEngine;

public class Grenade : MonoBehaviour, IEquipment
{
    public static event Action OnAbilityTrigger;
    
    float _throwTime;
    float _lifeTime = 10f;
    Vector2 _trajectory;
    Rigidbody2D _rb2D;
    BoxCollider2D _boxCollider2D;
    public float _speed = 10000f;
    
    void Awake(){
        _rb2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnEnable(){
        _throwTime = Time.time;
    }

    void Start()
    {
        
    }

    void Update(){
        CheckLifeTme();
        if(_rb2D.velocity.magnitude < 0.05f){
            _rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            _boxCollider2D.enabled = false;
            TriggerAbility();
        }
    }

    void FixedUpdate(){
        _rb2D.AddForce(-_rb2D.velocity.normalized * 1f);
        if(0.0005f < _rb2D.velocity.magnitude)
        _rb2D.AddTorque(1f);
    }

    public void SetupEquipmentParameters(Quaternion rotation, Vector2 direction){
        transform.rotation = rotation;
        _trajectory = direction;
    }

    public void UseEquipment(){
        _rb2D.AddForce(_trajectory * _speed, ForceMode2D.Impulse);
    }
    public void TriggerAbility(){
        OnAbilityTrigger?.Invoke();
    }

    public IEnumerator TriggerAbilityAfterTime(){
        yield return new WaitForSeconds(3f);
    }

    void CheckLifeTme(){
        if (_lifeTime < Time.time - _throwTime){

            gameObject.SetActive(false);
        }
    }

}
