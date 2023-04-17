using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Generator : MonoBehaviour
{

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
        LevelBuilder(6,8,10);
        Instantiator();

    }

    void LevelBuilder(int height, int width, int length){
        Possibility<GameObject> choices = PossibilityGenerator();
        for(int i = 0; i < height; i++){
            levels.Add(LevelGenerator(i, width, length, 1, choices));
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

    Level<Vector3, GameObject> LevelGenerator(int levelID, int width, int length, double height, Possibility<GameObject> choices)
    {
        int id = 0;
        Level<Vector3, GameObject> level = new Level<Vector3, GameObject>(levelID, width, length,height);
        GameObject go = levelID == 0 ? Ground : Empty;

        
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
                Debug.Log(i * length + j);
                Debug.Log((i+1) * length + j);
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
                Instantiate(group.GetObject(), group.Units[0].GetVector(), Quaternion.identity);
            }


        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
