using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Vector3 arenaPos;
    public Vector3 shopPos;
    public Transform cameraPostion;
    public Transform playerPostion;
    private bool isInShop = false;

    public float cameraMoveSpeed = 5f;
    private Vector3 targetPos;
    private bool moveCam = false;

    private void Start()
    {
        targetPos = arenaPos;
    }

    /*
     * moves the camera between the shop and the arena
     */
    private void MoveCameraPosition()
    {
        if (isInShop)
        {
            targetPos = arenaPos;
            playerPostion.position += new Vector3(0, 1, 0);
            moveCam = true;
        }
        else
        {
            targetPos = shopPos;
            playerPostion.position += new Vector3(0, -1, 0);
            moveCam = true;
        }
        isInShop = !isInShop;
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
