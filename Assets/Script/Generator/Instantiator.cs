using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    private List<GameObject> InstantiatedGo = new List<GameObject>();
    public GameObject collider;

    private List<GameObject> GroupColliders = new List<GameObject>();

    public void InitGroupManager()
    {
        foreach (Level<GameObject, GameObject> level in levels){
            foreach (Group<GameObject, GameObject> group in level.Groups.Values){
                            GameObject block = Instantiate(
                    GoMap[(int)Geo.Empty],
                    group.Units[0].GetVector().transform.position - new Vector3(0.5f, 0, 0.5f),
                    Quaternion.identity
                );
                GroupManager gm = block.AddComponent<GroupManager>();
                gm.Initialize(group, GeoMap, ModMap);
                GroupMap.Add(group, gm);
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
            foreach (Unit<GameObject, GameObject> unit in level.Units.Values)
            {
                InstantiatedGo.Add(
                    Instantiate(
                        unit.GetObject(),
                        unit.Group.Units[0].GetVector().transform.position,
                        Quaternion.identity
                    )
                );
            }

            foreach (Group<GameObject, GameObject> group in level.Groups.Values)
            {
                if (group.GetTypes() != GeoMap[(int)Geo.Empty])
                {
                    GameObject GroupCollider = Instantiate(
                        collider,
                        group.Units[0].GetVector().transform.position - new Vector3(0.5f, 0, 0.5f),
                        Quaternion.identity
                    );
                    group.Instance = GroupCollider;
                    GroupCollider.GetComponent<GroupCollider>().SetGroup(group);
                }

            }
        }
    }

}
