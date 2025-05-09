using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleArrowSkill : IAttackSkill, ISKill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public SkillType Type { get; set; }
    public float Value { get; set; }

    void IAttackSkill.Attack(float _angle)
    {
        GameObject arrow = ObjectPool.Instance.Get();
        ProjectileController controller = arrow.GetComponent<ProjectileController>();
        float[] angleOffsets = { -15f, 0f, 15f };

        foreach (float offset in angleOffsets)
        {
            float angle = _angle + offset;

            Vector2 direction = Quaternion.Euler(0, 0, _angle) * player.lookDirection;

            arrow.transform.position = Quaternion.Euler(0, 0, angle);

            //Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

            //if (rb != null)
            //{
            //    rb.velocity = shootDir.normalized * 10f;
            //}

        }
    }
}
