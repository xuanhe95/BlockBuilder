using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public List<GameObject> Choices;
    public GameObject Empty;
    public GameObject Ground;
    private List<Level<Vector3, GameObject>> levels = new List<Level<Vector3, GameObject>>();
    // Start is called before the first frame update
    void Start()
    {
        Possibility<GameObject> choices = PossibilityGenerator();
        for(int i = 0; i < 4; i++){
            levels.Add(LevelGenerator(i, 1, choices));
        }
        Instantiator();

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

    Level<Vector3, GameObject> LevelGenerator(int levelID, double height, Possibility<GameObject> choices)
    {
        int id = 0;
        Level<Vector3, GameObject> level = new Level<Vector3, GameObject>(levelID, height);
        GameObject go = levelID == 0 ? Ground : Empty;
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                Vector3 vector = new Vector3(i, levelID, j);
                Unit<Vector3, GameObject> unit = 
                new Unit<Vector3, GameObject>(id, vector, go, choices, level);
                level.AddUnit(unit);
                id++;
            }
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
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
