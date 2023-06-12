using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    private GameObject lastHit;
    private RectTransform rectTransform;
    System.Random rd;
    private bool XPressed = false;

    private Dictionary<Group<GameObject, GameObject>, GroupManager> GroupMap =
        new Dictionary<Group<GameObject, GameObject>, GroupManager>();

    private HashSet<GroupManager> visited = new HashSet<GroupManager>();

    // Start is called before the first frame update
    void CursorStart()
    {
        rd = new System.Random();
        rectTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScroll();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Group"))
        {
            this.transform.position = hit.collider.transform.position;
            Group<GameObject, GameObject> group = hit.collider.gameObject
                .GetComponent<GroupCollider>()
                .thisGroup;
            //GroupManager manager = hit.collider.gameObject.GetComponent<GroupManager>();
            //if(manager !=null) RecursiveSelect(manager, 1);
            if (hit.collider.gameObject != lastHit)
            {
                ResetPreview(true);
                if (PrepareCurrent(group.FindRelativeGroup(Direction.Up)))
                {
                    InstancePreview(currentSelection, currentTypes, currentGroup);
                }
            }

            lastHit = hit.collider.gameObject;
        }


        if (Input.GetMouseButtonDown(1))
        {
            Withdraw();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            XPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            XPressed = false;
        }

    }

    public void SetCursor(int input)
    {
        print("SetCursorCalled");
        Group<GameObject, GameObject> group = lastHit.GetComponent<GroupCollider>().thisGroup;
        print(group);
        GroupManager manager = hit.collider.gameObject.GetComponent<GroupManager>();
        print(manager);

        int dir = 0;

        switch (input)
        {

            case 1:
                dir = Direction.Left;
                break;
            case 2:
                dir = Direction.Right;
                break;
            case 3:
                dir = Direction.Forward;
                break;
            case 4:
                dir = Direction.Back;
                break;
            case 5:
                dir = Direction.Up;
                break;
            case 6:
                dir = Direction.Down;
                break;
        }
        Group<GameObject, GameObject> relativeGroup = group.FindRelativeGroup(dir);
        if (true)
        {
            print("recursive called");
            
            Debug.Log(GroupMap[relativeGroup]);
            BSelect(GroupMap[relativeGroup], 2);
        }
        else{
            SetGroup(group, dir);
        }


    }

    public void SetGroup(Group<GameObject, GameObject> group, int direction)
    {
        Group<GameObject, GameObject> relativeGroup = group.FindRelativeGroup(direction);
        Debug.Log(relativeGroup);
        Debug.Log(currentSelection);
        if (relativeGroup != null)
        {
            if (currentTypes[currentSelection] != null)
            {
                relativeGroup.SetType(currentTypes[currentSelection]);
                //GroupMap[relativeGroup].Select();
            }

            PushToHistory(GroupMap[relativeGroup]);
        }

    }

    public void RecursiveSelect(GroupManager manager, int depth = 1)
    {
        if (manager == null) return;
        print("recursive " + depth);
        if (depth == 0) return;
        visited.Add(manager);
        
        for (int dir = 0; dir < 5; dir++)
        {
            Group<GameObject, GameObject> group = manager.GetGroup();
            Group<GameObject, GameObject> relative = group.FindRelativeGroup(dir);
            if (relative == null) continue;
            GroupManager relativeManager = GroupMap[relative];
            if (relativeManager == null || visited.Contains(relativeManager)) continue;
            if (relativeManager.GetGroup().GetTypes() == GeoMap[(int)Geo.Empty]) continue;
            relativeManager.SetEmpty();
            relativeManager.Select(rd);
            RecursiveSelect(relativeManager, depth - 1);
            PushToHistory(relativeManager);
        }
    }

    public void BSelect(GroupManager manager, int depth = 1)
    {
        if (manager == null || depth == 0){
            Debug.Log("BSelect return");
            return;
        }
        bool flag = false;
        visited.Clear();
        Queue<GroupManager> q = new Queue<GroupManager>();
        //visited.Add(manager);
        q.Enqueue(manager);
        int size = q.Count;
        int level = 0;
        Debug.Log("BSelect");
        while (q.Count > 0 && level < depth)
        {
            for (int i = 0; i < size; i++)
            {
                GroupManager gm = q.Dequeue();
                Debug.Log("Recursive " + gm.GetGroup().GetTypes());
                Debug.Log(flag);
                if (flag && (visited.Contains(gm) || gm.GetGroup().GetTypes() == GeoMap[(int)Geo.Empty])) continue;
                
                flag = true;
                Debug.Log(flag);
                visited.Add(gm);
                gm.SetEmpty();
                //gm.Select(rd);

                Group<GameObject, GameObject> group = gm.GetGroup();
                Group<GameObject, GameObject> down = group.FindRelativeGroup(Direction.Down);
                gm.Select(GroupMap[down]);

                for (int dir = 0; dir < 5; dir++)
                {
                    Group<GameObject, GameObject> relative = gm.GetGroup().FindRelativeGroup(dir);
                    GroupManager relativeManager = GroupMap[relative];
                    q.Enqueue(relativeManager);
                }
                
            }

            size = q.Count;
            level++;
        }
        
    }




}
