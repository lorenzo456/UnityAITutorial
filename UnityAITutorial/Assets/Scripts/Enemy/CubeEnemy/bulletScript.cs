using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update ()
    {
        transform.position += transform.forward * Time.deltaTime * 2f;	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
