using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSkill : ISKill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public SkillType Type { get; set; }

    public float Value { get; set; }

    public ArrowSkill(SkillData data)
    {
        Id = data.Id;
        Name = data.Name;
        Type = data.Type;
    }

    public void ExecuteSkill(GameObject player)
    {

    }

    public void TripleArrow(GameObject player, GameObject weapon, GameObject arrowPrefab)
    {
        Vector3 firePoint = player.transform.position;

        float baseAngle = weapon.transform.eulerAngles.z;

        float[] angleOffsets = { -15f, 0f, 15f };

        foreach (float offset in angleOffsets)
        {
            float angle = baseAngle + offset;

            Vector2 shootDir = Quaternion.Euler(0, 0, angle) * Vector2.right;
            GameObject arrow = GameObject.Instantiate(arrowPrefab, firePoint, Quaternion.identity);
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = shootDir.normalized * 10f;
            }

            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
