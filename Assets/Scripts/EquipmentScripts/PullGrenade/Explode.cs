using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{   
    AudioSource _audioSource;
    AudioClip _explosionAudio;
    public event Action OnExplosionEndResetEvent;
    Transform _parentObjectTransform;
    int _damage = 10;
    public bool _explosionTriggered = false;
    [SerializeField] ParticleSystem _explosionParticles = default;
    void OnEnable(){
        _explosionTriggered = false;
        _parentObjectTransform = transform;
        //SuctionAbility.OnAbilityEndExplosion += StartExplosion;
    }

    void OnDisable(){
        //SuctionAbility.OnAbilityEndExplosion -= StartExplosion;
        //_explosionParticles.transform.parent = _parentObjectTransform;
    }

    void Update(){
        if (_explosionTriggered && !_explosionParticles.IsAlive()){
            //gameObject.SetActive(false);
            OnExplosionEndResetEvent?.Invoke();
            _explosionTriggered = false;
            //enabled = false;
        }
    }
    
    public void StartExplosion(List<IDamageable> enemies){
        print("explosion started");
        _explosionTriggered = true;
        //_explosionParticles.transform.parent = null;
        _explosionParticles.Play();
        PlayWeaponSounds.ReceiveAudioSource(_audioSource);
        PlayWeaponSounds.PlayGunSound(_explosionAudio);
        
        for(int i = 0; i < enemies.Count; ++i){
            enemies[i].TakeDamage(_damage);
        }
        //enabled = false;
        enemies.Clear();
    }

    public void StopExplosion(){
        if(_explosionParticles.isPlaying)
        _explosionParticles.Stop();
    }

    public IEnumerator Wait(){
        print("waiting");
        yield return new WaitForSeconds(.5f);
    }

    public void ReceiveAudioSource(AudioSource source){
        _audioSource = source;
    }
    public void ReceiveExplosionAudio(AudioClip clip){
        _explosionAudio = clip;
    }
}
