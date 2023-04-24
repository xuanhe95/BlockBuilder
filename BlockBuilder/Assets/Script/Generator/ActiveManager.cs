using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{

    void Instantiator()
    {
        foreach(Level<GameObject, GameObject> level in levels)
        {
            foreach(Unit<GameObject, GameObject> unit in level.Units.Values)
            {
                InstantiatedGo.Add(Instantiate(unit.GetObject(), unit.GetVector().transform.position, Quaternion.identity));
                //InstantiatedUnit.Add(unit);
            }

            foreach(Group<GameObject, GameObject> group in level.Groups.Values)
            {
                GameObject GroupCollider = Instantiate(group.GetObject(), group.Units[0].GetVector().transform.position, Quaternion.identity);
                GroupCollider.GetComponent<GroupCollider>().SetGroup(group);
            }
        }
    }

    void UpdateUnits(){
        foreach(Unit<GameObject, GameObject> unit in InstantiatedUnit)
        {
            GameObject go = unit.GetObject().gameObject;
            GameObject type = unit.Type;
            ModifyMeshWithGameObject(go, type);
        }
    }

    //将所有mesh以字典形式依序存储
    void InitializeMeshes()
    {
        foreach(Transform child in meshAll.transform)
        {
            meshDic.Add(child.GetSiblingIndex(), child.GetComponent<MeshFilter>().sharedMesh);
        }
    }

    //输入一个要修改的gameobject和意欲修改成的mesh编号
    void ModifyMesh(GameObject gameObject, int index)
    {
        if (gameObject.GetComponent<MeshFilter>() != null)
        {
            MeshFilter filter = gameObject.GetComponent<MeshFilter>();
            Mesh meshResult = filter.mesh;
            meshDic.TryGetValue(index, out meshResult);
            filter.mesh = meshResult;
        }
    }
    
    //现在的方法：用gameobject代替
    void ModifyMeshWithGameObject(GameObject gameObject, GameObject game)
    {
        if (game.GetComponent<MeshFilter>() != null)
        {
            MeshFilter filter = gameObject.GetComponent<MeshFilter>();
            filter.mesh = game.GetComponent<MeshFilter>().sharedMesh;
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            renderer.material = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        }
    }
    
    void ModifyMeshWithMesh(GameObject gameObject, Mesh mesh)
    {
        if (gameObject.GetComponent<MeshFilter>() != null)
        {
            MeshFilter filter = gameObject.GetComponent<MeshFilter>();
            filter.mesh = mesh;
        }
    }

}
