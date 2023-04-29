using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
 void GenerateRules(){
        AddSimpleUpRule(midRule, Waters, Sands);
        AddSimpleUpRule(midRule, Waters, Lands);
        AddSimpleRule(midRule, Sands, Lands);
        AddSimpleUpRule(midRule, Sands, Trees);
        AddSimpleRule(midRule, Trees);
        AddSimpleUpRule(midRule, Lands, Trees);
    }

    public void AddSimpleUpRule(Rule<GameObject> rule, Type<GameObject> baseGo, Type<GameObject> go)
    {
        rule.AddRule(Emptys, go);
        rule.AddUpRule(baseGo, go);
        rule.AddRule(go, go);
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
