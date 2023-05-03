using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Grass : Terrain
{
    [SerializeField] List<GameObject> treePrefabList;
    [Range(0,1)] float treeProbability = 0.3f;

    public void SetTreePercentage(float newProbability)
    {
        this.treeProbability = Mathf.Clamp01(newProbability);
    }

    // Dari Script Terrain
    public override void Generate(int size)
    {
        base.Generate(size);
        int limit = Mathf.FloorToInt((float)size / 2);
        int treeCount = Mathf.FloorToInt((float)size*treeProbability);

        // Membuat Daftar Posisi Kosong
        List<int> emptyPosition = new List<int>();
        for (int i = -limit; i <= limit; i++)
        {
            emptyPosition.Add(i);
        }

        for (int i = 0; i < treeCount; i++)
        {
            // Pohon Memilih Posisi Kosong Secara Random
            var randomIndex = Random.Range(0, emptyPosition.Count);
            var pos = emptyPosition[randomIndex];

            // Hapus daru Daftar Posisi Kosong
            emptyPosition.RemoveAt(randomIndex);

            // Spawn Pohon Random
            SpawnRandomTree(pos);
        }
        // Pohon Selalu Di Ujung
        SpawnRandomTree(-limit - 1);
        SpawnRandomTree(limit + 1);
    }

    // Method Spawn Pohon secara Random
    void SpawnRandomTree(int xPos)
    {
        // Pilih Pohon Secara Random
        var randomIndex = Random.Range(0, treePrefabList.Count);
        var prefab = treePrefabList[randomIndex];

        // Set Pohon Ke Posisi Terpilih
        var tree = Instantiate(prefab,
                                new Vector3(xPos, 0, this.transform.position.z),
                                Quaternion.identity,
                                transform);
    }
}
