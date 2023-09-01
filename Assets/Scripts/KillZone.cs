using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private ScenesManager scenesManager;

    private void Start()
    {
        scenesManager = FindObjectOfType<ScenesManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject)
        {
            Time.timeScale = 0;
            scenesManager.ActivePanel();
            Destroy(other.gameObject);
        }
    }
}
