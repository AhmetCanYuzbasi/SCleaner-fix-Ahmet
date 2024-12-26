using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuctionAbility : MonoBehaviour
{
    public static event Action<List<IDamageable>> OnAbilityEndExplosion;
    //public event Action OnAbilityStart;
    public float _activationTime;
    float _activationLifeTime = 3f;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _vacuumAudio;
    [SerializeField] CircleCollider2D _circleCollider2D;
    List<Rigidbody2D> _enemyRigidbodies = new List<Rigidbody2D>();
    List<IDamageable> _enemyDamageables = new List<IDamageable>();
    float _pullForce = .5f;
    public bool _isPulling = false;
    bool _hasActivated = false;
   
    void Update()
    {
        CheckActivatedLifeTime();
        if(!_isPulling)
        StartCoroutine(DragEnemiesTowardsCenter());
    }

    void Awake(){
        if(!_circleCollider2D)
            _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void OnEnable(){
        _activationTime = Time.time;
        _hasActivated = true;
        enabled = true;
        //OnAbilityStart?.Invoke();
    }

    void OnDisable(){
        enabled = false;
        _isPulling = false;
        //OnAbilityEndExplosion?.Invoke(_enemyDamageables);
        //_enemyRigidbodies.Clear();
        if(_audioSource)
        _audioSource.Stop();
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Enemy")){
            Rigidbody2D enemyRB = other.gameObject.GetComponent<Rigidbody2D>();
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            enemyRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            _enemyRigidbodies.Add(enemyRB);
            _enemyDamageables.Add(damageable);
        }
        
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Enemy")){
        Rigidbody2D enemyRB = other.gameObject.GetComponent<Rigidbody2D>();
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        enemyRB.constraints = RigidbodyConstraints2D.None;
        _enemyRigidbodies.Remove(enemyRB);
        _enemyDamageables.Remove(damageable);
        }
        
    }

    IEnumerator DragEnemiesTowardsCenter(){

        if(!_enemyRigidbodies.Any()) yield break;

        _isPulling = true;
        float distance;
        Vector2 individualPullForce;
        Vector2 direction;
        Vector2 suctionCenter = transform.position;


        for(int i = 0; i < _enemyRigidbodies.Count(); ++i){

            Rigidbody2D rb2d = _enemyRigidbodies.ElementAt(i);

            if(rb2d != null){
                direction = suctionCenter - rb2d.position;
                distance = direction.magnitude;
                direction.Normalize();

                distance = Mathf.Clamp(distance, 0.1f, _circleCollider2D.radius);

                if (distance < 2f){
                    rb2d.velocity = Vector2.zero;
                    //rb2d.position = Vector2.Lerp(rb2d.position, suctionCenter, 0.25f);
                    rb2d.position = suctionCenter;
                } else {
                    individualPullForce = direction * (_pullForce * distance);
                    rb2d.AddForce(individualPullForce, ForceMode2D.Impulse);
                }
            }
            
        }
        PlayWeaponSounds.ReceiveAudioSource(_audioSource);
        PlayWeaponSounds.PlayGunSound(_vacuumAudio);
        yield return new WaitForSeconds(0.1f);

        _isPulling = false;
    }

    void CheckActivatedLifeTime(){
        if(!_hasActivated) return;

        if(_activationLifeTime < Time.time - _activationTime){
            print("booooo");
            OnAbilityEndExplosion?.Invoke(_enemyDamageables);
            enabled = false;
        }
    }

    public void ReceiveAudioSource(AudioSource source){
        if(!_audioSource)
        _audioSource = source;
    }

    public void ReceiveVacuumAudio(AudioClip audioClip){
        if(!_vacuumAudio)
        _vacuumAudio = audioClip;
    }
}
