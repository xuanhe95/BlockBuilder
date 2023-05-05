using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private RaycastHit hit;

    private Ray ray;

    private GameObject lastHit;

    private RectTransform rectTransform;

    private Stack<Group<GameObject, GameObject>> History =
        new Stack<Group<GameObject, GameObject>>();

    System.Random rd;
    private int currentSelection = 0;
    private List<Type<GameObject>> currentTypes; 
    private Group<GameObject, GameObject> currentGroup; 
    private GameObject Preview;

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

            if (hit.collider.gameObject != lastHit)
            {
                ResetPreview();
                PrepareCurrent(hit.collider.gameObject.GetComponent<GroupCollider>().thisGroup);
                InstancePreview(currentSelection,currentTypes,currentGroup);
            }

            if (Input.GetMouseButtonDown(2))
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
                Group<GameObject, GameObject> leftGroup = group.FindRelativeGroup(
                    group,
                    Direction.Left
                );
                if (leftGroup == null)
                    break;
                PushToHistory(leftGroup);
                leftGroup.Select(rd);

                break;

            case 2:

                Group<GameObject, GameObject> rightGroup = group.FindRelativeGroup(
                    group,
                    Direction.Right
                );
                if (rightGroup == null)
                    break;
                PushToHistory(rightGroup);
                rightGroup.Select(rd);

                break;

            case 3:

                Group<GameObject, GameObject> forwardGroup = group.FindRelativeGroup(
                    group,
                    Direction.Forward
                );
                if (forwardGroup == null)
                    break;
                PushToHistory(forwardGroup);
                forwardGroup.Select(rd);
                break;

            case 4:

                Group<GameObject, GameObject> backGroup = group.FindRelativeGroup(
                    group,
                    Direction.Back
                );
                if (backGroup == null)
                    break;
                backGroup.Select(rd);
                PushToHistory(backGroup);
                break;

            case 5:
                Group<GameObject, GameObject> upGroup = group.FindRelativeGroup(
                    group,
                    Direction.Up
                );
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
                Group<GameObject, GameObject> downGroup = group.FindRelativeGroup(
                    group,
                    Direction.Down
                );
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

    private void PrepareCurrent(Group<GameObject,GameObject> group)
    {
        currentSelection = 0;
        currentGroup = group;
        currentTypes = group.GetChoicesSet();
        print(currentTypes.Count);
    }

    private void InstancePreview(int index, List<Type<GameObject>> types, Group<GameObject, GameObject> group)
    {
        if (types != null)
        {
            Preview = new GameObject();
            Type<GameObject> type = types[index];
            foreach (var go in type.GetObjects())
            {
                Instantiate(go, Vector3.zero, Quaternion.identity).transform.SetParent(Preview.transform);
                Preview.transform.position = group.GetUnit(0).GetVector().transform.position;
            }
        }
        
    }

    private void ResetPreview()
    {
        Destroy(Preview);
        currentSelection = 0;
    }

    private void TogglePreview(int select)
    {
        switch (select)
        {
            case 0:currentSelection += 1;
                InstancePreview(currentSelection, currentTypes, currentGroup);
                break;
            case 1:currentSelection += -1;
                InstancePreview(currentSelection, currentTypes, currentGroup);
                break;
        }
    }



    //鼠标移动至新物体 - 调用方法返回type list
    //鼠标移动至新物体 - 清除之前的，调用方法instantiate上述type list中的一个，序号为index
    //鼠标滚轮 - index +1/-1，清除之前的，调用方法instantiate上述type list中的一个
    //鼠标点击 - 清除之前预览，将当前type返还
    
}
