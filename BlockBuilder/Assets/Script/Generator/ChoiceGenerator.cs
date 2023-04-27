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




        foreach(GameObject go in Choices)
        {
            choices.Add(go);
        }
        return choices;
    }
}
