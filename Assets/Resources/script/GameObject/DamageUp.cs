using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : Item
{
    public float duration;
    public override void UseItem()
    {
        StartCoroutine(DamageBuff());
        base.UseItem();
    }
    IEnumerator DamageBuff()
    {
        GameManager.Instance.PlayerAttack++;
        yield return new WaitForSeconds(duration);
        GameManager.Instance.PlayerAttack--;
        Destroy(gameObject);
    }
    public override object Clone()
    {
        GameObject go = new GameObject() { name = "Item" };
        DamageUp dup = go.AddComponent<DamageUp>();
        dup.duration = duration;
        dup.Sprite = Sprite;
        dup.Weight = Weight;
        return dup;
    }

}
