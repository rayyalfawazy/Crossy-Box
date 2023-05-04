using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Range(0,5)] public float speed;

    protected Vector3 initialPosition;
    protected float distanceLimit = float.MaxValue;

    public void SetUpDistanceLimit(float distance)
    {
        this.distanceLimit = distance;
    }

    protected void Start()
    {
        initialPosition = this.transform.position;
    }

    protected void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(initialPosition,this.transform.position) > this.distanceLimit) 
        {
            Destroy(this.gameObject);
        }
    }
}
