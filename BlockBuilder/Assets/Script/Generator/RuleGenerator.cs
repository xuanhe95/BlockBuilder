using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    private Rule<GameObject> baseRule = new Rule<GameObject>(); 
    private Rule<GameObject> midRule = new Rule<GameObject>();

    public void GenerateRules(){
        AddSimpleRule(midRule, Sands, Lands);
        AddSimpleRule(midRule, Sands);
        AddSimpleRule(midRule, Lands);
        AddSimpleRule(midRule, Trees);
        AddSimpleRule(midRule, Waters);

        AddSimpleUpRule(midRule, Waters, Sands);
        AddSimpleUpRule(midRule, Waters, Lands);
        AddSimpleUpRule(midRule, Sands, Lands);
        AddSimpleUpRule(midRule, Lands, Trees);
        AddSimpleUpRule(midRule, Trees);

        AddSimpleUpRule(midRule, Lands);

    }

    public void AddSimpleUpRule(Rule<GameObject> rule, Type<GameObject> baseGo, Type<GameObject> go)
    {
        rule.AddRule(Emptys, go);
        rule.AddRule(go, go);
        rule.AddUpRule(baseGo, go);
    }

    public void AddSimpleUpRule(Rule<GameObject> rule, Type<GameObject> go)
    {
        rule.AddRule(Emptys, go);
        rule.AddRule(go, go);
        rule.AddUpRule(go, go);
    }

    public void AddSimpleRule(Rule<GameObject> rule, Type<GameObject> go)
    {
        rule.AddRule(go, go);
        rule.AddRule(Emptys, go);
    }

    public void AddSimpleRule(Rule<GameObject> rule, Type<GameObject> go1, Type<GameObject> go2)
    {
        rule.AddRule(go1, go2);
        rule.AddRule(Emptys, go1);
        rule.AddRule(Emptys, go2);
    }
}
