using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Vector3 arenaPos;
    public Vector3 roomPos;
    public Transform cameraPostion;
    public Transform playerPostion;
    public Transform playerArenaAdjust;
    public Transform playerRoomAdjust;
    public bool isBossRoom = false;
    private bool isInRoom = false;

    public float cameraMoveSpeed = 5f;
    private Vector3 targetPos;
    private bool moveCam = false;

    private void Start()
    {
        targetPos = arenaPos;
    }

    /*
     * moves the camera between the main arena and the room (boss room or shop room)
     */
    private void MoveCameraPosition()
    {
        if (isInRoom)
        {
            targetPos = arenaPos;
            playerPostion.position = playerArenaAdjust.position;
            moveCam = true;
        }
        else
        {
            targetPos = roomPos;
            playerPostion.position = playerRoomAdjust.position;
            moveCam = true;
        }
        isInRoom = !isInRoom;
    }

    private void Update()
    {
        if (moveCam)
        {
            cameraPostion.position = Vector3.Lerp(cameraPostion.position, targetPos, cameraMoveSpeed * Time.deltaTime);
            if(cameraPostion.position == targetPos)
            {
                moveCam = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MoveCameraPosition();
        }
    }
}
