using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(WaveManager))]
public class WaveManagerEditor : Editor
{
    private SerializedProperty waveList;

    private void OnEnable()
    {
        waveList = serializedObject.FindProperty("waveList");
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(waveList);
        
        serializedObject.ApplyModifiedProperties();
    }
}