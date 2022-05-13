using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStart : MonoBehaviour
{
    public WaveController waveController;
    public Transform playerPos;
    public float safeDistance = 2f;
    [SerializeField] private bool canBeUsed = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && canBeUsed)//only start the wave if the player hits the button
        {
            Debug.Log("Wave Start");
            waveController.WaveStart();
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        //check if player is too close
        if ((transform.position - playerPos.position).magnitude < safeDistance)
        {
            canBeUsed = false;
            //change sprite to a different look
        }
        else
        {
            canBeUsed = true;
            //change sprite to original look
        }
    }

    private void Update()
    {
        if (!canBeUsed)
        {
            if((transform.position - playerPos.position).magnitude >= safeDistance)
                    canBeUsed = true;
        }
    }
}
