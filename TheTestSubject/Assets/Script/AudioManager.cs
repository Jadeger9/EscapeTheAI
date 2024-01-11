using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script requires an AudioSource component to work
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _shortAudioSource;
    [SerializeField] private AudioSource _longAudioSource;
    [SerializeField] private AudioSource _backgroundSource;
    public static AudioManager Instance;

    // Fires after object has been initialized
    private void Awake()
    {
        if (Instance == null)
        {
            // If no instance exists, set this as the instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this GameObject
            Destroy(gameObject);
        }
    }

    // Plays given sound file
    public void PlayShortSound(string soundName)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(soundName);
        if (audioClip == null)
        {
            Debug.Log("AudioFile not found");
            return;
        }
        _shortAudioSource.PlayOneShot(audioClip, 1);
    }

    // Plays given sound file
    public void PlaySound(string soundName, float lowerPitch = 1.0f, float higherPitch = 1.0f)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(soundName);
        if (audioClip == null)
        {
            Debug.Log("AudioFile not found");
            return;
        }

        // Adjusts pitch randomly
        float pitchModifier = Random.Range(lowerPitch, higherPitch);

        _shortAudioSource.pitch = pitchModifier;
        _shortAudioSource.PlayOneShot(audioClip, 1);
    }

    // Play a sound that can play over a longer time (only one at a time)
    public void PlayLongSound(string soundName)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(soundName);
        if (audioClip == null) return;

        _longAudioSource.clip = audioClip;
        _longAudioSource.Play();
    }

    // Play a sound that can play over a longer time (only one at a time)
    public void ChangeBackgrondMusic(string soundName)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(soundName);
        if (audioClip == null) return;

        _backgroundSource.clip = audioClip;
        _backgroundSource.Play();
    }

    // Plays one random sound out of a given array
    public void PlayRandomSound(string[] soundNames, float lowerPitch = 1.0f, float higherPitch = 1.0f)
    {
        int number = Random.Range(0, soundNames.Length);
        PlaySound(soundNames[number], lowerPitch, higherPitch);
    }
}
