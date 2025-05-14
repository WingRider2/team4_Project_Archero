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
    public AudioClip[] attack;
    public AudioClip[] hit;
    public AudioClip[] die;
    public AudioClip[] skill;
    public AudioClip[] move;
    public AudioClip GetClip(UnitSoundType type)
    {
        return type switch
        {
            UnitSoundType.Attack => attack[Random.Range(0, attack.Length)],
            UnitSoundType.Hit => hit[Random.Range(0, hit.Length)],
            UnitSoundType.Die => die[Random.Range(0, die.Length)],
            UnitSoundType.Skill => skill[Random.Range(0,skill.Length)],
            UnitSoundType.Move => move[Random.Range(0, move.Length)],
            _ => null
        };
    }
    public AudioClip GetClip(UnitSoundType type,int index)
    {
        return type switch
        {
            UnitSoundType.Attack => attack[index],
            UnitSoundType.Hit => hit[index],
            UnitSoundType.Die => die[index],
            UnitSoundType.Skill => skill[index],
            UnitSoundType.Move => move[index],
            _ => null
        };
    }
}
