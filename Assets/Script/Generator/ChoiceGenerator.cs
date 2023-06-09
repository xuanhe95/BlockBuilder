using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    Choice<GameObject> ChoiceGenerator()
    {
        Choice<GameObject> choices = new Choice<GameObject>();
        //choices.Add(GeoMap[(int)Geo.Water]);
        //choices.Add(GeoMap[(int)Geo.Sand], 2);
        //choices.Add(GeoMap[(int)Geo.Land], 5);
        //choices.Add(GeoMap[(int)Geo.Tree], 4);
        for(int i = 0; i < GeoMap.Count; i++)
        {
            choices.Add(GeoMap[i], 1);
            //Debug.Log("Added " + GeoMap[i]);
        }

        return choices;
    }
}
