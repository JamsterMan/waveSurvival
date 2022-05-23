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

    /*
     * moves the camera between the shop and the arena
     */
    private void MoveCameraPosition()
    {
        if (isInShop)
        {
            cameraPostion.position = arenaPos;//switch to smooth camera movement
            playerPostion.position += new Vector3(0, 1, 0);
        }
        else
        {
            cameraPostion.position = shopPos;
            playerPostion.position += new Vector3(0, -1, 0);
        }
        isInShop = !isInShop;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MoveCameraPosition();
        }
    }
}
