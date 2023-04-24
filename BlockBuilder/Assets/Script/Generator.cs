using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Generator : MonoBehaviour
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

    //System.Random rd;

    const int LEFT = 0;
    const int RIGHT = 1;
    const int FORWARD = 2;
    const int  BACK = 3;

    public List<GameObject> Choices;
    //public GameObject Empty;
    public GameObject Ground;
    public GameObject GroupType;
    private List<Level<Vector3, GameObject>> levels = new List<Level<Vector3, GameObject>>();


    private List<GameObject> InstantiatedGo = new List<GameObject>();
    private List<Unit<Vector3, GameObject>> InstantiatedUnit = new List<Unit<Vector3, GameObject>>();
    public GameObject meshAll;
    private Dictionary<int, Mesh> meshDic = new Dictionary<int, Mesh>();

    // Start is called before the first frame update
    void Start()
    {
        //rd = new System.Random();
        GenerateRules();
        LevelBuilder(6,8,10);
        
        Debug.Log(levels.Count);
        // PrintRules();
        foreach(Level<Vector3, GameObject> level in levels)
        {
            Debug.Log(level.ID);
            PrintRules(level);
        }
        Instantiator();

    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            UpdateUnits();
            Instantiator();
        }   
    }

    void LevelBuilder(int height, int width, int length){
        Possibility<GameObject> choices = PossibilityGenerator();
        for(int i = 0; i < height; i++){
            if(i == 0) levels.Add(LevelGenerator(i, width, length, 2, choices, baseRule));
            else levels.Add(LevelGenerator(i, width, length, 2, choices, midRule));
        }
    }

    Possibility<GameObject> PossibilityGenerator()
    {
        Possibility<GameObject> choices = new Possibility<GameObject>();
        foreach(GameObject go in Choices)
        {
            choices.Add(go);
        }
        return choices;
    }

    Level<Vector3, GameObject> LevelGenerator(int levelID, int width, int length, double height, Possibility<GameObject> choices, Rule<GameObject> rule)
    {
        int id = 0;
        Level<Vector3, GameObject> level = new Level<Vector3, GameObject>(levelID, width, length, height);
        //levels.Add(level);
        level.SetRule(rule);
        GameObject go = levelID == 0 ? Water : Empty;

        
        for(int i = 0; i < width; i++)
        {
            Unit<Vector3, GameObject> leftUnit = null;
            for(int j = 0; j < length; j++)
            {
                Vector3 vector = new Vector3(i, levelID, j);
                Unit<Vector3, GameObject> unit = 
                new Unit<Vector3, GameObject>(id, vector, go, choices, level);
                level.AddUnit(unit);

                if(leftUnit != null) leftUnit.Relatives[RIGHT] = unit;
                unit.Relatives[LEFT] = leftUnit;
                leftUnit = unit;
            
                id++;
            }
        }
        for(int i = 0; i < width; i++){
            Unit<Vector3, GameObject> backUnit = level.Units[i];
            for(int j = i+width; j < width * length; j+=width)
            {
                Unit<Vector3, GameObject> forwardUnit = level.Units[j];
                backUnit.Relatives[FORWARD] = forwardUnit;
                forwardUnit.Relatives[BACK] = backUnit;
                backUnit = forwardUnit;
            }

        }

        for(int i = 0; i < width; i+=2)
        {
            
            for(int j = 0; j < length; j+=2)
            {
                Group<Vector3, GameObject> group = new Group<Vector3, GameObject>(i, GroupType);
                //Debug.Log(i * length + j);
                //Debug.Log((i+1) * length + j);
                group.AddUnit(level.Units[i * length + j]);
                group.AddUnit(level.Units[i * length + j+1]);
                group.AddUnit(level.Units[(i+1)*length + j]);
                group.AddUnit(level.Units[(i+1)*length + j + 1]);


                //print(group.Units[0].Group + " TEST");
                //print(group.Units[1].Group + " TEST");
                //print(group.Units[2].Group + " TEST");
                //print(group.Units[3].Group + " TEST");

                level.Groups.Add(i*length+j, group);
            }
            

            //Debug.Log("S");
        }


        return level;
    }


    void Instantiator()
    {
        foreach(Level<Vector3, GameObject> level in levels)
        {
            foreach(Unit<Vector3, GameObject> unit in level.Units.Values)
            {
                InstantiatedGo.Add(Instantiate(unit.GetObject(), unit.GetVector(), Quaternion.identity));
                //InstantiatedUnit.Add(unit);
            }

            foreach(Group<Vector3, GameObject> group in level.Groups.Values)
            {
                //Debug.Log(group.GetObject());
                //Debug.Log(group.Units[0].GetVector());
                GameObject GroupCollider = Instantiate(group.GetObject(), group.Units[0].GetVector(), Quaternion.identity);
                GroupCollider.GetComponent<GroupCollider>().setGroup(group);
            }
        }
    }

    void UpdateUnits(){
        foreach(Unit<Vector3, GameObject> unit in InstantiatedUnit)
        {
            GameObject go = unit.GetObject().gameObject;
            GameObject type = unit.Type;
            ModifyMeshWithGameObject(go, type);
        }
    }

    //将所有mesh以字典形式依序存储
    void InitializeMeshes()
    {
        foreach(Transform child in meshAll.transform)
        {
            meshDic.Add(child.GetSiblingIndex(), child.GetComponent<MeshFilter>().sharedMesh);
        }
    }

    //输入一个要修改的gameobject和意欲修改成的mesh编号
    void ModifyMesh(GameObject gameObject, int index)
    {
        if (gameObject.GetComponent<MeshFilter>() != null)
        {
            MeshFilter filter = gameObject.GetComponent<MeshFilter>();
            Mesh meshResult = filter.mesh;
            meshDic.TryGetValue(index, out meshResult);
            filter.mesh = meshResult;
        }
    }
    
    //现在的方法：用gameobject代替
    void ModifyMeshWithGameObject(GameObject gameObject, GameObject game)
    {
        if (game.GetComponent<MeshFilter>() != null)
        {
            MeshFilter filter = gameObject.GetComponent<MeshFilter>();
            filter.mesh = game.GetComponent<MeshFilter>().sharedMesh;
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            renderer.material = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        }
    }
    
    void ModifyMeshWithMesh(GameObject gameObject, Mesh mesh)
    {
        if (gameObject.GetComponent<MeshFilter>() != null)
        {
            MeshFilter filter = gameObject.GetComponent<MeshFilter>();
            filter.mesh = mesh;
        }
    }


    void GenerateRules(){
        //写Rules
        
        //baseRule.AddRule(Empty);
        baseRule.AddRule(Water, Land);
        baseRule.AddRule(Water, Water);
        baseRule.AddRule(Water, Bridge);

        baseRule.AddRule(Land, Water);
        baseRule.AddRule(Land, Land);
        baseRule.AddRule(Land, Bridge);

        baseRule.AddRule(Bridge, Water);
        baseRule.AddRule(Bridge, Land);
        baseRule.AddRule(Bridge, Bridge);
        
        //baseRule.AddRule(Land, Sand);
        baseRule.AddRule(Tree, Tree);

        baseRule.AddUpRule(Land, Building1);
        baseRule.AddUpRule(Land, Building2);

        midRule.AddUpRule(Building1, Roof);
        midRule.AddUpRule(Building2, Roof);
        midRule.AddUpRule(Building2, Building2);
        midRule.AddUpRule(Building1, Building1);

    
        midRule.AddRule(Building1, Building1);
        midRule.AddRule(Building2, Building2);

        midRule.AddRule(Empty, Building1);
        midRule.AddRule(Empty, Building2);
        midRule.AddRule(Water, Water);


    }

    void PrintRules(Level<Vector3, GameObject> level){
        Debug.Log("Work");
        foreach(GameObject go in level.Rules.Conditions.Keys){
            //Debug.Log("Rule");
            Debug.Log(go);
        }

    }

    void PrintRules(){
        foreach(GameObject go in baseRule.Conditions.Keys){
            Debug.Log(go);
        }
    }




}
