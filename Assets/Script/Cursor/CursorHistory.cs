using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Cursor : MonoBehaviour
{

    private Stack<Group<GameObject, GameObject>> History =
        new Stack<Group<GameObject, GameObject>>();

    private void PushToHistory(Group<GameObject, GameObject> ThisGroup)
    {
        History.Push(ThisGroup);
    }

    private void Withdraw(){
        Group<GameObject, GameObject> lastGroup = History.Pop();
        lastGroup.SetEmpty();
    }
}
