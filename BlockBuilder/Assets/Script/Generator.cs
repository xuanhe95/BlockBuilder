using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Generator : MonoBehaviour
{
    public GameObject Water;
    public GameObject Land;
    public GameObject Building1;
    public GameObject Building2;

    public GameObject Roof;
    public GameObject Sand;
    public GameObject Tree;

    Rule<GameObject> baseRule; 
    Rule<GameObject> midRule;

    //System.Random rd;

    const int LEFT = 0;
    const int RIGHT = 1;
    const int FORWARD = 2;
    const int  BACK = 3;

    public List<GameObject> Choices;
    public GameObject Empty;
    public GameObject Ground;
    public GameObject GroupType;
    private List<Level<Vector3, GameObject>> levels = new List<Level<Vector3, GameObject>>();
    // Start is called before the first frame update
    void Start()
    {
        //rd = new System.Random();
        GenerateRules();
        LevelBuilder(6,8,10);
        Instantiator();

    }


    // Update is called once per frame
    void Update()
    {
        //Group<Vector3, GameObject> group;
        //group.Select(rd);
        
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
                Instantiate(unit.GetObject(), unit.GetVector(), Quaternion.identity);
            }

            foreach(Group<Vector3, GameObject> group in level.Groups.Values)
            {
                Debug.Log(group.GetObject());
                Debug.Log(group.Units[0].GetVector());
                GameObject GroupCollider = Instantiate(group.GetObject(), group.Units[0].GetVector(), Quaternion.identity);
                GroupCollider.GetComponent<GroupCollider>().setGroup(group);
            }


        }

    }


    void GenerateRules(){
        baseRule = new Rule<GameObject>();

        //baseRule.AddRule(Empty);
        baseRule.AddRule(Water, Sand);
        baseRule.AddRule(Water, Water);
        baseRule.AddRule(Sand, Land);
        baseRule.AddRule(Sand, Sand);
        baseRule.AddRule(Land, Land);
        baseRule.AddRule(Land, Tree);
        //baseRule.AddRule(Land, Sand);
        baseRule.AddRule(Tree, Tree);

        baseRule.AddUpRule(Land, Building1);
        baseRule.AddUpRule(Land, Building2);


        midRule = new Rule<GameObject>();
        midRule.AddUpRule(Building1, Roof);
        midRule.AddUpRule(Building2, Roof);
        midRule.AddUpRule(Building2, Building2);
        midRule.AddUpRule(Building1, Building1);

    
        midRule.AddRule(Building1, Building1);
        midRule.AddRule(Building2, Building2);

        midRule.AddRule(Empty, Building1);
        midRule.AddRule(Empty, Building2);


    }




}
