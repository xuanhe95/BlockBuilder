using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class Generator : MonoBehaviour
{
    public int width = 1000;

    public int length = 1000;

    public int levelsNumber = 16;
    // Start is called before the first frame update
    void Start()
    {
        GenerateMeshs();
        GenerateRules();    // 创建规则
        LevelBuilder(width, length, levelsNumber);    // 创建levels
        CursorStart();
        InitGroupManager(); // 生成并挂载GroupManager
        Instantiator();
    }
        // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Instantiator();
        }
        if(Input.GetMouseButtonUp(1)){
            Instantiator();
        }
        if (Input.GetMouseButtonUp(2))
        {
            Instantiator();
        }

        // if (Input.GetKeyUp(KeyCode.X))
        // {
        //     print("xPressed");
        //     RecursiveSelect();
        // }
    }

}
