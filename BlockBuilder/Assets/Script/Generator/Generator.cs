using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class Generator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //rd = new System.Random();
        GenerateMeshs();
        GenerateRules();

        LevelBuilder(36, 30, 16);

        //Debug.Log(levels.Count);
        // PrintRules();
        foreach (Level<GameObject, GameObject> level in levels)
        {
            //Debug.Log(level.ID);
            //PrintRules(level);
        }
        Instantiator();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Instantiator();
        }
    }

    // IEnumerator UpdateIns()
    // {
    //     yield return new WaitForSeconds(0);
    //
    // }
}
