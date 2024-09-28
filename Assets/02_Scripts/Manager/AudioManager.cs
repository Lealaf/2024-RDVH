using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AudioManager;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    public static AudioManager Instance => instance;

    public AudioMixer mixer;

    public AudioSource SFXSource;
    public AudioSource music1;
    public AudioSource music2;

    // Liste des musiques dispo
    [SerializeField] private AudioClip[] Musics;
    private bool music1IsPlaying = false;

    // Ajouter des sons pour les menus par exemple?
    [SerializeField] private AudioClip[] menuSFXs;

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
        other
    }

    private void Awake()
    {
        Debug.Log("Awake du audio manager?");
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
    }

    void Update()
    {
        /* Utilisation des input direct pour tester. *//*
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {

        }
        */
    }

    private void Start()
    {

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

    public void PlaySFXAtPoint(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos);
    }

    public void pressButtonNoise()
    {
        PlaySFXRandomPitch(menuSFXs[0]);
    }

    /*                                 */
    /*      Gestion de la musique      */
    /*                                 */


    // Play a single clip through the music source.
    public void playMusic(music music)
    {
        Debug.Log("play Music (music)");
        bool fade = false;
        AudioClip clip;
        switch (music)
        {
            case music.menu:
                clip = Musics[0];
                break;
            case music.loss:
                clip = Musics[1];
                break;
            case music.win:
                clip = Musics[2];
                break;
            case music.pause:
                clip = Musics[3];
                break;
            case music.game1:
                clip = Musics[4];
                break;
            case music.game2:
                fade = true;
                clip = Musics[4];
                break;
            default: clip = null;
                break;
        }
        if (fade)
        {
            fadeMusic(clip);
        }
        else
        {
            playMusic(clip);
        }
    }


    public void playMusic(AudioClip clip)
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

    public void fadeMusic(AudioClip clip, float fadeTime = 1.0f)
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

            Invoke("stopM1", fadeTime);
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

            Invoke("stopM2", fadeTime);
            music1.Play();
            music1IsPlaying = true;
        }
    }


    void stopM1()
    {
        music1.Stop();
    }
    void stopM2()
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
