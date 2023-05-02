using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    private Rule<GameObject> baseRule = new Rule<GameObject>();
    private Rule<GameObject> midRule = new Rule<GameObject>();

    public void GenerateRules()
    {
        AddSimpleRule(midRule, GeoMap[(int)Geo.Sand], GeoMap[(int)Geo.Land]);
        AddSimpleRule(midRule, GeoMap[(int)Geo.Sand]);
        AddSimpleRule(midRule, GeoMap[(int)Geo.Land]);
        AddSimpleRule(midRule, GeoMap[(int)Geo.Tree]);
        AddSimpleRule(midRule, GeoMap[(int)Geo.Water]);

        AddSimpleUpRule(midRule, GeoMap[(int)Geo.Water], GeoMap[(int)Geo.Sand]);
        AddSimpleUpRule(midRule, GeoMap[(int)Geo.Water], GeoMap[(int)Geo.Land]);
        AddSimpleUpRule(midRule, GeoMap[(int)Geo.Sand], GeoMap[(int)Geo.Land]);
        AddSimpleUpRule(midRule, GeoMap[(int)Geo.Land], GeoMap[(int)Geo.Tree]);
        AddSimpleUpRule(midRule, GeoMap[(int)Geo.Tree]);

        AddSimpleUpRule(midRule, GeoMap[(int)Geo.Land]);
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
