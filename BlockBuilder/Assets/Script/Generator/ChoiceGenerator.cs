using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    Choice<GameObject> ChoiceGenerator()
    {
        Choice<GameObject> choices = new Choice<GameObject>();
        choices.Add(GeoMap[(int)Geo.Water]);
        choices.Add(GeoMap[(int)Geo.Sand], 2);
        choices.Add(GeoMap[(int)Geo.Land], 5);
        choices.Add(GeoMap[(int)Geo.Tree], 4);
        // foreach(Type<GameObject> type in Meshes)
        // {
        //     Debug.Log("Added " + type);
        //     choices.Add(type);
        // }
        return choices;
    }
}
