using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSoundPlayer : MonoBehaviour
{
    public UnitSoundBase soundBase;
    public void Play(UnitSoundType type)
    {
        if (soundBase == null)
        {
            Debug.Log("사운드 베이스 실종");
            return;
        }
        var clip = soundBase.GetClip(type);
        if (clip != null)
            SoundManager.PlayClip(clip);
    }
    public void Play(UnitSoundType type,int i)
    {
        if (soundBase == null)
        {
            Debug.Log("사운드 베이스 실종");
            return;
        }
        var clip = soundBase.GetClip(type,i);
        if (clip != null)
            SoundManager.PlayClip(clip);
    }
    public SoundSource MakeLoop(UnitSoundType type)
    {
        if (soundBase == null)
        {
            Debug.Log("사운드 베이스 실종");
            return null;
        }
        var clip = soundBase.GetClip(type);
        if (clip != null)
            return SoundManager.GetClip(clip);
        return null;
    }
   
}
