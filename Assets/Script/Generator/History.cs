using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    private Stack<GroupManager> History =
        new Stack<GroupManager>();

    private void PushToHistory(GroupManager manager)
    {
        History.Push(manager);
    }

    private void Withdraw(){
        if(History.Count == 0) return;
        GroupManager lastManager = History.Pop();
        lastManager.SetEmpty();
    }
}
