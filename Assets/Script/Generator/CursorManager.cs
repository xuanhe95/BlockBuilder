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

    private Dictionary<Group<GameObject, GameObject>, GroupManager> GroupMap = new Dictionary<Group<GameObject, GameObject>, GroupManager>();
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
            GroupManager manager = hit.collider.gameObject.GetComponent<GroupManager>();
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

        if(Input.GetMouseButtonDown(1)){
            Withdraw();
        }
        
    }

    public void SetCursor(int input)
    {
                print("SetCursorCalled");
        Group<GameObject, GameObject> group = lastHit.GetComponent<GroupCollider>().thisGroup;
        GroupManager manager = hit.collider.gameObject.GetComponent<GroupManager>();

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
    }

        public void SetGroup(Group<GameObject, GameObject> group, int direction){
            Group<GameObject, GameObject> relativeGroup = group.FindRelativeGroup(direction);
            Debug.Log(relativeGroup);
            if (relativeGroup != null){
                if(currentTypes[currentSelection] != null){
                    relativeGroup.SetType(currentTypes[currentSelection]);
                }
                PushToHistory(GroupMap[relativeGroup]);
            }

        }

    public void RecursiveSelect(GroupManager manager, int depth = 0){
        if(depth == 0) return;
        visited.Add(manager);
        for(int dir = 0; dir < 6; dir++){
            Group<GameObject, GameObject> relative = manager.GetGroup().FindRelativeGroup(dir);
            if(relative == null) continue;
            GroupManager relativeManager = GroupMap[relative];
            if(relativeManager == null  || visited.Contains(relativeManager)) continue;
            relativeManager.Select(rd);
            RecursiveSelect(relativeManager, depth-1);
            PushToHistory(relativeManager);
        }
    }




}
