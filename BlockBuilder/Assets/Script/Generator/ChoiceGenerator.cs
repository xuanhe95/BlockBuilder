using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    Choice<GameObject> ChoiceGenerator()
    {
        Choice<GameObject> choices = new Choice<GameObject>();

        //choices.Add(Waters);
        //choices.Add(Sands);
        //choices.Add(Lands);
        //choices.Add(Trees);

        foreach(Type<GameObject> listedType in Meshes)
        {
            if(listedType == null) continue;
            choices.Add(listedType);
        }
        return choices;
    }
}
