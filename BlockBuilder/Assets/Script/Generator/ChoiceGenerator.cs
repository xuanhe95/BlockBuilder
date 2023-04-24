using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    Choice<GameObject> ChoiceGenerator()
    {
        Choice<GameObject> choices = new Choice<GameObject>();
        foreach(GameObject go in Choices)
        {
            choices.Add(go);
        }
        return choices;
    }
}
