using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audiSource;
    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        if(_audiSource == null)
            _audiSource = GetComponent<AudioSource>();
        CancelInvoke();
        _audiSource.clip = clip;
        _audiSource.volume = soundEffectVolume;
        _audiSource.Play();
        _audiSource.pitch=1f+Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);
        Invoke("Disable", clip.length + 2);
    }
    public void Disable()
    {
        _audiSource.Stop();
        Destroy(this.gameObject);
    }

}
