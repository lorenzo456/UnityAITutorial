using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpike : MonoBehaviour {

    void OnTriggerEnter( Collider other)
    {
        if (CompareTag("Enemy"))
        {
            other.transform.GetComponent<BaseEnemy>().OnHit(10);
        }
    }
}
