using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController player;
    Vector3 CameraOffset;
    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        CameraOffset = transform.position - player.transform.position;
    }
    private void Update()
    {
        RaycastHit[] hits = Physics.RaycastAll(player.transform.position, CameraOffset.normalized, CameraOffset.magnitude);

        Vector3 collsionPos = Vector3.zero;
        foreach (RaycastHit hit in hits )
        {
            if (hit.collider.CompareTag("Attack") || hit.collider.CompareTag("Trap"))
                continue;
            collsionPos = hit.point;
            break;
        }
        if (collsionPos == Vector3.zero )
        {
            transform.position = player.transform.position + CameraOffset;
        }
        else
        {
            transform.position = collsionPos - CameraOffset.normalized * .1f;
        }
    }
}
