using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source)
    {
        audioSource = source;
        audioSource.volume = 0.5f;
        audioClips.Add(AudioClipName.Explosion, 
            Resources.Load<AudioClip>("explosion"));
        audioClips.Add(AudioClipName.PlayerDeath,
            Resources.Load<AudioClip>("die"));
        audioClips.Add(AudioClipName.PlayerShot,
            Resources.Load<AudioClip>("shoot"));
        audioClips.Add(AudioClipName.Background,
            Resources.Load<AudioClip>("background"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

    /// <summary>
    /// Stops the audio source
    /// </summary>
    public static void Stop()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// Sets the volume level in percentage (50 by default)
    /// </summary>
    /// <param name="volume">volume level from 0 to 100</param>
    public static void SetVolumeLevel(int volume)
    {
        audioSource.volume = (float)volume / 100.0f;
    }
}
