using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Grass grassPrefab;
    [SerializeField] Road roadPrefab;
    [SerializeField] int initialGrassCount = 5;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance = -5; // Spawn Line ke Belakang
    [SerializeField] int frontViewDistance = 7; // Spawn Line ke Depan (Relative terhadap player)

    private void Start()
    {
        // Membuat Initial Grass
        for (int zPos = backViewDistance; zPos < initialGrassCount; ++zPos) 
        {
            var grass = Instantiate(grassPrefab);
            grass.transform.localPosition = new Vector3(0,0,zPos);
            grass.SetTreePercentage(zPos < -1 ? 1 : 0); //Jika zPos dibawah 0 maka spawn semua pohon.
            grass.Generate(horizontalSize);
        }

        // Membuat Line Kedepan Player
        for (int zPos = initialGrassCount; zPos < frontViewDistance; ++zPos)
        {
            var terrain = Instantiate(roadPrefab);
            terrain.transform.localPosition = new Vector3(0, 0, zPos);
            terrain.Generate(horizontalSize);
        }
    }
}
