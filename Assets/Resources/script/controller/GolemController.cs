using System.Collections;
using System.Collections.Generic;
using Unity.Content;
using UnityEngine;

public class GolemController : MonoBehaviour
{
    PlayerController player;
    Rigidbody rb;
    public float Speed;
    public float distance;
    public float AttackDistance;
    public Collider AttackCollider;
    bool canAttack = true;
    public AudioSource audios;
    public enum GolemState
    {
        Idle,
        Walk,
        Attack
    }
    GolemState State {
        set
        {
            switch (value)
            {
                case GolemState.Idle:
                    S_Idle();
                    break;
                case GolemState.Walk:
                    S_Walk();
                    break;
                case GolemState.Attack:
                    S_Attack();
                    break;
            }
        }
    }
    public int MaxHp;
    int currentHp;
    Animator anim;
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
    public ParticleSystem[] particles;
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        rb = GetComponent<Rigidbody>();
        CurrentHp = MaxHp;
        anim = GetComponent<Animator>();
        AttackCollider.enabled = false;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > distance || player.Invisible)
        {
            rb.velocity = Vector3.zero;
            State = GolemState.Idle;
            return;
        }
        Vector3 dir = player.transform.position - transform.position;
        dir.y = 0;
        dir = dir.normalized;
        transform.rotation = Quaternion.LookRotation(dir);
        rb.velocity = dir * Speed;
        
        if (Vector3.Distance(transform.position, player.transform.position) <= AttackDistance && canAttack)
        {
            State = GolemState.Attack;
        }
        else
        {
            State = GolemState.Walk;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Attack"))
            return;
        CurrentHp--;
        audios.Play();
        particles[0].Play();
    }
    void S_Idle()
    {
        anim.SetFloat("Speed", 0);
    }
    void S_Walk()
    {
        anim.SetFloat("Speed", 1);
    }
    void S_Attack()
    {
        if (canAttack)
            anim.Play("Attack");
    }
    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(3);
        canAttack = true;
    }
    void AttackCollisionOn()
    {
        StartCoroutine(OnOffAttackCollsion());
        StartCoroutine(AttackCoolDown());
    }
    IEnumerator OnOffAttackCollsion()
    {
        AttackCollider.enabled = true;
        particles[1].Play();
        audios.Play();
        yield return new WaitForSeconds(.3f);
        AttackCollider.enabled = false;
    }
}
