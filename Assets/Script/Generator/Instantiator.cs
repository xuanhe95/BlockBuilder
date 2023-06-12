using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    private List<GameObject> InstantiatedGo = new List<GameObject>();
    public GameObject collider;
    private List<GameObject> GroupColliders = new List<GameObject>();

    public void InitGroupManager() //挂载GroupManager
    {

        foreach (Level<GameObject, GameObject> level in levels)
        {
            foreach (var group in level.Groups.Values)
            {
                GameObject block = new GameObject();
                block.transform.position = group.Units[0].GetVector().transform.position - new Vector3(0.5f, 0, 0.5f);
                // GameObject block = Instantiate(GoMap[(int)Geo.Empty],
                //     group.Units[0].GetVector().transform.position - new Vector3(0.5f, 0, 0.5f),
                //     Quaternion.identity);
                block.name = "newBlock";
                GroupManager gm = block.AddComponent<GroupManager>();
                gm.Initialize(group, GeoMap, ModMap, GoMap); //初始化GroupManager
                GroupMap.Add(group, gm); //记录GroupManager和Group的对应关系
            }
        }
    }

    void Instantiator()
    {
        if (InstantiatedGo.Count > 0)
        {
            foreach (var o in InstantiatedGo)
            {
                Destroy(o);
            }

            foreach (var o in GroupColliders)
            {
                Destroy(o);
            }

            InstantiatedGo.Clear();
            GroupColliders.Clear();
        }
        

        foreach (Level<GameObject, GameObject> level in levels)
        {
            foreach (Group<GameObject, GameObject> group in level.Groups.Values)
            {
                GroupManager blockGm = GroupMap[group];
                //print(blockGm);
                GameObject block = blockGm.transform.gameObject;

                if (group.GetTypes() != GeoMap[(int)Geo.Empty])
                {
                    GameObject GroupCollider = Instantiate(
                        collider,
                        group.Units[0].GetVector().transform.position - new Vector3(0.5f, 0, 0.5f),
                        Quaternion.identity
                    );
                    group.Instance = GroupCollider;
                    GroupCollider.GetComponent<GroupCollider>().SetGroup(group);
                    GroupCollider.transform.SetParent(block.transform);
                    GroupColliders.Add(GroupCollider);

                    
                    foreach (Unit<GameObject, GameObject> unit in group.Units)
                    {
                        GameObject Newunit = Instantiate(
                            unit.GetObject(),
                            unit.Group.Units[0].GetVector().transform.position,
                            Quaternion.identity
                        );
                        InstantiatedGo.Add(Newunit);
                        Newunit.transform.SetParent(GroupCollider.transform);
                    }
                }
            }

        }
    }
}
