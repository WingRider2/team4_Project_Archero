using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitSoundType
{
    Attack,
    Hit,
    Die,
    Move,
    Skill
}
[CreateAssetMenu(menuName = "Audio/UnitSoundBase")]
public class UnitSoundBase : ScriptableObject
{
    public AudioClip attack;
    public AudioClip hit;
    public AudioClip die;
    public AudioClip skill;
    public AudioClip move;
    public AudioClip GetClip(UnitSoundType type)
    {
        return type switch
        {
            UnitSoundType.Attack => attack,
            UnitSoundType.Hit => hit,
            UnitSoundType.Die => die,
            UnitSoundType.Skill => skill,
            UnitSoundType.Move => move,
            _ => null
        };
    }
}
