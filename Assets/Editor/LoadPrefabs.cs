using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;

public class AddPrefabsToList : MonoBehaviour
{
    public static string folderPath = "Assets/Prefabs/Model/"; // 指定的文件夹路径
    public static Generator scriptWithList; // 包含公共列表的脚本

    [MenuItem("Custom/Add Prefabs to List")]
    private static void AddPrefabs()
    {
        scriptWithList = FindObjectOfType<Generator>();


        if (Directory.Exists(folderPath))
        {
            string[] prefabPaths = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);

            foreach (string prefabPath in prefabPaths)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                if (prefab != null)
                {
                    // 将Prefab添加到脚本的公共列表中
                    scriptWithList.Models.Add(prefab);
                }
            }

            // foreach (Geo geo in Enum.GetValues(typeof(Geo))){
            //     string geoName = Enum.GetName(typeof(Geo), geo);
            //     Debug.Log(geoName);
            //     //int index = Array.IndexOf(prefabPaths, geoName);

            //     for(int i = 0; i < prefabPaths.Length; i++){
            //         if(prefabPaths[i].Contains(geoName)){
            //             GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPaths[i]);
            //         if (prefab != null)
            //         {
            //             // 将Prefab添加到脚本的公共列表中
            //             scriptWithList.Models.Add(prefab);
            //         }
            //         }
            //     }

            // }

            Debug.Log("Prefab已添加到列表");
        }
        else
        {
            Debug.LogWarning("指定的文件夹路径不存在：" + folderPath);
        }
    }
}
