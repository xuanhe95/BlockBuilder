using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public Group<GameObject, GameObject> thisGroup;

    public void SetGroup(Group<GameObject, GameObject> groupIn)
    {
        thisGroup = groupIn;
    }
    
}
