using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WaveManager.Wave))]
public class WavePropertyDrawer : PropertyDrawer
{
    private const float YOffset = 3f;
    private float lineHeight = EditorGUIUtility.singleLineHeight;
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = YOffset * 7;
        height += lineHeight * 8;
        height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("enemyPrefabs"));
        
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //EditorGUI.indentLevel++;

        float yOffset = lineHeight + YOffset;
        
        position.y += lineHeight; //Small Space

        EditorGUI.indentLevel++;

        EditorGUIUtility.labelWidth = 190f;
        EditorGUI.PropertyField(new Rect(position.x, position.y, EditorGUIUtility.labelWidth + 35f, lineHeight),
            property.FindPropertyRelative("delayBetweenSpawns"));
        EditorGUI.PropertyField(new Rect(position.x, position.y+=yOffset, EditorGUIUtility.labelWidth + 35, lineHeight),
            property.FindPropertyRelative("delayBeforeWaveWarning"));
        EditorGUI.PropertyField(new Rect(position.x, position.y+=yOffset, EditorGUIUtility.labelWidth + 150, lineHeight),
            property.FindPropertyRelative("waveWarning"));
        EditorGUI.PropertyField(new Rect(position.x, position.y+=yOffset, EditorGUIUtility.labelWidth + 35, lineHeight),
            property.FindPropertyRelative("delayBeforeWaveStart"));
        
        
        position.y += lineHeight / 2;
 
        SerializedProperty enemyPrefabs = property.FindPropertyRelative("enemyPrefabs");
        SerializedProperty useCount = property.FindPropertyRelative("useCount");

        EditorGUI.PropertyField(new Rect(position.x, position.y+=yOffset, position.width, EditorGUI.GetPropertyHeight(enemyPrefabs)),enemyPrefabs);
        EditorGUIUtility.labelWidth = 80f;
        EditorGUI.PropertyField(new Rect(position.x, position.y+=(EditorGUI.GetPropertyHeight(enemyPrefabs)+YOffset), EditorGUIUtility.labelWidth + 35, lineHeight) ,useCount);
            
        EditorGUI.indentLevel++;
            
        EditorGUIUtility.labelWidth = 180f;
        if (useCount.boolValue)
        {
            SerializedProperty enemyCount = property.FindPropertyRelative("enemyCount");
            SerializedProperty maxEnemiesOnScreen = property.FindPropertyRelative("maxEnemiesOnScreen");
                
            EditorGUI.PropertyField(new Rect(position.x, position.y+=yOffset, EditorGUIUtility.labelWidth + 35, lineHeight),enemyCount);
            EditorGUI.PropertyField(new Rect(position.x, position.y+=yOffset, EditorGUIUtility.labelWidth + 35, lineHeight),maxEnemiesOnScreen);
        }
        else
        {
            SerializedProperty wavePower = property.FindPropertyRelative("wavePower");
            SerializedProperty maxPowerOnScreen = property.FindPropertyRelative("maxPowerOnScreen");
                
            EditorGUI.PropertyField(new Rect(position.x, position.y+=yOffset, EditorGUIUtility.labelWidth + 35, lineHeight),wavePower);
            EditorGUI.PropertyField(new Rect(position.x, position.y+=yOffset, EditorGUIUtility.labelWidth + 35, lineHeight),maxPowerOnScreen);
        }
        
        
        EditorGUI.EndProperty();
    }
}