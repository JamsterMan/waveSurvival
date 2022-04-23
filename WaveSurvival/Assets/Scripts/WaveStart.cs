using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStart : MonoBehaviour
{
    public WaveController waveController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Wave Start");
        waveController.WaveStart();
        gameObject.SetActive(false);
    }
}
