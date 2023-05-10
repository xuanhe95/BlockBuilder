using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Cursor : MonoBehaviour
{
    private RaycastHit hit;

    private Ray ray;

    private GameObject lastHit;

    private RectTransform rectTransform;


    System.Random rd;

    // Start is called before the first frame update
    void Start()
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
            //print(hit.collider.gameObject);
            this.transform.position = hit.collider.transform.position;
            Group<GameObject, GameObject> group = hit.collider.gameObject
                .GetComponent<GroupCollider>()
                .thisGroup;
            GroupManager manager = hit.collider.gameObject.GetComponent<GroupManager>();
            if (hit.collider.gameObject != lastHit)
            {
                ResetPreview(true);

                if (PrepareCurrent(manager.FindRelativeGroup(Direction.Up)))
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

        //print(group);

        switch (input)
        {
            // case 0:

            // //remove grid

            // lastHit.SetDisable();

            // break;

            case 1:
                Group<GameObject, GameObject> leftGroup = manager.FindRelativeGroup(Direction.Left);
                if (leftGroup == null)
                    break;
                if(currentTypes[currentSelection] != null){
                    leftGroup.SetType(currentTypes[currentSelection]);
                }
                PushToHistory(leftGroup);
                
                break;

            case 2:

                Group<GameObject, GameObject> rightGroup = manager.FindRelativeGroup(Direction.Right);
                if (rightGroup == null)
                    break;
                if(currentTypes[currentSelection] != null){
                    rightGroup.SetType(currentTypes[currentSelection]);
                }
                PushToHistory(rightGroup);
                
                break;

            case 3:

                Group<GameObject, GameObject> forwardGroup = manager.FindRelativeGroup(Direction.Forward);
                if (forwardGroup == null)
                    break;
                if(currentTypes[currentSelection] != null){
                    forwardGroup.SetType(currentTypes[currentSelection]);
                }
                PushToHistory(forwardGroup);
                
                break;

            case 4:

                Group<GameObject, GameObject> backGroup = manager.FindRelativeGroup(Direction.Back);
                if (backGroup == null)
                    break;
                if(currentTypes[currentSelection] != null){
                    backGroup.SetType(currentTypes[currentSelection]);
                }
                PushToHistory(backGroup);
                
                break;

            case 5:
                Group<GameObject, GameObject> upGroup = manager.FindRelativeGroup(Direction.Up);
                if (upGroup == null)
                    break;
                if(currentTypes[currentSelection] != null){
                    upGroup.SetType(currentTypes[currentSelection]);
                }
                PushToHistory(upGroup);

                break;

            case 6:
                Group<GameObject, GameObject> downGroup = manager.FindRelativeGroup(Direction.Down);
                if (downGroup == null)
                    break;
                if(currentTypes[currentSelection] != null){
                    downGroup.SetType(currentTypes[currentSelection]);
                }
                PushToHistory(downGroup);

                break;
        }
    }


}
