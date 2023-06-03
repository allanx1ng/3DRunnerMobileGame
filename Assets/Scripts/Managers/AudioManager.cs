using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private List<AudioSource> audioSources;
    private Dictionary<string, AudioClip> audioClips;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create AudioSources
        audioSources = new List<AudioSource>();
        for (int i = 0; i < 7; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.volume = 0.3f;
            audioSources.Add(source);
        }

        // Load audio clips
        audioClips = new Dictionary<string, AudioClip>();
        LoadAudioClip("Block Hit");
        LoadAudioClip("Block Break");
        LoadAudioClip("Mob Hit");
        LoadAudioClip("Player Hit");
    }

    private void LoadAudioClip(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>(clipName);
        if (clip == null)
        {
            Debug.LogError("Failed to load audio clip: " + clipName);
        }
        else
        {
            audioClips[clipName] = clip;
        }
    }

    public void PlaySound(string soundName)
    {
        if (audioClips.TryGetValue(soundName, out AudioClip clip))
        {
            // Find a free AudioSource (not currently playing anything)
            foreach (AudioSource source in audioSources)
            {
                if (!source.isPlaying)
                {
                    source.clip = clip;
                    source.Play();
                    return;
                }
            }

            // If all AudioSources are busy, do nothing
            Debug.Log("All AudioSources are busy, cannot play sound: " + soundName);
        }
        else
        {
            Debug.LogError("No audio clip with name: " + soundName);
        }
    }
}
