using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource musicSource;
    private AudioSource soundEffectSource;

    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip bell;
    [SerializeField] private AudioClip objectSound;
    [SerializeField] private AudioClip rightOrder;
    [SerializeField] private AudioClip wrongOrder;
    [SerializeField] private AudioClip pourSoundEffect;
    [SerializeField] private AudioClip hardInredientEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        musicSource = FindObjectOfType<Camera>().GetComponent<AudioSource>();
        soundEffectSource = GetComponent<AudioSource>();
    }

    public void ActivateGameplayMusic()
    {
        musicSource.clip = gameplayMusic;
        musicSource.Play();
    }

    public void ActivateMainMenuMusic()
    {
        musicSource.clip = mainMenuMusic;
    }

    public void PlayBell()
    {
        soundEffectSource.clip = bell;
        soundEffectSource.Play();
    }

    public void PlayObjectSound()
    {
        soundEffectSource.clip = objectSound;
        soundEffectSource.Play();
    }

    public void PlayRightOrder()
    {
        soundEffectSource.clip = rightOrder;
        soundEffectSource.Play();
    }

    public void PlayeWrongOrder()
    {
        soundEffectSource.clip = wrongOrder;
        soundEffectSource.Play();
    }

    public void PlayPourEffect()
    {
        soundEffectSource.clip = pourSoundEffect;
        soundEffectSource.Play();
    }

    public void PlayHardIngredientPoutEffect()
    {
        soundEffectSource.clip = hardInredientEffect;
        soundEffectSource.Play();
    }
}