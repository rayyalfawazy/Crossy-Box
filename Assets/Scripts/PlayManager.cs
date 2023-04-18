using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Grass grassPrefab;
    [SerializeField] Road roadPrefab;
    [SerializeField] int initialGrassCount = 5;
    [SerializeField] int horizontalSize;

    private void Start()
    {
        for (int i = 0; i < initialGrassCount; ++i) 
        {
            var grass = Instantiate(grassPrefab);
            grass.transform.localPosition = new Vector3(0,0,i);
            grass.Generate(horizontalSize);
        }
    }
}
