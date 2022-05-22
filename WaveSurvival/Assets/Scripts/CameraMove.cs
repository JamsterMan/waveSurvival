using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Vector3 arenaPos;
    public Vector3 shopPos;
    public Transform cameraPostion;
    private bool isInShop = false;

    private void MoveCameraPosition()
    {
        if (isInShop)
        {
            cameraPostion.position = arenaPos;
        }
        else
            cameraPostion.position = shopPos;

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
