using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    [Header("Audio Clips")]
    public AudioClip BGM;

    [Header("UI SFX Clips")]
    public AudioClip selectSound;
    public AudioClip continueDialogueSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM()
    {
        PlayMusicOnLoop(BGM);
    }
    
    void Start()
    {
        LoadVolumeSettings();
        PlayBGM();
    }

    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    void ResetToDefaultVolumes()
    {
        PlayerPrefs.DeleteKey("MasterVolume");
        PlayerPrefs.DeleteKey("MusicVolume");
        PlayerPrefs.DeleteKey("SFXVolume");
    }

    void PlayMusicOnLoop(AudioClip clip)
    {
        if (clip == null || musicSource == null)
            return;

        if (musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.loop = true;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }

    public void PlaySelectSound()
    {
        if (sfxSource != null && selectSound != null)
        {
            sfxSource.PlayOneShot(selectSound, 1.0f);
        }
    }

    public void PlayContinueDialogueSound()
    {
        if (sfxSource != null && continueDialogueSound != null)
        {
            sfxSource.PlayOneShot(continueDialogueSound, 3.0f);
        }
    }

    public void PlayEndingTrack(AudioClip endingClip)
    {
        if (musicSource.isPlaying) musicSource.Stop();

        if (endingClip != null)
        {
            musicSource.loop = false;
            musicSource.clip = endingClip;
            musicSource.Play();
        }
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    
    void LoadVolumeSettings()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.7f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.7f);
        
        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }
}