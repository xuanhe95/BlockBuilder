using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
 void GenerateRules(){
        //写Rules
        
        //baseRule.AddRule(Empty);
        baseRule.AddRule(Water, Land);
        baseRule.AddRule(Water, Water);
        baseRule.AddRule(Water, Bridge);

        midRule.AddUpRule(Land, Tree);
        midRule.AddUpRule(Water, Tree);
        midRule.AddRule(Empty, Tree);

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


        midRule.AddRule(Tree, Tree);

        midRule.AddRule(Empty, Building1);
        midRule.AddRule(Empty, Building2);
        midRule.AddRule(Water, Water);


    }

    void PrintRules(Level<GameObject, GameObject> level){
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
