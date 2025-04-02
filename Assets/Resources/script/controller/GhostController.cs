using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Transform[] Points;
    int index = 1;
    public float Speed;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(CommandMove());
    }
    IEnumerator CommandMove()
    {
        while (true)
        {
            yield return Move();
            index = (index + 1) % Points.Length;
        }
    }
    IEnumerator Move()
    {
        Vector3 originPos = transform.position;
        Vector3 direction = (Points[index].position - originPos).normalized;
        rb.velocity = direction * Speed;
        transform.rotation = Quaternion.LookRotation(direction);
        while (true)
        {
            if (Vector3.Distance(transform.position, Points[index].position) <= 0.1f)
                break;
            yield return null;
        }
    }
}
