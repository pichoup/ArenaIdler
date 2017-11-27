using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WearableItemEditor : EditorWindow {

    public WearableItemDatabase WearableItemDatabase;
    private int viewIndex = 1;

    [MenuItem("Window/Wearable Item Editor %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(WearableItemEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            WearableItemDatabase = AssetDatabase.LoadAssetAtPath(objectPath, typeof(WearableItemDatabase)) as WearableItemDatabase;
        }

    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Wearable Item Editor", EditorStyles.boldLabel);
        if (WearableItemDatabase != null)
        {
            if (GUILayout.Button("Show Item List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = WearableItemDatabase;
            }
        }
        if (GUILayout.Button("Open Item List"))
        {
            OpenWearableItemDatabase();
        }
        if (GUILayout.Button("New Item List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = WearableItemDatabase;
        }
        GUILayout.EndHorizontal();

        if (WearableItemDatabase == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Item List", GUILayout.ExpandWidth(false)))
            {
                CreateNewWearableItemDatabase();
            }
            if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false)))
            {
                OpenWearableItemDatabase();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (WearableItemDatabase != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < WearableItemDatabase.wearableItemDatabase.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false)))
            {
                AddWearableItem();
            }
            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false)))
            {
                DeleteWearableItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (WearableItemDatabase.wearableItemDatabase == null)
                Debug.Log("wtf");
            if (WearableItemDatabase.wearableItemDatabase.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, WearableItemDatabase.wearableItemDatabase.Count);
                //Mathf.Clamp (viewIndex, 1, WearableItemDatabase.itemList.Count);
                EditorGUILayout.LabelField("of   " + WearableItemDatabase.wearableItemDatabase.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                WearableItemDatabase.wearableItemDatabase[viewIndex - 1].itemName = EditorGUILayout.TextField("Item Name", WearableItemDatabase.wearableItemDatabase[viewIndex - 1].itemName as string);
                WearableItemDatabase.wearableItemDatabase[viewIndex - 1].id = (int)EditorGUILayout.IntField("Item Id", WearableItemDatabase.wearableItemDatabase[viewIndex - 1].id);

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                WearableItemDatabase.wearableItemDatabase[viewIndex - 1].wearableType = (WearableItemType)EditorGUILayout.EnumPopup("Wearable Type", WearableItemDatabase.wearableItemDatabase[viewIndex - 1].wearableType);
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                WearableItemDatabase.wearableItemDatabase[viewIndex - 1].accuracy = (int)EditorGUILayout.IntField("Accuracy", WearableItemDatabase.wearableItemDatabase[viewIndex - 1].accuracy);
                WearableItemDatabase.wearableItemDatabase[viewIndex - 1].strength = (int)EditorGUILayout.IntField("Strength", WearableItemDatabase.wearableItemDatabase[viewIndex - 1].strength);
                WearableItemDatabase.wearableItemDatabase[viewIndex - 1].defence = (int)EditorGUILayout.IntField("Defence", WearableItemDatabase.wearableItemDatabase[viewIndex - 1].defence);
                WearableItemDatabase.wearableItemDatabase[viewIndex - 1].health = (int)EditorGUILayout.IntField("Health", WearableItemDatabase.wearableItemDatabase[viewIndex - 1].health);
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                WearableItemDatabase.wearableItemDatabase[viewIndex - 1].accuracyMod = (float)EditorGUILayout.FloatField("Accuracy Modifier", WearableItemDatabase.wearableItemDatabase[viewIndex - 1].accuracyMod);
                WearableItemDatabase.wearableItemDatabase[viewIndex - 1].strengthMod = (float)EditorGUILayout.FloatField("Strength Modifier", WearableItemDatabase.wearableItemDatabase[viewIndex - 1].strengthMod);
                WearableItemDatabase.wearableItemDatabase[viewIndex - 1].defenceMod = (float)EditorGUILayout.FloatField("Defence Modifier", WearableItemDatabase.wearableItemDatabase[viewIndex - 1].defenceMod);
                WearableItemDatabase.wearableItemDatabase[viewIndex - 1].healthMod = (float)EditorGUILayout.FloatField("Health Modifier", WearableItemDatabase.wearableItemDatabase[viewIndex - 1].healthMod);
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

            }
            else
            {
                GUILayout.Label("This Wearable List is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(WearableItemDatabase);
        }
    }

    void CreateNewWearableItemDatabase()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        WearableItemDatabase = CreateWearableItemDatabase.Create();
        if (WearableItemDatabase)
        {
            WearableItemDatabase.wearableItemDatabase = new List<WearableItem>();
            string relPath = AssetDatabase.GetAssetPath(WearableItemDatabase);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenWearableItemDatabase()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Wearable Item List", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            WearableItemDatabase = AssetDatabase.LoadAssetAtPath(relPath, typeof(WearableItemDatabase)) as WearableItemDatabase;
            if (WearableItemDatabase.wearableItemDatabase == null)
                WearableItemDatabase.wearableItemDatabase = new List<WearableItem>();
            if (WearableItemDatabase)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddWearableItem()
    {
        WearableItem newItem = new WearableItem();
        newItem.itemName = "New Item";
        WearableItemDatabase.wearableItemDatabase.Add(newItem);
        viewIndex = WearableItemDatabase.wearableItemDatabase.Count;
    }

    void DeleteWearableItem(int index)
    {
        WearableItemDatabase.wearableItemDatabase.RemoveAt(index);
    }
}
