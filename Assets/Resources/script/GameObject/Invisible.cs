using System.Collections;
using UnityEngine;

public class Invisible : Item
{
    public float duration;
    public Material[] PlayerMaterials;
    public override void UseItem()
    {
        StartCoroutine(FadeOutNIn());
        base.UseItem();
    }
    IEnumerator FadeOutNIn()
    {
        foreach (var item in PlayerMaterials)
        {
            Color color = item.color;
            color.a = .1f;
            item.color = color;
        }

        yield return new WaitForSeconds(duration);

        foreach (var item in PlayerMaterials)
        {
            Color color = item.color;
            color.a = 1f;
            item.color = color;
        }
        Destroy(gameObject);
    }
    public override object Clone()
    {
        GameObject go = new GameObject() { name = "Item"};
        Invisible i  = go.AddComponent<Invisible>();
        i.duration = duration;
        i.PlayerMaterials = PlayerMaterials;
        i.Sprite = Sprite;
        i.Weight = Weight;
        return i;
    }
}
