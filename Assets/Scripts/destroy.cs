using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public float timer = 5f;

    public Material borderMaterial;

    void Start()
    {
        if (transform.position.x < -23 || transform.position.x > 23 || transform.position.z < -23 || transform.position.z > 23)
        {
            GetComponent<Renderer>().material = borderMaterial;
        }

        Destroy(gameObject, timer);
     
    }
}
