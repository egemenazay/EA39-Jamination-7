using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 targetPoint = Vector3.zero;

    public PlayerController player;

    private void Start()
    {
        targetPoint = new Vector3(player.transform.position.x, player.transform.position.y, -4);
    }

    private void LateUpdate()
    {
        targetPoint.x = player.transform.position.x;
        targetPoint.y = player.transform.position.y;

        if (targetPoint.y<-3)
        {
            targetPoint.y = -3;
        }

        transform.position = targetPoint;
    }
}
