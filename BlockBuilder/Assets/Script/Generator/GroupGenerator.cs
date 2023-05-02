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
    private Type<GameObject> Bridges;

    private List<Type<GameObject>> Meshes;

    private Dictionary<int, Type<GameObject>> GeoMap;

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
        GeoMap = new Dictionary<int, Type<GameObject>>();

        GeoMap.Add((int)Geo.Empty, Pick(Empty));
        GeoMap.Add((int)Geo.Water, Pick(Water));
        GeoMap.Add((int)Geo.Sand, Pick(Sand));
        GeoMap.Add((int)Geo.Land, Pick(Land));
        GeoMap.Add((int)Geo.Tree, Pick(Tree));
        GeoMap.Add((int)Geo.Bridge, Pick(Bridge));
        GeoMap.Add((int)Geo.T1, Pick(Terrace1));
        GeoMap.Add((int)Geo.T2, Pick(Terrace2));

        Debug.Log(GeoMap[(int)Geo.Empty]);

        //Meshes.Add(Emptys);
        Meshes.Add(Waters);
        Meshes.Add(Sands);
        Meshes.Add(Lands);
        Meshes.Add(Trees);
    }

    public Type<GameObject> Pick(GameObject go)
    {
        return new Type<GameObject>(picker.ReadListFrom(go));
    }

    public Dictionary<int, Type<GameObject>> GetGeo()
    {
        return GeoMap;
    }
}
