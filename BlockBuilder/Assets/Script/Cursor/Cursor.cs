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

        //print(group);

        switch (input)
        {
            // case 0:

            // //remove grid

            // lastHit.SetDisable();

            // break;

            case 1:
                Group<GameObject, GameObject> leftGroup = group.FindRelativeGroup(Direction.Left);
                if (leftGroup == null)
                    break;
                PushToHistory(leftGroup);
                leftGroup.Select(rd);

                break;

            case 2:

                Group<GameObject, GameObject> rightGroup = group.FindRelativeGroup(Direction.Right);
                if (rightGroup == null)
                    break;
                PushToHistory(rightGroup);
                rightGroup.Select(rd);

                break;

            case 3:

                Group<GameObject, GameObject> forwardGroup = group.FindRelativeGroup(
                    Direction.Forward
                );
                if (forwardGroup == null)
                    break;
                PushToHistory(forwardGroup);
                forwardGroup.Select(rd);
                break;

            case 4:

                Group<GameObject, GameObject> backGroup = group.FindRelativeGroup(Direction.Back);
                if (backGroup == null)
                    break;
                backGroup.Select(rd);
                PushToHistory(backGroup);
                break;

            case 5:
                Group<GameObject, GameObject> upGroup = group.FindRelativeGroup(Direction.Up);
                if (upGroup == null)
                    break;
                if(currentTypes[currentSelection] != null){
                    upGroup.SetType(currentTypes[currentSelection]);
                }

                
                
                PushToHistory(upGroup);

                break;

            case 6:
                Group<GameObject, GameObject> downGroup = group.FindRelativeGroup(Direction.Down);
                if (downGroup == null)
                    break;
                downGroup.Select(rd);
                PushToHistory(downGroup);
                break;
        }
    }


}
