using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public Group<Vector3, GameObject> thisGroup;

    public void setGroup(Group<Vector3, GameObject> groupIn)
    {
        thisGroup = groupIn;
    }
    
}
