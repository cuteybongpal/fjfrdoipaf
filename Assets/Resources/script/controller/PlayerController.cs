using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Attack,
        Jump
    }
    PlayerState state = PlayerState.Idle;
    public PlayerState State 
    {
        get { return state; }
        set 
        {
            switch (value)
            {
                case PlayerState.Idle:
                    S_Idle();
                    break;
                case PlayerState.Walk:
                    S_Run();
                    break;
                case PlayerState.Jump:
                    S_Jump();
                    break;
                case PlayerState.Attack:
                    S_Attack();
                    break;
            }
            state = value;
        }
    }
    bool canJump = true;
    Rigidbody rb;
    Animator anim;
    public int MaxHp;
    int currentHp;
    int currentO2;
    public Collider AttackCollider;
    public float AttackCoolDown = 1;
    public int CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = value;
            var ui = UIManager.Instance.GetMainUI<UI_Player>();
            if (ui == null)
                return;
            if (currentHp <= 0)
            {
                GameManager.Instance.GameOver();
                return;
            }
            ui.SetHp(currentHp);
        }
    }
    public int CurrentO2
    {
        get { return currentO2; }
        set
        {
            currentO2 = value;
            var ui = UIManager.Instance.GetMainUI<UI_Player>();
            if (ui == null)
                return;
            ui.SetO2(currentO2);
            if (currentO2 <= 0)
            {
                currentO2 = 0;
                CurrentHp--;
            }
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        AttackCollider.enabled = false;

        if (GameManager.Instance.PlayerCurrentHp != 0)
        {
            CurrentHp = GameManager.Instance.PlayerCurrentHp;
            CurrentO2 = GameManager.Instance.PlayerCurrentO2;
        }
        else
        {
            CurrentHp = MaxHp;
            CurrentO2 = GameManager.Instance.MaxPlayerO2;
        }
        StartCoroutine(ReduceO2());
        GameManager.Instance.GetPlayer = null;
        GameManager.Instance.GetPlayer = GetPlayer;
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            dir += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            dir += Vector3.back;

        if (Input.GetKey(KeyCode.A))
            dir += Vector3.left;

        if (Input.GetKey(KeyCode.D))
            dir += Vector3.right;
        if (Input.GetMouseButtonDown(0))
        {
            State = PlayerState.Attack;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity += Vector3.up * 5;
            State = PlayerState.Jump;
        }

        if (dir == Vector3.zero)
        {
            State = PlayerState.Idle;
            rb.velocity = Vector3.zero + new Vector3(0,rb.velocity.y,0);
        }
        else
        {
            rb.velocity = dir * GameManager.Instance.PlayerSpeed + new Vector3(0,rb.velocity.y,0);
            State = PlayerState.Walk;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, ray.direction, 100);
        Vector3 collisionPos = Vector3.zero;
        foreach (RaycastHit hit in hits )
        {
            if (hit.collider.CompareTag("Ground"))
            {
                collisionPos = new Vector3(hit.point.x, 0, hit.point.z) - new Vector3(transform.position.x, 0, transform.position.z);
                break;
            }
        }
        if (collisionPos != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(collisionPos);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            CurrentHp--;
            Debug.Log("데미지 입음");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
    void S_Idle()
    {
        anim.SetFloat("Speed", 0);
    }
    void S_Run()
    {
        anim.SetFloat("Speed", 1);
    }
    void S_Attack()
    {
        anim.Play("Attack");
    }
    void S_Jump()
    {
        anim.Play("Jump");
    }

    void AttackCollisionOn()
    {
        StartCoroutine(OnoffAttackCollision());
    }
    IEnumerator OnoffAttackCollision()
    {
        AttackCollider.enabled = true;
        yield return new WaitForSeconds(.1f);
        AttackCollider.enabled = false;
    }
    IEnumerator ReduceO2()
    {
        while (true)
        {
            CurrentO2--;
            yield return new WaitForSeconds(1);
        }
    }
    public PlayerController GetPlayer()
    {
        return this;
    }

}
