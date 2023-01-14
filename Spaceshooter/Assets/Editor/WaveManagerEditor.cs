using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveManager))]
public class WaveManagerEditor : Editor
{
    private SerializedProperty waves;
    private int waveIndex = 0;
    private int waveCount = 0;
    private List<string> wavePopupDisplayNames = new List<string>();

    private void OnEnable()
    {
        waves = serializedObject.FindProperty("waves");
        waveCount = waves.arraySize;
        
        for (int i = 0; i < waveCount; i++)
        {
            wavePopupDisplayNames.Add("Wave " + (i+1));
        }

        

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        
        
        AddOrRemoveOrMoveWave();
        
        EditorGUILayout.Space();

        if(waves.arraySize>0)
        {
            DisplaySelectedWave();
        }
        
        serializedObject.ApplyModifiedProperties();
    }

    private void AddOrRemoveOrMoveWave()
    {
        EditorGUILayout.BeginHorizontal();
        
        bool addnewWave = EditorGUILayout.LinkButton("Add new Wave");
        bool removeCurrentWave = false;
        bool moveWaveUp = false;
        
        
        
        if (waves.arraySize > 0)
        {
            removeCurrentWave = EditorGUILayout.LinkButton("Remove Selected Wave");
        }
        
        if(waveIndex>0)
            moveWaveUp = EditorGUILayout.LinkButton("Move Wave Up");
                
        
                
        EditorGUILayout.EndHorizontal();
        
        if (addnewWave)
        {
            waves.InsertArrayElementAtIndex(waveCount);
            wavePopupDisplayNames.Add("Wave " + (waveCount + 1));
            waveCount++;
        }
        
        if (removeCurrentWave)
        {
            waves.DeleteArrayElementAtIndex(waveIndex);
            waveCount--;
            wavePopupDisplayNames.RemoveAt(waves.arraySize);
            waveIndex--;
            if (waveIndex < 0) waveIndex = 0;
        }
        
        if (moveWaveUp)
        {
            waves.MoveArrayElement(waveIndex, waveIndex - 1);
        }
        
        

    }

    private void DisplaySelectedWave()
    {
        if (wavePopupDisplayNames.Count > 0)
            waveIndex = EditorGUILayout.Popup("Waves", waveIndex, wavePopupDisplayNames.ToArray());


        SerializedProperty displayedWave = waves.GetArrayElementAtIndex(waveIndex);

        EditorGUILayout.BeginVertical();

        EditorGUILayout.Space();
        
        EditorGUI.indentLevel++;

        EditorGUILayout.PropertyField(displayedWave.FindPropertyRelative("delayBetweenSpawns"));
        EditorGUILayout.PropertyField(displayedWave.FindPropertyRelative("delayBeforeWaveWarning"));
        EditorGUILayout.PropertyField(displayedWave.FindPropertyRelative("waveWarning"));
        EditorGUILayout.PropertyField(displayedWave.FindPropertyRelative("delayBeforeWaveStart"));
        
        EditorGUILayout.Space();
        
        SerializedProperty useSubwaves = displayedWave.FindPropertyRelative("useSubwaves");

        EditorGUILayout.PropertyField(useSubwaves);

        if (useSubwaves.boolValue)
        {
            SerializedProperty subwaves = displayedWave.FindPropertyRelative("subwaves");
            EditorGUILayout.PropertyField(subwaves);
            
        }
        else
        {
            SerializedProperty enemyPrefabs = displayedWave.FindPropertyRelative("enemyPrefabs");
            SerializedProperty useCount = displayedWave.FindPropertyRelative("useCount");

            EditorGUILayout.PropertyField(enemyPrefabs);
            EditorGUILayout.PropertyField(useCount);
            
            EditorGUI.indentLevel++;
            
            if (useCount.boolValue)
            {
                SerializedProperty enemyCount = displayedWave.FindPropertyRelative("enemyCount");
                SerializedProperty maxEnemiesOnScreen = displayedWave.FindPropertyRelative("maxEnemiesOnScreen");
                
                EditorGUILayout.PropertyField(enemyCount);
                EditorGUILayout.PropertyField(maxEnemiesOnScreen);
            }
            else
            {
                SerializedProperty wavePower = displayedWave.FindPropertyRelative("wavePower");
                SerializedProperty maxPowerOnScreen = displayedWave.FindPropertyRelative("maxPowerOnScreen");
                
                EditorGUILayout.PropertyField(wavePower);
                EditorGUILayout.PropertyField(maxPowerOnScreen);
            }
        }

        EditorGUILayout.EndVertical();
    }
}