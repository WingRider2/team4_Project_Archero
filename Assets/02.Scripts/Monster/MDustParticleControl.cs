using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDustParticleControl : MonoBehaviour
{
    [SerializeField] private bool createDustOnWalk = true;
    [SerializeField] private ParticleSystem dustParticleSystem;
   int walkDustAmount = 5;
   int attackDustAmount =20;

    public void CreateDustParticles()
    {
        if (createDustOnWalk)
        {
            dustParticleSystem.Emit(walkDustAmount);
        }
    }
    public void AttackParitcles()
    {
        if (createDustOnWalk)
        {
            dustParticleSystem.Emit(attackDustAmount);
        }
    }
}
