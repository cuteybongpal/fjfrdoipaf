using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
    Animator anim;
    public float CoolDown;
    Collider AttackCollider;
    void Start()
    {
        anim = GetComponent<Animator>();
        AttackCollider = GetComponent<Collider>();
        StartCoroutine(Command());
    }
    IEnumerator Command()
    {
        while (true)
        {
            anim.Play("Up");
            AttackCollider.enabled = true;
            yield return new WaitForSeconds(CoolDown);
            anim.Play("Down");
            AttackCollider.enabled = false;
            yield return new WaitForSeconds(CoolDown);
        }
    }
}
