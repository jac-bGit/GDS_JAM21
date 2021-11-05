/*
Simple audio manager component based on Brackes audio manager.
Can register and play sounds.

TODO: enhance sound settings with editor tools 
*/

using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManagerComponent : MonoBehaviour
{
    [SerializeField] protected Sound[] m_aSounds;

    //------------------------------------------------------
    // Mono functions 
    //------------------------------------------------------

    /// <summary>
    /// Create audio sources for sounds on object awake
    /// <summary>
    void Awake()
    {
        foreach (Sound sound in m_aSounds)
        {
            // Add source of sound
            sound.m_AudioSource = gameObject.AddComponent<AudioSource>();

            // Setup sound properties 
            sound.m_AudioSource.clip = sound.m_AudioClip;
            sound.m_AudioSource.volume = sound.m_fVolume;
            sound.m_AudioSource.pitch = sound.m_fPitch;
        }
    }

    //------------------------------------------------------
    // Public functions 
    //------------------------------------------------------

    /// <summary>
    /// Find and play sound by name id 
    /// <summary>
    public void PlaySound(string name)
    {   
        // Find sound in sounds array 
        Sound sound = Array.Find(m_aSounds, s => s.m_sName == name);
        
        // Sound check 
        if (sound == null)
            return;

        // Play 
        sound.m_AudioSource.Play();
    }
}

//------------------------------------------------------
[Serializable]
public class Sound
{
    // Basics 
    public string m_sName;
    public AudioClip m_AudioClip;

    // Sound properties

    [Range(0.0f, 1.0f)]
    public float m_fVolume = 1.0f;

    [Range(0.1f, 3.0f)]
    public float m_fPitch = 1.0f;

    // Components 
    [HideInInspector] public AudioSource m_AudioSource;
}