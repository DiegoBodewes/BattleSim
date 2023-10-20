using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float destroyTime = 10;
    private void Update()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack") Destroy(this.gameObject);
    }
}
