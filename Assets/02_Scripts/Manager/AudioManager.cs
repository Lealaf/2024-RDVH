using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using static AudioManager;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    public static AudioManager Instance => instance;

    public AudioMixer mixer;

    public AudioSource SFXSource;
    public AudioSource ambiant1;
    public AudioSource ambiant2;
    public AudioSource music1;
    public AudioSource music2;

    // Liste des musiques dispo
    [SerializeField] private AudioClip[] Musics;
    private bool music1IsPlaying = false;

    // Ajouter des sons pour les menus par exemple?
    [SerializeField] private AudioClip[] menuSFXs;

    [SerializeField] private AudioClip[] ambiantsSounds;

    private float masterVolume;
    private float musicsVolume;
    private float sfxVolume;

    public enum music
    {
        menu,
        loss,
        win,
        pause,
        game1,
        game2
    }
    public enum menuSFX
    {
        button,
        page,
        book,
        take,
        putDown
    }
    public enum ambiant
    {
        inside,
        outside
    }

    private void Awake()
    {
        Debug.Log("Awake audio manager");
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        SetMasterLevel(PlayerPrefs.GetFloat("MasterVolume", 0.75f));
        SetSFXLevel(PlayerPrefs.GetFloat("SFXVolume", 0.75f));
        SetMusicLevel(PlayerPrefs.GetFloat("MusicVolume", 0.75f));
    }

    void Update()
    {
        /* Utilisation des input direct pour tester. */
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            PlayMusic(music.game2);
        }
    }

    public void SetMasterLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
        masterVolume = sliderValue;
    }

    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
        sfxVolume = sliderValue;
    }

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        musicsVolume = sliderValue;
    }

    public float GetMasterLevel()
    {
        //Debug.Log("AM - GetMasterLevel");
        return masterVolume;
    }
    public float GetMusicLevel()
    {
        //Debug.Log("AM - GetMusicLevel");
        return musicsVolume;
    }
    public float GetSFXLevel()
    {
        //Debug.Log("AM - GetSFXLevel");
        return sfxVolume;
    }

    /*                           */
    /*      Gestion des SFX      */
    /*                           */

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlaySFXRandomPitch(AudioClip clip, int percentage = 10)
    {
        float p = percentage / 100f;
        float pitch = Random.Range(1 - p, 1 + p);
        SFXSource.pitch = pitch;
        SFXSource.PlayOneShot(clip);
    }

    public void PlaySFXRandomPitch(menuSFX fx, int percentage = 10)
    {
        AudioClip clip = menuSFXs[0];
        switch(fx)
        {
            case menuSFX.button:
                break;
            case menuSFX.take:
                clip = menuSFXs[3];
                break;
            case menuSFX.putDown:
                clip = menuSFXs[4];
                break;
            case menuSFX.page:
                clip = menuSFXs[1];
                break;
            case menuSFX.book:
                clip = menuSFXs[2];
                break;
        }
        PlaySFXRandomPitch(clip, percentage);
    }

    public void PlaySFXAtPoint(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos);
    }

    public void pressButtonNoise()
    {
        PlaySFXRandomPitch(menuSFX.button);
    }
    public void putDownObjectNoise()
    {
        PlaySFXRandomPitch(menuSFX.putDown);
    }
    public void takeObjectNoise()
    {
        Debug.Log("takeObjectNoise");
        PlaySFXRandomPitch(menuSFX.take);
    }
    public void turnPageNoise()
    {
        PlaySFXRandomPitch(menuSFX.page);
    }
    public void closeBookNoise()
    {
        PlaySFXRandomPitch(menuSFX.book);
    }

    /*                                  */
    /*     Gestion des sons ambiants    */
    /*                                  */

    public void StopAmbiant()
    {
        ambiant1.Stop();
        ambiant2.Stop();
    }

    public void PlayAmbiant(ambiant amb)
    {
        AudioClip clip = null;
        switch (amb)
        {
            case ambiant.outside: 
                clip = ambiantsSounds[0]; 
                break;
            case ambiant.inside: 
                clip = ambiantsSounds[1]; 
                break;
        }
        PlayAmbiant(clip);
    }

    public void PlayAmbiant(AudioClip clip, float fadeTime = 1.0f)
    {
        Debug.Log("play ambiant");
        float clipTime = 0;
        if (ambiant1.isPlaying)
        {
            Debug.Log("ambiant 1 playing");
            if (ambiant1.clip.length > clip.length)
            {
                clipTime = music1.time % clip.length;
            }
            else
            {
                clipTime = music1.time;
            }
            ambiant2.clip = clip;
            ambiant2.time = clipTime;

            StartCoroutine(FadeMixerGroup.StartFade(mixer, "Ambiant1Volume", fadeTime, 0.0f));
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "Ambiant2Volume", fadeTime, 1.0f));

            Invoke("StopA1", fadeTime);
            music2.Play();
        }
        else if (ambiant2.isPlaying)
        {
            Debug.Log("ambiant 2 playing");
            if (music2.clip.length > clip.length)
            {
                clipTime = music2.time % clip.length;
            }
            else
            {
                clipTime = music2.time;
            }
            ambiant1.clip = clip;
            ambiant1.time = clipTime;

            StartCoroutine(FadeMixerGroup.StartFade(mixer, "Ambiant1Volume", fadeTime, 1.0f));
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "Ambiant2Volume", fadeTime, 0.0f));

            Invoke("StopA2", fadeTime);
            ambiant1.Play();
        }
        else
        {
            ambiant1.clip = clip;
            ambiant1.Play();
        }
    }

    void StopA1()
    {
        ambiant1.Stop();
    }
    void StopA2()
    {
        ambiant2.Stop();
    }

    /*                                 */
    /*      Gestion de la musique      */
    /*                                 */

    // Play a single clip through the music source.
    public void PlayMusic(music music)
    {
        Debug.Log("play Music (music)"+music);
        bool fade = false;
        AudioClip clip;
        switch (music)
        {
            case music.menu:
                clip = Musics[0];
                break;
            case music.loss:
                clip = Musics[3];
                break;
            case music.win:
                clip = Musics[4];
                break;
            case music.pause:
                clip = Musics[0];
                break;
            case music.game1:
                clip = Musics[1];
                break;
            case music.game2:
                //fade = true;
                clip = Musics[2];
                break;
            default:
                clip = null;
                break;
        }
        if (fade)
        {
            FadeMusic(clip);
        }
        else
        {
            PlayMusic(clip);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        Debug.Log("play Music (clip)");
        if (music1IsPlaying)
        {
            music1.clip = clip;
            music1.Play();
        }
        else
        {
            music2.clip = clip;
            music2.Play();
        }
    }

    public void FadeMusic(AudioClip clip, float fadeTime = 1.0f)
    {
        Debug.Log("fade music");
        float musicTime = 0;
        if (music1IsPlaying)
        {
            Debug.Log("music 1 playing");
            if (music1.clip.length > clip.length)
            {
                musicTime = music1.time % clip.length;
            }
            else
            {
                musicTime = music1.time;
            }
            music2.clip = clip;
            music2.time = musicTime;

            StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music1Volume", fadeTime, 0.0f));
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music2Volume", fadeTime, 1.0f));

            Invoke("StopM1", fadeTime);
            music2.Play();
            music1IsPlaying = false;
        }
        else
        {
            Debug.Log("music 2 playing");
            if (music2.clip.length > clip.length)
            {
                musicTime = music2.time % clip.length;
            }
            else
            {
                musicTime = music2.time;
            }
            music1.clip = clip;
            music1.time = musicTime;

            StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music1Volume", fadeTime, 1.0f));
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music2Volume", fadeTime, 0.0f));

            Invoke("StopM2", fadeTime);
            music1.Play();
            music1IsPlaying = true;
        }
    }

    void StopM1()
    {
        music1.Stop();
    }
    void StopM2()
    {
        music2.Stop();
    }
}

/*                                      */
/*      Classe pour fade deux clips     */
/*                                      */

public static class FadeMixerGroup
{
    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
        while (currentTime < duration)
        {
            //Debug.Log("On passe ici"+ exposedParam+ " currentVol = "+ currentVol+ " target = "+ targetValue+" log : "+ Mathf.Log10(targetValue) * 20);
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }
}
