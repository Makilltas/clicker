using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 70f;

    public bool randomAxis = true;

    public Vector3 axis = Vector3.forward;



    // Start is called before the first frame update
    void Start()
    {
        if(randomAxis)
        {
            axis.x = Random.Range(-1f,1f);
            axis.y = Random.Range(-1f, 1f);
            axis.z = Random.Range(-1f, 1f);
        }
    }

    void Update()
    {
        transform.Rotate(axis * speed * Time.deltaTime);
    }


}
