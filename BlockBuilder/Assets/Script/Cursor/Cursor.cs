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

    private Stack<Group<GameObject, GameObject>> History =
        new Stack<Group<GameObject, GameObject>>();

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
                ResetPreview();

                if (PrepareCurrent(group.FindRelativeGroup(Direction.Up)))
                {
                    InstancePreview(currentSelection, currentTypes, currentGroup);
                }
            }

            if (Input.GetMouseButtonDown(2) && currentTypes.Count != 0)
            {
                ResetPreview();
                TogglePreview(1);
            }
            lastHit = hit.collider.gameObject;
        }
    }

    // public void SetCursor(int input)
    // {
    //     coord coord = lastHit.GetCoord();
    //     int width = LevelGenerator.instance.width;
    //     int height = LevelGenerator.instance.height;
    //
    //     switch (input)
    //     {
    //         case 0:
    //             //remove grid
    //             lastHit.SetDisable();
    //             break;
    //         case 1:
    //             //add X+
    //             if (coord.x < width - 1)
    //             {
    //                 LevelGenerator.instance.gridElements[coord.x + width * (coord.z + width * coord.y) + 1].SetEnable();
    //             }
    //             break;
    //         case 2:
    //             //add X-
    //             if (coord.x > 0)
    //             {
    //                 LevelGenerator.instance.gridElements[coord.x + width * (coord.z + width * coord.y) - 1].SetEnable();
    //             }
    //             break;
    //         case 3:
    //             //add Y+
    //             if (coord.y < height - 1)
    //             {
    //                 LevelGenerator.instance.gridElements[coord.x + width * (coord.z + width * (1+coord.y))].SetEnable();
    //             }
    //             break;
    //
    //         case 4:
    //             //add Y-
    //             if (coord.y > 0)
    //             {
    //                 LevelGenerator.instance.gridElements[coord.x + width * (coord.z + width * (coord.y-1))].SetEnable();
    //             }
    //
    //             break;
    //         case 5:
    //             //add Z+
    //             if (coord.z < width - 1)
    //             {
    //                 LevelGenerator.instance.gridElements[coord.x + width * (coord.z + 1 + width * coord.y)].SetEnable();
    //             }
    //             break;
    //         case 6:
    //             //add Z-
    //             if (coord.z > 0)
    //             {
    //                 LevelGenerator.instance.gridElements[coord.x + width * (coord.z - 1 + width * coord.y)].SetEnable();
    //             }
    //             break;
    //     }
    // }

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

                upGroup.Select(rd);
                // print(upGroup.GetChoices());
                // print(upGroup.GetChoices().GetType());
                //
                int index = 0;
                List<Type<GameObject>> AllType = upGroup.GetChoicesSet();
                List<GameObject> SelectedGroup = AllType[index].GetObjects();
                Type<GameObject> CurrentSelection = AllType[index];

                group.SetType(CurrentSelection);
                for (int i = 1; i < 5; i++)
                {
                    Instantiate(
                        SelectedGroup[i],
                        upGroup.GetUnit(0).GetVector().transform.position,
                        Quaternion.identity
                    );
                    // List<GameObject> AllGO = new List<GameObject>();
                    // foreach (Type<GameObject> thisType in AllType)
                    // {
                    //     foreach (GameObject go in thisType.GetObjects())
                    //     {
                    //         Instantiate(go,upGroup.get)
                    //     }
                    // }
                }
                //PushToHistory(upGroup);

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

    private void PushToHistory(Group<GameObject, GameObject> ThisGroup)
    {
        History.Push(ThisGroup);
    }
}
