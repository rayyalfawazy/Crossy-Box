using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Grass grassPrefab;
    [SerializeField] Road roadPrefab;
    [SerializeField] int initialGrassCount = 5;
    [SerializeField] int horizontalSize;
    [SerializeField] int backRelativePos = -5; // Spawn Line ke Belakang

    private void Start()
    {
        for (int zPos = backRelativePos; zPos < initialGrassCount; ++zPos) 
        {
            var grass = Instantiate(grassPrefab);
            grass.transform.localPosition = new Vector3(0,0,zPos);
            grass.SetTreePercentage(zPos < -1 ? 1 : 0); //Jika zPos dibawah 0 maka spawn semua pohon.
            grass.Generate(horizontalSize);
        }
    }
}
