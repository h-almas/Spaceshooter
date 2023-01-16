using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WaveManager.WaveList<>))]
public class WaveListPropertyDrawer : PropertyDrawer
{

    private SerializedProperty waves = null;
        
    private int waveIndex = 0;
    private int waveCount = 0;
    private List<string> wavePopupDisplayNames = null;

    const float YOffset = 3f;
    float lineHeight = EditorGUIUtility.singleLineHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (wavePopupDisplayNames == null || waves==null)
        {
            waves = property.FindPropertyRelative("waves");
            waveCount = waves.arraySize;

            wavePopupDisplayNames = new List<string>();
            for (int i = 0; i < waveCount; i++)
            {
                wavePopupDisplayNames.Add(("Wave ") + (i + 1));
            }

        }


        return lineHeight * 2 + YOffset * 2 + (waveCount > 0
            ? EditorGUI.GetPropertyHeight(waves.GetArrayElementAtIndex(waveIndex)) : 0);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        AddOrRemoveOrMoveWave(position, property, label);

        position.y += lineHeight / 2;
        

        if(waveCount>0)
        {
            DisplaySelectedWave(position, property, label);
        }
    }

    

    private void AddOrRemoveOrMoveWave(Rect position, SerializedProperty property, GUIContent label)
    {
        bool addnewWave = EditorGUI.LinkButton(new Rect(position.x, position.y, 90, lineHeight),"Add new Wave");
        bool removeCurrentWave = false;
        bool moveWaveUp = false;
        
        if (waveCount > 0)
        {
            removeCurrentWave = EditorGUI.LinkButton(new Rect(position.x+95, position.y, 140, lineHeight),"Remove Selected Wave");
        }
        
        if(waveIndex>0)
            moveWaveUp = EditorGUI.LinkButton(new Rect(position.x + 240, position.y, 95, lineHeight),"Move Wave Up");
                
        
        if (addnewWave)
        {
            waves.InsertArrayElementAtIndex(waveCount);
            

            wavePopupDisplayNames.Add("Wave " + (waveCount + 1));
            waveIndex = waveCount;
            waveCount++;
        }
        
        if (removeCurrentWave)
        {
            waves.DeleteArrayElementAtIndex(waveIndex);
            waveCount--;
            wavePopupDisplayNames.RemoveAt(waves.arraySize);
            waveIndex -= waveIndex == waveCount ? 1 : 0;
            if (waveIndex < 0) waveIndex = 0;
            
        }
        
        if (moveWaveUp)
        {
            waves.MoveArrayElement(waveIndex, waveIndex - 1);
            waveIndex--;
        }
        
        

    }

    private void DisplaySelectedWave(Rect position, SerializedProperty property, GUIContent label)
    {
        
        if (wavePopupDisplayNames.Count > 0)
            waveIndex = EditorGUI.Popup(new Rect(position.x, position.y+=YOffset+lineHeight, position.width, lineHeight),"Waves", waveIndex, wavePopupDisplayNames.ToArray());


        SerializedProperty displayedWave = waves.GetArrayElementAtIndex(waveIndex);

        EditorGUI.PropertyField(new Rect(position.x, position.y += YOffset, position.width, EditorGUI.GetPropertyHeight(displayedWave)),displayedWave);
    }
}