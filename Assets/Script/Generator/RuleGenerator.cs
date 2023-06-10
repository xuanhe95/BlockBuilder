using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    private Rule<GameObject> baseRule = new Rule<GameObject>();
    private Rule<GameObject> midRule = new Rule<GameObject>();

    public void GenerateRules()
    {
        foreach(GameObject go in GoMap.Values){ // Generate rules from Rule Creator Component
            if(go.GetComponent<RuleCreator>() == null) continue;
            foreach(GameObject relative in go.GetComponent<RuleCreator>().Rule){
                AddSimpleRule(midRule, ModMap[go], ModMap[relative]);
            }
            foreach(GameObject relative in go.GetComponent<RuleCreator>().Up){
                //Debug.Log(go.name + " " + relative.name);
                AddSimpleUpRule(midRule, ModMap[go], ModMap[relative]);
            }
        }


    }

    public void AddSimpleUpRule(Rule<GameObject> rule, Type<GameObject> baseGo, Type<GameObject> go)
    {
        rule.AddRule(GeoMap[(int)Geo.Empty], go);
        rule.AddRule(go, go);
        rule.AddUpRule(baseGo, go);
    }

    public void AddSimpleUpRule(Rule<GameObject> rule, Type<GameObject> go)
    {
        rule.AddRule(GeoMap[(int)Geo.Empty], go);
        rule.AddRule(go, go);
        rule.AddUpRule(go, go);
    }

    public void AddSimpleRule(Rule<GameObject> rule, Type<GameObject> go)
    {
        rule.AddRule(go, go);
        rule.AddRule(GeoMap[(int)Geo.Empty], go);
    }

    public void AddSimpleRule(Rule<GameObject> rule, Type<GameObject> go1, Type<GameObject> go2)
    {
        rule.AddRule(go1, go2);
        rule.AddRule(GeoMap[(int)Geo.Empty], go1);
        rule.AddRule(GeoMap[(int)Geo.Empty], go2);
    }
}
