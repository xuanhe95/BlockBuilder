using UnityEngine;
using UnityEditor;
using System.IO;

public class AddRules : MonoBehaviour
{
    public static string addPrefabPath = "Assets/Prefabs/Model/Empty.prefab";
    public static string folderPath = "Assets/Prefabs/Model/Grass"; // 文件夹路径

    [MenuItem("Custom/Add Rules")]
    private static void GetScriptsFromPrefabs()
    {
        SetFixedRule("Assets/Prefabs/Model/Empty.prefab", "Assets/Prefabs/Model/Rock");

        SetFixedRules("Assets/Prefabs/Model/Rock","Assets/Prefabs/Model/Rock");
        SetFixedRules("Assets/Prefabs/Model/Pillar_Mid","Assets/Prefabs/Model/Pillar_Mid");
        SetFixedRules("Assets/Prefabs/Model/Pillar_Roof","Assets/Prefabs/Model/Pillar_Roof");
        SetFixedRules("Assets/Prefabs/Model/Building","Assets/Prefabs/Model/Building");
        SetFixedRules("Assets/Prefabs/Model/Roof","Assets/Prefabs/Model/Roof");
        SetFixedRules("Assets/Prefabs/Model/Roof2","Assets/Prefabs/Model/Roof2");
        SetFixedRules("Assets/Prefabs/Model/Roof3","Assets/Prefabs/Model/Roof3");



        SetUpRule("Assets/Prefabs/Model/Water.prefab", "Assets/Prefabs/Model/Rock");
        // Rock
        SetUpRules("Assets/Prefabs/Model/Rock", "Assets/Prefabs/Model/Pillar_Mid");
        SetUpRules("Assets/Prefabs/Model/Rock", "Assets/Prefabs/Model/Building", false);

        // Pillar_Mid
        SetUpRules("Assets/Prefabs/Model/Pillar_Mid", "Assets/Prefabs/Model/Pillar_Mid");
        SetUpRules("Assets/Prefabs/Model/Pillar_Mid", "Assets/Prefabs/Model/Building", false);
        //SetUpRules("Assets/Prefabs/Model/Pillar_Mid", "Assets/Prefabs/Model/Roof", false);
        //SetUpRules("Assets/Prefabs/Model/Pillar_Mid", "Assets/Prefabs/Model/Roof2", false);
        //SetUpRules("Assets/Prefabs/Model/Pillar_Mid", "Assets/Prefabs/Model/Roof3", false);
        SetUpRules("Assets/Prefabs/Model/Pillar_Mid", "Assets/Prefabs/Model/Pillar_Roof", false);

        // Building
        SetUpRules("Assets/Prefabs/Model/Building", "Assets/Prefabs/Model/Building");
        SetUpRules("Assets/Prefabs/Model/Building", "Assets/Prefabs/Model/Roof", false);
        SetUpRules("Assets/Prefabs/Model/Building", "Assets/Prefabs/Model/Roof2", false);
        //SetUpRules("Assets/Prefabs/Model/Building", "Assets/Prefabs/Model/Roof3", false);

    }

    private static void SetFixedRule(string addPrefabPath, string folderPath){
        // 获取文件夹中的所有 Prefab 文件路径
        string[] prefabPaths = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);

        // 加载要添加的 Prefab
        GameObject addPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(addPrefabPath);
        addPrefab.GetComponent<RuleCreator>().FixedRules.Clear();

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
                    addPrefab.GetComponent<RuleCreator>().FixedRules.Add(prefab);
                    Debug.Log("Prefab: " + prefab.name + ", Script: " + script.GetType().Name);
                }
            }
        }
    }

    private static void SetUpRule(string addPrefabPath, string folderPath){
        // 获取文件夹中的所有 Prefab 文件路径
        string[] prefabPaths = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);

        // 加载要添加的 Prefab
        GameObject addPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(addPrefabPath);
        addPrefab.GetComponent<RuleCreator>().Up.Clear();

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
                    addPrefab.GetComponent<RuleCreator>().Up.Add(prefab);
                    Debug.Log("Prefab: " + prefab.name + ", Script: " + script.GetType().Name);
                }
            }
        }
    }
    private static void SetUpRules(string downPath, string upPath, bool isClear = true)
    {
        // 获取文件夹中的所有 Prefab 文件路径
        string[] downPaths = Directory.GetFiles(downPath, "*.prefab", SearchOption.AllDirectories);
        string[] upPaths = Directory.GetFiles(upPath, "*.prefab", SearchOption.AllDirectories);

    
        foreach(string downPrefabPath in downPaths){
            // 加载要添加的 Prefab
            GameObject downPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(downPrefabPath);
            if(isClear) downPrefab.GetComponent<RuleCreator>().Up.Clear();

        // 遍历每个 Prefab
        foreach (string upPrefabPath in upPaths)
        {
            // 加载 Prefab
            GameObject upPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(upPrefabPath);
            if (upPrefab != null)
            {
                // 获取 Prefab 上的所有脚本组件
                //MonoBehaviour[] scripts = downPrefab.GetComponents<RuleCreator>();

                // 输出脚本名称
                //foreach (MonoBehaviour script in scripts)
                //{
                    downPrefab.GetComponent<RuleCreator>().Up.Add(upPrefab);
                    //Debug.Log("Prefab: " + prefab.name + ", Script: " + script.GetType().Name);
                //}
            }
        }
        }

    }

        private static void SetFixedRules(string downPath, string upPath)
    {
        // 获取文件夹中的所有 Prefab 文件路径
        string[] downPaths = Directory.GetFiles(downPath, "*.prefab", SearchOption.AllDirectories);
        string[] upPaths = Directory.GetFiles(upPath, "*.prefab", SearchOption.AllDirectories);

    
        foreach(string downPrefabPath in downPaths){
            // 加载要添加的 Prefab
            GameObject downPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(downPrefabPath);
            downPrefab.GetComponent<RuleCreator>().FixedRules.Clear();

        // 遍历每个 Prefab
        foreach (string upPrefabPath in upPaths)
        {
            // 加载 Prefab
            GameObject upPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(upPrefabPath);
            if (upPrefab != null)
            {
                // 获取 Prefab 上的所有脚本组件
                //MonoBehaviour[] scripts = downPrefab.GetComponents<RuleCreator>();

                // 输出脚本名称
                //foreach (MonoBehaviour script in scripts)
                //{
                    downPrefab.GetComponent<RuleCreator>().FixedRules.Add(upPrefab);
                    //Debug.Log("Prefab: " + prefab.name + ", Script: " + script.GetType().Name);
                //}
            }
        }
        }

    }
}