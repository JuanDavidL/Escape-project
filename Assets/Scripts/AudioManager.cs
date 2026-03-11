using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource sfxAudioSource, musicAudioSource;
    [SerializeField][Range(0f,1f)] float musicVolume, sfxVolume;
    private Slider musicSlider, sfxSlider;

    private void Awake()
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

    private void Start() {
        musicVolume = musicAudioSource.volume;
        sfxVolume = sfxAudioSource.volume;

        musicSlider = GameObject.FindWithTag("musicSlider").GetComponent<Slider>();
        sfxSlider = GameObject.FindWithTag("sfxSlider").GetComponent<Slider>();

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }

 
    
    // ============================ sfx =============================

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxAudioSource == null)
        {
            Debug.LogError("AudioClip o AudioSource no asignados en P3_GenAudioManager when PlaySFX() was called.");
            return;
        } // protección simple contra nulls
        sfxAudioSource.Stop();
        sfxAudioSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip == null || sfxAudioSource == null)
        {
            Debug.LogError("AudioClip o AudioSource no asignados en P3_GenAudioManager when PlaySFX() was called.");
            return;
        } // protección simple contra nulls
        sfxAudioSource.Stop();
        sfxAudioSource.volume = volume;
        sfxAudioSource.PlayOneShot(clip);
    }

    public void PlaySFX(float delayTime, AudioClip clip)
    {
        if (clip == null || sfxAudioSource == null)
        {
            Debug.LogError("AudioClip o AudioSource no asignados en P3_GenAudioManager when PlaySFX() was called.");
            return;
        } // protección simple contra nulls

        float timer = 0;
        while (timer < delayTime)
        {
            timer += Time.deltaTime;
        }
        
        sfxAudioSource.Stop();
        sfxAudioSource.PlayOneShot(clip);
        
    }

    // ============================== music ================================

    public void PlayMusic()
    {
        if (musicAudioSource == null) 
        {
            Debug.LogError("AudioClip o AudioSource no asignados en AudioManager when PlayMusic() was called.");
            return;
        }
        musicAudioSource.Stop();
        musicAudioSource.Play();
        Debug.Log("Music has begun playing");

    }

    public void PlayMusic(AudioClip newMusic)
    {
        
        if (musicAudioSource == null) 
        {
            Debug.LogError("AudioClip o AudioSource no asignados en AudioManager when PlayMusic() was called.");
            return;
        } // protección simple contra nulls
        

        // Si se desea un cambio instantáneo: detener, asignar y reproducir.
        musicAudioSource.Stop();
        musicAudioSource.clip = newMusic;
        if (newMusic != null)
        {
            
            musicAudioSource.Play();
            Debug.Log("Music has begun playing");
        }
    }

    public void PlayMusic(AudioClip newMusic, float volume)
    {
        
        if (musicAudioSource == null) 
        {
            Debug.Log("AudioClip o AudioSource no asignados en AudioManager when PlayMusic() was called.");
            return;
        } // protección simple contra nulls
        

        // Si se desea un cambio instantáneo: detener, asignar y reproducir.
        musicAudioSource.Stop();
        musicAudioSource.clip = newMusic;
        if (newMusic != null)
        {
            Debug.LogError("AudioClip o AudioSource no asignados en AudioManager when PlayMusic() was called.");
            musicAudioSource.volume = volume;
            musicAudioSource.Play();
            Debug.Log("Music has begun playing");
        }
    }

    public void StopMusic()
    {
        if (musicAudioSource != null) 
        {
            musicAudioSource.Stop();
            Debug.Log("Music stopped");
            return;
        } 
        Debug.Log("Music was not playing");
        
    }

    public void PauseMusic()
    {
        if (musicAudioSource != null) 
        {
            musicAudioSource.Pause();
            Debug.Log("Music was Paused");
            return;
        } 
        Debug.Log("Music was not playing");
        
    }

    public void UnPauseMusic()
    {
        if (musicAudioSource != null) 
        {
            musicAudioSource.UnPause();
            Debug.Log("Music has been unpaused");
            return;
        } 
        Debug.Log("Music was not playing");
        
    }

    public void ChangeMusicVolume(float newVolume) {
        //if (newVolume < 1f) newVolume = 1f;
        //else if (newVolume < 0) newVolume = 0f;

        newVolume = newVolume > 1f ? 1 : newVolume < 0f ? 0 : newVolume;
        musicAudioSource.volume = newVolume;
        
    }


    public void ChangeVolumeWithSlider(AudioSource audioSourceToChange, float newVolume) {
       
        newVolume = newVolume > 1f ? 1 : newVolume < 0f ? 0 : newVolume;
        audioSourceToChange.volume = newVolume;        
    }

    public float CurrentMusicVolume()
    {
        return musicVolume;
    }

}

