using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayGunSounds : MonoBehaviour
{
    AudioSource source;
    void OnEnable(){
        WeaponManager.onLeftMouseButtonClicked += PlayGunSound;
    }

    void OnDisable(){
        WeaponManager.onLeftMouseButtonClicked -= PlayGunSound;
    }

    void Start(){
        source = GetComponent<AudioSource>();
    }

    void PlayGunSound(AudioClip gunSound){

        source.clip = gunSound;
        if (!source.isPlaying){
            source.Play();
        }
        //source.PlayOneShot(gunSound);
    }

}
