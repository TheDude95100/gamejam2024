using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilityData))]
public class AbilityDataEditor : Editor
{
    public SerializedProperty abilityName;
    public SerializedProperty isUnlockedAbility;

    public SerializedProperty activeAbility;

    public SerializedProperty castingTime;
    public SerializedProperty cooldown;
    public SerializedProperty baseDamage;

    public SerializedProperty bonusDamage;
    public SerializedProperty bonusAttackSpeed;
    public SerializedProperty bonusLife;
    public SerializedProperty bonusMovementSpeed;

    private void OnEnable()
    {
        abilityName = serializedObject.FindProperty("abilityName");
        isUnlockedAbility = serializedObject.FindProperty("isUnlockedAbility");

        activeAbility = serializedObject.FindProperty("activeAbility");

        castingTime = serializedObject.FindProperty("castingTime");
        cooldown = serializedObject.FindProperty("cooldown");
        baseDamage = serializedObject.FindProperty("baseDamage");

        bonusDamage = serializedObject.FindProperty("bonusDamage");
        bonusAttackSpeed = serializedObject.FindProperty("bonusAttackSpeed");
        bonusLife = serializedObject.FindProperty("bonusLife");
        bonusMovementSpeed = serializedObject.FindProperty("bonusMovementSpeed");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();

        EditorGUILayout.PropertyField(abilityName, new GUIContent("Name"));
        if (abilityName.stringValue == string.Empty)
        {
            EditorGUILayout.HelpBox("Caution: No name specified. Please name the monster!", MessageType.Warning);
        }

        EditorGUILayout.PropertyField(isUnlockedAbility, new GUIContent("Is the ability unlocked?"));
        EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(activeAbility, new GUIContent("Is it an active ability?"));
        EditorGUILayout.Space(10);

        if(activeAbility.boolValue)
        {
            EditorGUI.indentLevel++;

            EditorGUI.indentLevel--;
        }
        else
        {
            EditorGUI.indentLevel++;

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }

}
