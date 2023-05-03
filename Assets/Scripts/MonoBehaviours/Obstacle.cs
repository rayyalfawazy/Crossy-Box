using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    static HashSet<Vector3> positionSet = new HashSet<Vector3>();
    public static HashSet<Vector3> Position
    { 
        get => new HashSet<Vector3>(positionSet); 
    }

    void OnEnable()
    {
        positionSet.Add(this.transform.position);
    }

    void OnDisable()
    {
        positionSet.Remove(this.transform.position);
    }
}

