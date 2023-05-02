using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class Generator : MonoBehaviour
{
    //Base
    public GameObject Water;
    public GameObject Land;
    public GameObject Bridge;

    //Mid
    public GameObject Building1;
    public GameObject Building2;
    public GameObject Terrace1;
    public GameObject Terrace2;

    //Roof
    public GameObject Roof;
    public GameObject Empty;

    public GameObject Sand;
    public GameObject Tree;

    // Start is called before the first frame update
    void Start()
    {
        //rd = new System.Random();
        GenerateMeshs();
        GenerateRules();

        LevelBuilder(28, 20, 16);

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
