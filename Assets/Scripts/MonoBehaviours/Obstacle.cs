using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    protected static HashSet<Vector3> positionSet = new HashSet<Vector3>();
    public static HashSet<Vector3> Position
    { 
        get => new HashSet<Vector3>(positionSet); 
    }

    protected void OnEnable()
    {
        positionSet.Add(this.transform.position);
    }

    protected void OnDisable()
    {
        positionSet.Remove(this.transform.position);
    }
}

