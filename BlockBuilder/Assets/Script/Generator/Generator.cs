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

    Rule<GameObject> baseRule = new Rule<GameObject>(); 
    Rule<GameObject> midRule = new Rule<GameObject>();

    public List<List<GameObject>> ListedChoices;


    //public GameObject Empty;
    public GameObject Ground;
    //public GameObject GroupType;
    private List<Level<GameObject, GameObject>> levels = new List<Level<GameObject, GameObject>>();


    private List<GameObject> InstantiatedGo = new List<GameObject>();
    private List<Unit<GameObject, GameObject>> InstantiatedUnit = new List<Unit<GameObject, GameObject>>();
    public GameObject meshAll;
    private Dictionary<int, Mesh> meshDic = new Dictionary<int, Mesh>();
    private GeoPicker picker;
    // Start is called before the first frame update
    void Start()
    {
        picker = new GeoPicker();

        //rd = new System.Random();
        GenerateMeshs();
        GenerateRules();

        LevelBuilder(10,12,8);
        
        //Debug.Log(levels.Count);
        // PrintRules();
        foreach(Level<GameObject, GameObject> level in levels)
        {
            //Debug.Log(level.ID);
            //PrintRules(level);
        }
        Instantiator();

    }


    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetMouseButtonUp(0))
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
