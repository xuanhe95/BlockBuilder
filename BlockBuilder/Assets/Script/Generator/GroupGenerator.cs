using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator
{

    private List<GameObject> Waters;
    private List<GameObject> Sands;
    private List<GameObject> Lands;
    private List<GameObject> Trees;
    private List<GameObject> Emptys;
    private List<List<GameObject>> Meshes;

    public void GenerateMeshs()
    {
        Emptys = picker.ReadListFrom(Empty);
        Waters = picker.ReadListFrom(Water);
        Sands = picker.ReadListFrom(Sand);
        Lands = picker.ReadListFrom(Land);
        Trees = picker.ReadListFrom(Tree);

        Meshes = new List<List<GameObject>>();

        //Meshes.Add(Emptys);
        Meshes.Add(Waters);
        Debug.Log(Meshes[0]);
        //Meshes.Add(Sands);
        //Meshes.Add(Lands);
        //Meshes.Add(Trees);

    }
}
