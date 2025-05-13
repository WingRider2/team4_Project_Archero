using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCloseSim : MonsterBase
{
    
    public override float Attack()
    {


        if (!isAttack)
        {
            StartCoroutine(AttackMotion());
            return base.Attack();
        }
        return 0;
    }
  
    IEnumerator AttackMotion()
    {
        manationHandler.Attack();
        int cnt = 2;
        _rigidbody.velocity = new(0, 0);
        Vector2 targetPos = Target.position;
        while (cnt>0)
        {
            cnt--;
            yield return new WaitForSeconds(0.3f);
            _rigidbody.velocity = new(0, 5f);
            yield return new WaitForSeconds(0.1f);
            _rigidbody.velocity = new(0, -5f);
            yield return new WaitForSeconds(0.1f);
            _rigidbody.velocity = new(0, 0);
        }

        Vector2 nowPos = transform.position;
        yield return new WaitForSeconds(0.2f);
        float nowTime = 0;
        while (nowTime<attackTime)
        {
            nowTime += Time.deltaTime;
            float t = Mathf.Clamp01(nowTime / attackTime);
            float sq = t * t;
           Vector2 mov = Vector2.Lerp(nowPos, targetPos, sq);
            _rigidbody.MovePosition(mov);
            yield return null;
        }
        manationHandler.AttackEnd();
    }
}
