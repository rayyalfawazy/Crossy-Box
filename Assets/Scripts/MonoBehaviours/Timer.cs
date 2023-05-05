using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float initialTimer = 10;
    public float timer;

    // Start is called before the first frame update
    public void Start()
    {
        timer = initialTimer;
    }

    // Update is called once per frame
    public void Update()
    {
        timer -= Time.deltaTime;
    }
}
