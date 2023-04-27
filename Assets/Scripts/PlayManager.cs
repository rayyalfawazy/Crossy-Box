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
    Dictionary<int, Terrain> activeTerrain = new Dictionary<int, Terrain>(20); // Untuk Menentukan isi zPos dengan terrain (Dictionary)

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
        for (int zPos = backViewDistance; zPos < initialGrassCount; zPos++) 
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
        for (int zPos = initialGrassCount; zPos < frontViewDistance; zPos++)
        {
            var terrain = SpawnRandomTerrain(zPos);
            //var randomIndex = Random.Range(0, terrainList.Count); //Generate Random Index hingga Jumlah Terrain
            //var terrain = Instantiate(terrainList[randomIndex]); //Generate Random Terrain sesuai index yang ditentukan

            terrain.Generate(horizontalSize);
            activeTerrain[zPos] = terrain;
        }
        SpawnRandomTerrain(0);
    }

    private Terrain SpawnRandomTerrain(int zPos)
    {
        Terrain terrainCheck = null;
        Terrain terrain = null;
        int randomIndex;

        for(int z = -1; z >= -3; z--) 
        {
            var checkPos = zPos + z;
            if (terrainCheck == null)
            {
                terrainCheck = activeTerrain[checkPos];
                continue;
            }
            else if (terrainCheck.GetType() != activeTerrain[checkPos].GetType())
            {
                randomIndex = Random.Range(0, terrainList.Count);
                terrain = Instantiate(terrainList[randomIndex]);
                terrain.transform.position = new Vector3(0, 0, zPos);
                return terrain;
            }
            else
            {
                continue;
            }
        }
        
        var candidateTerrain = new List<Terrain>(terrainList);
        for (int i = 0; i < candidateTerrain.Count; ++i)
        {
            if (terrainCheck.GetType() == candidateTerrain[i].GetType())
            {
                candidateTerrain.Remove(candidateTerrain[i]);
                break;
            }
        }

        randomIndex = Random.Range(0,candidateTerrain.Count);
        terrain = Instantiate(candidateTerrain[randomIndex]);
        terrain.transform.position = new Vector3(0,0, zPos);
        return terrain;
    }
}
