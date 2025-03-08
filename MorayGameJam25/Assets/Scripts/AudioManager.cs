using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource spaceAmbience;
    void Start()
    {
        if (!spaceAmbience.isPlaying) 
        {
            spaceAmbience.Play();
        }
    }
}
