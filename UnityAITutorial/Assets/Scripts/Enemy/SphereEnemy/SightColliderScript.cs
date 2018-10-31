using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightColliderScript : MonoBehaviour {

    public bool playerInSight;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInSight = true;
        }
    }
}
