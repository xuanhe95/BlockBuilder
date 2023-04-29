using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    Choice<GameObject> ChoiceGenerator()
    {
        Choice<GameObject> choices = new Choice<GameObject>();
        choices.Add(Waters);
        choices.Add(Lands, 3);
        choices.Add(Trees, 3);
        choices.Add(Sands, 10);
        // foreach(Type<GameObject> type in Meshes)
        // {
        //     Debug.Log("Added " + type);
        //     choices.Add(type);
        // }
        return choices;
    }
}
