using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] List<Terrain> terrainList; //Terrain List Initialize (Elemen 0 harus Grass-chan)

    [SerializeField] int initialGrassCount;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance; // Spawn Line ke Belakang
    [SerializeField] int frontViewDistance; // Spawn Line ke Depan (Relative terhadap player)
    [SerializeField] int travelDistance;

    Dictionary<int, Terrain> activeTerrain = new Dictionary<int, Terrain>(20); // Untuk Menentukan isi zPos dengan terrain (Dictionary)

    public UnityEvent<int, int> OnUpdateTerrainLimit; // Untuk pembatas gerak player terhadap Terain

    private void Start()
    {
        // Membuat Initial Grass
        for (int zPos = backViewDistance; zPos < initialGrassCount; zPos++) 
        {
            /*
                Pada method ini (Start), terdapat initial grass cound yang menjadi awalan mulai spawn palyer di rumput
                sehingga pohon yang digenerate pada initial grass count adalah 0 atu tidak terdapat pohon.
             */

            var terrain = Instantiate(terrainList[0]);
            terrain.transform.localPosition = new Vector3(0, 0, zPos);

            //Periksa apakah terrain adalah Grass-chan
            if (terrain is Grass grass)
            {
                grass.SetTreePercentage(zPos < -1 ? 1 : 0);//Jika zPos dibawah -1 maka spawn semua pohon.
            }
            terrain.Generate(horizontalSize);
            activeTerrain[zPos] = terrain;
        }

        // Membuat Line Kedepan Player Randomly
        for (int zPos = initialGrassCount; zPos < frontViewDistance; zPos++)
        {
            SpawnRandomTerrain(zPos);
        }

        OnUpdateTerrainLimit.Invoke(horizontalSize, travelDistance + backViewDistance);
    }

    private Terrain SpawnRandomTerrain(int zPos)
    {
        Terrain terrainCheck = null;
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
                return SpawnTerrain(terrainList[randomIndex], zPos);
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
        return SpawnTerrain(candidateTerrain[randomIndex], zPos);
    }

    public void DestroyBehind()
    {
        var destroyPos = travelDistance - 1 + backViewDistance;
        Destroy(activeTerrain[destroyPos].gameObject);
        activeTerrain.Remove(destroyPos);
    }

    public Terrain SpawnTerrain(Terrain terrain, int zPos)
    {
        terrain = Instantiate(terrain);
        terrain.transform.position = new Vector3(0, 0, zPos);
        terrain.Generate(horizontalSize);
        activeTerrain[zPos] = terrain;
        return terrain;
    }

    public void UpdateTravelDistance(Vector3 targetPosition)
    {
        if (targetPosition.z > travelDistance)
        {
            travelDistance = Mathf.CeilToInt(targetPosition.z);
            UpdateTerrain();
        }
    }

    public void UpdateTerrain()
    {
        DestroyBehind();

        var spawnPos = travelDistance - 1 + frontViewDistance;
        SpawnRandomTerrain(spawnPos);

        // Pembatas gerak player terhadap Terain
        OnUpdateTerrainLimit.Invoke(horizontalSize, travelDistance + backViewDistance);
    }
}
