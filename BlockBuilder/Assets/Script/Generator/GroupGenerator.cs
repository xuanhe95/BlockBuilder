using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator
{

    private Type<GameObject> Waters;
    private Type<GameObject> Sands;
    private Type<GameObject> Lands;
    private Type<GameObject> Trees;
    private Type<GameObject> Emptys;
    private List<Type<GameObject>> Meshes;

    private GeoPicker picker;

    public void GenerateMeshs()
    {
        picker = new GeoPicker();
        Emptys = new Type<GameObject>(picker.ReadListFrom(Empty));
        Waters = new Type<GameObject>(picker.ReadListFrom(Water));
        Sands = new Type<GameObject>(picker.ReadListFrom(Sand));
        Lands = new Type<GameObject>(picker.ReadListFrom(Land));
        Trees = new Type<GameObject>(picker.ReadListFrom(Tree));

        Meshes = new List<Type<GameObject>>();

        //Meshes.Add(Emptys);
        Meshes.Add(Waters);
        Meshes.Add(Sands);
        Meshes.Add(Lands);
        Meshes.Add(Trees);

    }
}
