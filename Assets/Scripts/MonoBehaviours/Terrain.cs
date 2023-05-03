using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    protected int horizontalSize;

    public virtual void Generate(int size)
    {
        horizontalSize = size;

        if (size == 0)
        {
            return;
        }

        if ((float)size % 2 == 0)
        {
            size -= 1;
        }

        int moveLimit = Mathf.FloorToInt((float)size / 2);
        for (int i = -moveLimit; i <= moveLimit; i++)
        {
            SpawnTile(i);
        }
        var LeftBoundaryTile = SpawnTile(-moveLimit - 1);
        var RightBoundaryTile = SpawnTile(moveLimit + 1);
        DarkenObject(LeftBoundaryTile);
        DarkenObject(RightBoundaryTile);
    }

    GameObject SpawnTile(int xPos)
    {
        var gobj = Instantiate(tilePrefab, transform);
        gobj.transform.localPosition = new Vector3(xPos, 0, 0);
        return gobj;
    }

    void DarkenObject(GameObject gobj)
    {
        var renderers = gobj.GetComponentsInChildren<MeshRenderer>(includeInactive:true);
        foreach (var rend in renderers) 
        {
            rend.material.color *= Color.gray;
        }
    }
}
