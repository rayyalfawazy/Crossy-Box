using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float speed = 0.004f;
        var x = Input.GetAxisRaw("Vertical");
        //var z = Input.GetAxis("Vertical");
        Vector3 directionVector = new Vector3(x,0,0);

        transform.position = transform.position + speed * directionVector.normalized;
    }
}
