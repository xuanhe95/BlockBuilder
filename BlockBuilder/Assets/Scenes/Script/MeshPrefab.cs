using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshPrefab : MonoBehaviour
{
    private MeshFilter meshFilter;
    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        List<Vector3> list = new List<Vector3>();
        list.Add(new Vector3(0,0,0));
        list.Add(new Vector3(0,1,0));
        list.Add(new Vector3(1,1,0));
        list.Add(new Vector3(1,0,0));
        Initialize(list);
    }

    // Start is called before the first frame update
    void Initialize(List<Vector3> inputPos)
    {
        Mesh myMesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            vertices[i] = inputPos[i];
        }
        myMesh.vertices = vertices;
        
        int[] triangles = new int[6]
        {
            0, 1, 2,
            2, 3, 0
        };
        
        myMesh.triangles = triangles;

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };

        myMesh.uv = uv;

        myMesh.RecalculateNormals();
        meshFilter.mesh = myMesh;
    }
    
    
    
    
    

}
