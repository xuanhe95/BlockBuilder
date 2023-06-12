using UnityEngine;
using UnityEditor;
using System.IO;

public class AddRoofRule : MonoBehaviour
{
    public static string upPrefabPath = "Assets/Prefabs/Model/Roof";
    public static string folderPath = "Assets/Prefabs/Model/Roof"; // 文件夹路径

    [MenuItem("Custom/Add Roof Rules")]
    private static void GetScriptsFromPrefabs()
    {
        // 获取文件夹中的所有 Prefab 文件路径
        string[] prefabPaths = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);
        string[] addPrefabPaths = Directory.GetFiles(upPrefabPath, "*.prefab", SearchOption.AllDirectories);

    
        foreach(string addPrefabPath in addPrefabPaths){
            // 加载要添加的 Prefab
            GameObject addPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(addPrefabPath);




        // 遍历每个 Prefab
        foreach (string prefabPath in prefabPaths)
        {
            // 加载 Prefab
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab != null)
            {
                // 获取 Prefab 上的所有脚本组件
                MonoBehaviour[] scripts = prefab.GetComponents<RuleCreator>();

                // 输出脚本名称
                foreach (MonoBehaviour script in scripts)
                {
                    prefab.GetComponent<RuleCreator>().FixedRules.Add(addPrefab);
                    //Debug.Log("Prefab: " + prefab.name + ", Script: " + script.GetType().Name);
                }
            }
        }
        }

    }
}