using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CreateWearableItemDatabase : MonoBehaviour {
    [MenuItem("Assets/Create/Inventory Item List")]
    public static WearableItemDatabase Create()
    {
        WearableItemDatabase asset = ScriptableObject.CreateInstance<WearableItemDatabase>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/WearableItemList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
