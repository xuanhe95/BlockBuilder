using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator
{

    private Dictionary<int, Type<GameObject>> GeoMap;
    private Dictionary<int ,GameObject> GoMap;
    private Dictionary<GameObject, int> IdxMap;
    private Dictionary<GameObject, Type<GameObject>> ModMap;

    private GeoPicker picker;
    public List<GameObject> Models;

    public void GenerateMeshs()
    {
        // GameObject ModelsGO = new GameObject();
        // ModelsGO.name = "Models";
        picker = new GeoPicker();
        GeoMap = new Dictionary<int, Type<GameObject>>();
        GoMap = new Dictionary<int, GameObject>();
        IdxMap = new Dictionary<GameObject, int>();
        ModMap = new Dictionary<GameObject, Type<GameObject>>();

        int index = 0;
        foreach(GameObject go in Models)
        {
            //go.transform.SetParent(ModelsGO.transform);
            //go.name = index.ToString();
            GeoMap.Add(index, Pick(go, index));
            GoMap.Add(index, go);
            IdxMap.Add(go, index);
            ModMap.Add(go, GeoMap[index]);
            Debug.Log(go.name + " " + index);
            index++;
        }
    }

    public Type<GameObject> Pick(GameObject go, int index)
    {
        return new Type<GameObject>(picker.ReadListFrom(go), index);
    }

    public Dictionary<int, Type<GameObject>> GetGeo()
    {
        return GeoMap;
    }
}
