using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BatConroller : MonoBehaviour
{
    PlayerController player;
    Rigidbody rb;
    public float Speed;
    public float distance;

    public int MaxHp;
    int currentHp;
    public int CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = value;
            if (currentHp <= 0)
                Destroy(gameObject);
        }
    }
    public ParticleSystem particle;
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        rb = GetComponent<Rigidbody>();
        CurrentHp = MaxHp;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > distance || player.Invisible)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        Vector3 dir = player.transform.position - transform.position;
        dir.y = 0;
        dir = dir.normalized;
        transform.rotation = Quaternion.LookRotation(dir);
        rb.velocity = dir * Speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Attack"))
            return;
        CurrentHp--;
        particle.Play();
    }
}

