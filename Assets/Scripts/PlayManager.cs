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

    List<Terrain> terrainList; //Terrain List Initialize
    Dictionary<int, Terrain> activeTerrain = new Dictionary<int, Terrain>(20); // Untuk Menentukan isi zPos dengan terrain

    [SerializeField, Range(0, 1)] float treeProbability;

    private void Start()
    {
        //Terrain List
        terrainList = new List<Terrain>() 
        {
            grassPrefab, 
            roadPrefab
        };

        // Membuat Initial Grass
        for (int zPos = backViewDistance; zPos < initialGrassCount; ++zPos) 
        {
            var grass = Instantiate(grassPrefab);
            grass.transform.localPosition = new Vector3(0,0,zPos);
            grass.SetTreePercentage(zPos < -1 ? 1 : 0); //Jika zPos dibawah -1 maka spawn semua pohon.
            /*
                Pada method ini (Start), terdapat initial grass cound yang menjadi awalan mulai spawn palyer di rumput
                sehingga pohon yang digenerate pada initial grass count adalah 0 atu tidak terdapat pohon.
             */
            grass.Generate(horizontalSize);
            activeTerrain[zPos] = grass;
        }

        // Membuat Line Kedepan Player Randomly
        for (int zPos = initialGrassCount; zPos < frontViewDistance; ++zPos)
        {
            var randomIndex = Random.Range(0, terrainList.Count); //Generate Random Index hingga Jumlah Terrain
            var terrain = Instantiate(terrainList[randomIndex]); //Generate Random Terrain sesuai index yang ditentukan
            terrain.transform.localPosition = new Vector3(0, 0, zPos);
            terrain.Generate(horizontalSize);

            activeTerrain[zPos] = terrain;
        }
    }

    Terrain SpawnTerain(int zPos)
    {
        for(int i = zPos + frontViewDistance; i < 10; ++i) 
        {
            
        }
        return null;
    }
}
