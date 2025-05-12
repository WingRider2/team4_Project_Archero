using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageEffect :MonoBehaviour
{
    //각종 공격방식에 대해서
    //공격의 개수,
    //공격의 각도,
    //투사체 속도?

    public abstract void ExecuteAttack();
}
