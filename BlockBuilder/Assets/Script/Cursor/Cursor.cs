using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
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
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Group"))
        {
            //print(hit.collider.gameObject);
            this.transform.position = hit.collider.transform.position;
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

                group.FindRelativeGroup(group,Direction.Left).Select(rd);

                break;

            case 2:
                

                group.FindRelativeGroup(group,Direction.Right).Select(rd);

                break;

            case 3:
                

                group.FindRelativeGroup(group,Direction.Forward).Select(rd);

                break;

            case 4:
                

                group.FindRelativeGroup(group,Direction.Back).Select(rd);

                break;
             case 5:


    
                group.FindRelativeGroup(group,Direction.Up).Select(rd);

                break;

            case 6:

                group.FindRelativeGroup(group,Direction.Down).Select(rd);

                break;

        }

    }
    
    
}
