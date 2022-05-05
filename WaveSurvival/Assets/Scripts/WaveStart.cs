using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStart : MonoBehaviour
{
    public WaveController waveController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))//only start the wave if the player hits the button
        {
            Debug.Log("Wave Start");
            waveController.WaveStart();
            gameObject.SetActive(false);
        }
    }
}
