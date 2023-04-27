using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
 void GenerateRules(){
        //写Rules
        
        //baseRule.AddRule(Empty);
        // baseRule.AddRule(Water, Land);
        // baseRule.AddRule(Water, Water);
        // baseRule.AddRule(Water, Bridge);

        // midRule.AddUpRule(Land, Tree);
        // midRule.AddUpRule(Water, Tree);
        // midRule.AddRule(Empty, Tree);

        // baseRule.AddRule(Land, Water);
        // baseRule.AddRule(Land, Land);
        // baseRule.AddRule(Land, Bridge);

        // baseRule.AddRule(Bridge, Water);
        // baseRule.AddRule(Bridge, Land);
        // baseRule.AddRule(Bridge, Bridge);
        
        // //baseRule.AddRule(Land, Sand);
        // baseRule.AddRule(Tree, Tree);

        // baseRule.AddUpRule(Land, Building1);
        // baseRule.AddUpRule(Land, Building2);

        // midRule.AddUpRule(Building1, Roof);
        // midRule.AddUpRule(Building2, Roof);
        // midRule.AddUpRule(Building2, Building2);
        // midRule.AddUpRule(Building1, Building1);

    
        // midRule.AddRule(Building1, Building1);
        // midRule.AddRule(Building2, Building2);


        // midRule.AddRule(Tree, Tree);

        // midRule.AddRule(Empty, Building1);
        // midRule.AddRule(Empty, Building2);
        // midRule.AddRule(Water, Water);



        // midRule.AddUpRule(Tree, Tree);
        // midRule.AddRule(Empty, Tree);

        // midRule.AddUpRule(Water, Sand);
        // midRule.AddUpRule(Water, Land);
        // midRule.AddRule(Empty, Sand);
        // midRule.AddRule(Empty, Sand);

        AddSimpleUpRule(midRule, Water, Sand);
        AddSimpleUpRule(midRule, Water, Land);
        AddSimpleRule(midRule, Sand, Land);
        AddSimpleUpRule(midRule, Sand, Tree);
        AddSimpleRule(midRule, Tree);
        AddSimpleUpRule(midRule, Land, Tree);


    }

    public void AddSimpleUpRule(Rule<GameObject> rule, GameObject baseGo, GameObject go)
    {
        rule.AddRule(Empty, go);
        rule.AddUpRule(baseGo, go);
        rule.AddRule(go, go);
    }

    public void AddSimpleRule(Rule<GameObject> rule, GameObject go)
    {
        rule.AddRule(go, go);
        rule.AddRule(Empty, go);
    }

    public void AddSimpleRule(Rule<GameObject> rule, GameObject go1, GameObject go2)
    {
        rule.AddRule(go1, go2);
        rule.AddRule(Empty, go1);
        rule.AddRule(Empty, go2);
    }

    public void GenerateListedRules()
    {
        baseRule.AddRule(Waters, Lands);
        baseRule.AddRule(Waters, Sands);
        
        midRule.AddRule(Emptys, Trees);
        midRule.AddRule(Trees, Trees);
        midRule.AddUpRule(Waters, Trees);
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
