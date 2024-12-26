using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setAudio : MonoBehaviour
{
    public void SetAudio(float value)
    {
        AudioListener.volume = value;
    }
}
