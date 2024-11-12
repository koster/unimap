using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapNodePreset))]
public class MapNodePresetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapNodePreset tileData = (MapNodePreset)target;

        GUILayout.Space(20);

        // if (GUILayout.Button("[CTRL-W] Link"))
        // {
        //     tileData.Link();
        // }

        if (GUILayout.Button("[CTRL-E] Extend"))
        {
            ExtendTile(tileData);
        }

        // Проверка нажатия горячей клавиши в окне инспектора
        CheckHotkeyForExtend(tileData);
    }

    private void OnSceneGUI()
    {
        MapNodePreset tileData = (MapNodePreset)target;

        CheckHotkeyForExtend(tileData);
    }

    private void CheckHotkeyForExtend(MapNodePreset tileData)
    {
        Event e = Event.current;

        if (e != null && e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.E)
        {
            ExtendTile(tileData);
            e.Use();
        }
    }
    
    private void ExtendTile(MapNodePreset tileData)
    {
        string baseName = "Tile";
        int index = 0;

        while (tileData.transform.parent.Find($"{baseName}_{index}") != null)
        {
            index++;
        }

        var newTile = new GameObject($"{baseName}_{index}");
        newTile.transform.parent = tileData.transform.parent;
        Undo.RegisterCreatedObjectUndo(newTile, "create tile");
        newTile.transform.position = tileData.transform.position + tileData.transform.forward;
        Undo.RecordObject(tileData, "add join");
        tileData.join.Add(newTile.AddComponent<MapNodePreset>());
        Selection.activeObject = newTile;
    }

}