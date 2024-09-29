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
    public AudioManagerVolumeSetter volumeSetter;

    public AudioSource SFXSource;
    public AudioSource ambiant1;
    public AudioSource ambiant2;
    public AudioSource musicSource;

    // Liste des musiques dispo
    [SerializeField] private AudioClip[] Musics;
    private bool ambiant1IsPlaying = false;
    private bool ambiantIsPlaying = false;

    // Ajouter des sons pour les menus par exemple?
    [SerializeField] private AudioClip[] menuSFXs;

    [SerializeField] private AudioClip[] AmbiantsList;

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
    public enum ambiant
    {
        inside,
        outside
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
        /* Utilisation des input direct pour tester. */
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            Debug.Log("Musique secrète!");
            PlayMusic(music.game2);
        }
    }

    private void Start()
    {
        volumeSetter.setLevels();
        PlayMusic(music.menu);
    }

    /*                           */
    /*      Gestion des SFX      */
    /*                           */

    public void PlayAmbiant(ambiant amb)
    {
        AudioClip clip = AmbiantsList[0];
        if(amb == ambiant.outside)
        {
            clip = AmbiantsList[1];
        }
        if (ambiantIsPlaying)
        {
            FadeAmbiant(clip);
        }
        else
        {
            PlayAmbiant(clip);
            ambiantIsPlaying = true;
        }
    }

    public void StopAmbiant()
    {
        if (!ambiantIsPlaying) return;
        if (ambiant1IsPlaying)
        {
            ambiant1.Stop();
        }
        else
        {
            ambiant2.Stop();
        }
        ambiantIsPlaying = false;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlaySFXRandomPitch(AudioClip clip, int percentage = 10)
    {
        Debug.Log("Clip = " + clip);
        float p = percentage / 100f;
        float pitch = Random.Range(1 - p, 1 + p);
        //SFXSource.pitch = pitch;
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

    public void turnPageNoise()
    {
        PlaySFXRandomPitch(menuSFXs[1]);
    }

    public void closeBookNoise()
    {
        PlaySFXRandomPitch(menuSFXs[2]);
    }

    public void takeObjectNoise()
    {
        PlaySFXRandomPitch(menuSFXs[3]);
    }

    public void putDownObjectNoise()
    {
        PlaySFXRandomPitch(menuSFXs[4]);
    }

    /*                                 */
    /*      Gestion de la musique      */
    /*                                 */


    // Play a single clip through the music source.
    public void PlayMusic(music music)
    {
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
                clip = Musics[2];
                break;
            default: clip = null;
                break;
        }
        PlayMusic(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayAmbiant(AudioClip clip)
    {
        Debug.Log("play ambiant");
        //Debug.Log("play Music (clip)");
        if (ambiant1IsPlaying)
        {
            ambiant1.clip = clip;
            ambiant1.Play();
        }
        else
        {
            ambiant2.clip = clip;
            ambiant2.Play();
        }
    }

    public void FadeAmbiant(AudioClip clip, float fadeTime = 1.0f)
    {
        Debug.Log("fade ambiant");
        //Debug.Log("fade music");
        float musicTime = 0;
        if (ambiant1IsPlaying)
        {
            //Debug.Log("music 1 playing");
            if (ambiant1.clip.length > clip.length)
            {
                musicTime = ambiant1.time % clip.length;
            }
            else
            {
                musicTime = ambiant1.time;
            }
            ambiant2.clip = clip;
            ambiant2.time = musicTime;

            StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music1Volume", fadeTime, 0.0f));
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music2Volume", fadeTime, 1.0f));

            Invoke("StopA1", fadeTime);
            ambiant2.Play();
            ambiant1IsPlaying = false;
        }
        else
        {
            Debug.Log("music 2 playing");
            if (ambiant2.clip.length > clip.length)
            {
                musicTime = ambiant2.time % clip.length;
            }
            else
            {
                musicTime = ambiant2.time;
            }
            ambiant1.clip = clip;
            ambiant1.time = musicTime;

            StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music1Volume", fadeTime, 1.0f));
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music2Volume", fadeTime, 0.0f));

            Invoke("StopA2", fadeTime);
            ambiant1.Play();
            ambiant1IsPlaying = true;
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
