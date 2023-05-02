using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator
{
        //Base
    public GameObject Water;
    public GameObject Land;
    public GameObject Bridge1;
    public GameObject Bridge2;

    //Mid
    public GameObject Building1;
    public GameObject Building2;
    public GameObject T1A;
    public GameObject T1B;
    public GameObject T1C;
    public GameObject T1D;
    public GameObject T2A;
    public GameObject T2B;
    public GameObject T2C;
    public GameObject T2D;
    

    //Roof
    public GameObject Roof;
    public GameObject Empty;

    public GameObject Sand;
    public GameObject Tree;



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
        GeoMap.Add((int)Geo.Bridge1, Pick(Bridge1));
        GeoMap.Add((int)Geo.Bridge2, Pick(Bridge2));
        GeoMap.Add((int)Geo.T1A, Pick(T1A));
        GeoMap.Add((int)Geo.T1B, Pick(T1B));
        GeoMap.Add((int)Geo.T1C, Pick(T1C));
        GeoMap.Add((int)Geo.T1D, Pick(T1D));
        GeoMap.Add((int)Geo.T2A, Pick(T2A));
        GeoMap.Add((int)Geo.T2B, Pick(T2B));
        GeoMap.Add((int)Geo.T2C, Pick(T2C));
        GeoMap.Add((int)Geo.T2D, Pick(T2D));
        


        //Debug.Log(GeoMap[(int)Geo.Empty]);

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
