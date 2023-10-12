using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(Sound sound, Vector3 position)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = sound.clip;
        audioSource.volume = sound.volume;
        audioSource.pitch = sound.pitch;
        audioSource.Play();

        Destroy(soundGameObject, sound.clip.length);
    }

    public void PlayMusic(Sound sound)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sound.clip;
        audioSource.volume = sound.volume;
        audioSource.pitch = sound.pitch;
        audioSource.loop = sound.loop;
        audioSource.Play();
    }

    public void StopAllAudio()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            source.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            source.volume = volume;
        }
    }
}