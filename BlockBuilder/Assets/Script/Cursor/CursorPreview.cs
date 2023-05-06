using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Cursor : MonoBehaviour
{
    private int currentSelection = 0;
    private List<Type<GameObject>> currentTypes;
    private Group<GameObject, GameObject> currentGroup;
    private GameObject Preview;

    private bool PrepareCurrent(Group<GameObject, GameObject> group)
    {
        currentGroup = group;
        currentTypes = group.GetChoicesSet();
        currentSelection = Random.Range(0, currentTypes.Count);
        print(currentTypes.Count);
        if (currentTypes.Count > 0)
            return true;
        else
            return false;
    }

    private void InstancePreview(
        int index,
        List<Type<GameObject>> types,
        Group<GameObject, GameObject> group
    )
    {
        if (types != null)
        {
            Preview = new GameObject();
            Type<GameObject> type = types[index];
            foreach (var go in type.GetObjects())
            {
                Vector3 position = group.GetUnit(0).GetVector().transform.position;
                GameObject newGO = Instantiate(go, position, Quaternion.identity);
                newGO.transform.SetParent(Preview.transform);
                Renderer render = newGO.GetComponent<Renderer>();
                if(render != null){
                    Color color = render.material.color;
                    render.material.color = new Color(color.r, color.g, color.b, 0.8f);
                }
            }
        }
    }

    private void ResetPreview(bool reset)
    {
        Destroy(Preview);
        if(reset){
            currentSelection = 0;
        }
        
    }

    private void TogglePreview(int select)
    {
        UpdateSelection(select);
        InstancePreview(currentSelection, currentTypes, currentGroup);
    }

    private void UpdateSelection(int i)
    {
        currentSelection += i;
        while (currentSelection >= currentTypes.Count)
        {
            currentSelection -= currentTypes.Count;
        }
        while (currentSelection < 0)
        {
            currentSelection += currentTypes.Count;
        }
        Debug.Log(currentSelection);
    }

    //鼠标移动至新物体 - 调用方法返回type list
    //鼠标移动至新物体 - 清除之前的，调用方法instantiate上述type list中的一个，序号为index
    //鼠标滚轮 - index +1/-1，清除之前的，调用方法instantiate上述type list中的一个
    //鼠标点击 - 清除之前预览，将当前type返

    private void UpdateScroll(){
        if(currentTypes == null) return;
                    if(currentTypes.Count != 0){
                float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
                if (scrollWheel > 0f){
                    ResetPreview(false);
                    TogglePreview(1);
                }
                else if(scrollWheel < 0f){
                    ResetPreview(false);
                    TogglePreview(-1);
                }
            }

    }

}
