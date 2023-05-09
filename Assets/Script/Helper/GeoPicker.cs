using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoPicker
{
    // public GameObject Obj;

    // GeoPicker(GameObject go)
    // {
    //     Obj = go;
    // }
    
    // public void Start()
    // {
    //     List<GameObject>go = ReadListFromGeo(Obj);
    //     foreach (var VARIABLE in go)
    //     {
    //         Instantiate(VARIABLE, transform.position, Quaternion.identity);
    //     }
    // }

    public List<GameObject> ReadListFrom(GameObject GOtoRead)
    {
        List<GameObject> meshGO = new List<GameObject>();
        GameObject thisGroup = new GameObject();
        thisGroup.name = GOtoRead.name;
        meshGO.Add(thisGroup);
        foreach (Transform child in GOtoRead.transform)
        {
            // GameObject thisUnit = new GameObject();
            // thisUnit.transform.name = GOtoRead.name + "_" + (child.GetSiblingIndex()+1).ToString();
            // MeshFilter filter = thisUnit.AddComponent<MeshFilter>();
            // MeshRenderer renderer = thisUnit.AddComponent<MeshRenderer>();
            // renderer.material = mat;
            // filter.sharedMesh = child.GetComponent<MeshFilter>().sharedMesh;
            // meshDic.Add(child.GetSiblingIndex(), child.GetComponent<MeshFilter>().sharedMesh);
            meshGO.Add(child.gameObject);
        }

        return meshGO;
    }
}
