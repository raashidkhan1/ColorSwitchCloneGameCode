using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform player;
    public float offsetY = 1f;
    public Vector3 tempPos;

    private void Awake()
    {
        if (player!=null)
        {
            // set the camera's position to the player's position
            transform.position = new Vector3(player.position.x, player.position.y + offsetY, transform.position.z);
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // follow the player on the y axis with an offset
            tempPos = transform.position;
            tempPos.y = Mathf.Max(tempPos.y, player.position.y + offsetY);
            transform.position = tempPos;
        }
    }
}
