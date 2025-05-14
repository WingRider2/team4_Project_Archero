using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MonSFX { }
public enum PlayerSFX { }
public enum EnvironmentSFX { }
public class SoundManager : Singleton<SoundManager>
{
    public SoundSource soundPrefab;
    [Header("SFX")]
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)]private float soundEffectPitch;
    public AudioClip[] monSFXClips;
    public AudioClip[] playerSFXClips;
    public AudioClip[] enviromentSFXClips;
    [Header("BGM")]
    [SerializeField][Range(0f, 1f)] private float bgmVolume;
    private AudioSource bgmAudioSource;
    public AudioClip bgmClip;
    
    private void Awake()
    {
        bgmAudioSource = GetComponent<AudioSource>();
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.loop = true;
    }
    private void Start()
    {
        ChangeBGM(bgmClip);
    }
    public void ChangeBGM(AudioClip clip)
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }
    
    public static void PlayClip(MonSFX fx)
    {
        AudioClip clip = Instance.monSFXClips[(int)fx];
        PlayClip(clip);
    }
    public static void PlayClip(PlayerSFX fx)
    {
        AudioClip clip = Instance.monSFXClips[(int)fx];
       PlayClip(clip);
    }
    public static void PlayClip(EnvironmentSFX fx)
    {
        AudioClip clip = Instance.monSFXClips[(int)fx];
      PlayClip(clip);
    }
    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(Instance.soundPrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, Instance.soundEffectVolume, Instance.soundEffectPitch);
    }
    public static SoundSource GetClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(Instance.soundPrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.LoopMake(clip, Instance.soundEffectVolume, Instance.soundEffectPitch);
        return soundSource;
    }
}
