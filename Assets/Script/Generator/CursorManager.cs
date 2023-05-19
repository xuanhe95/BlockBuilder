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
        GroupManager manager = hit.collider.gameObject.GetComponent<GroupManager>();
        print(manager);

        switch (input)
        {

            case 1:
                SetGroup(group, Direction.Left);
                break;
            case 2:
                SetGroup(group, Direction.Right);
                break;
            case 3:
                SetGroup(group, Direction.Forward);
                break;
            case 4:
                SetGroup(group, Direction.Back);
                break;
            case 5:
                SetGroup(group, Direction.Up);
                break;
            case 6:
                SetGroup(group, Direction.Down);
                break;
        }

        if (XPressed)
        {
            print("recursive called");
            Group<GameObject, GameObject> relativeGroup = group.FindRelativeGroup(Direction.Up);
            BSelect(GroupMap[relativeGroup], 10);
        }


    }

    public void SetGroup(Group<GameObject, GameObject> group, int direction)
    {
        Group<GameObject, GameObject> relativeGroup = group.FindRelativeGroup(direction);
        Debug.Log(relativeGroup);
        if (relativeGroup != null)
        {
            if (currentTypes[currentSelection] != null)
            {
                relativeGroup.SetType(currentTypes[currentSelection]);
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
        if (manager == null || depth == 0) return;
        visited.Clear();
        Queue<GroupManager> q = new Queue<GroupManager>();
        //visited.Add(manager);
        q.Enqueue(manager);
        int size = q.Count;
        int level = 0;
        while (q.Count > 0 && level < depth)
        {
            for (int i = 0; i < size; i++)
            {
                GroupManager gm = q.Dequeue();
                
                if (visited.Contains(gm) || gm.GetGroup().GetTypes() == GeoMap[(int)Geo.Empty]) continue;
                gm.SetEmpty();
                gm.Select(rd);

                for (int dir = 0; dir < 5; dir++)
                {
                    Group<GameObject, GameObject> relative = gm.GetGroup().FindRelativeGroup(dir);
                    GroupManager relativeManager = GroupMap[relative];
                    q.Enqueue(relativeManager);
                }
                visited.Add(gm);
            }

            size = q.Count;
            level++;
        }
        
    }




}
