using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IStorable
{
    public Sprite Sprite;
    public int Weight;

    public virtual void UseItem()
    {
        GameManager.Instance.Inventory.Remove(this);
        UIManager.Instance.GetMainUI<UI_Player>().Refresh();
    }
    public virtual object Clone()
    {
        GameObject go = new GameObject() { name = "Item" };
        Item item = go.AddComponent<Item>();
        item.Weight = Weight;
        item.Sprite = Sprite;
        return item;
    }

    public void Store()
    {
        if (GameManager.Instance.AddToInventory(this))
        {
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        Store();
    }
}
