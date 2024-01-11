using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilityData))]
public class AbilityDataEditor : Editor
{
    public SerializedProperty abilityName;
    public SerializedProperty abilityIcon;
    public SerializedProperty description;
    public SerializedProperty isUnlockedAbility;

    public SerializedProperty isActiveAbility;

    public SerializedProperty baseDamage;
    public SerializedProperty castingTime;
    public SerializedProperty cooldown;
    public SerializedProperty duration;
    public SerializedProperty attackRange;

    public SerializedProperty hasPassiveBonus;

    public SerializedProperty bonusLife;
    public SerializedProperty bonusDamage;
    public SerializedProperty bonusDefense;
    public SerializedProperty bonusAttackSpeed;
    public SerializedProperty bonusMovementSpeed;
    public SerializedProperty bonusExperience;

    private void OnEnable()
    {
        abilityName = serializedObject.FindProperty("abilityName");
        abilityIcon = serializedObject.FindProperty("abilityIcon");
        description = serializedObject.FindProperty("description");
        isUnlockedAbility = serializedObject.FindProperty("isUnlockedAbility");

        isActiveAbility = serializedObject.FindProperty("isActiveAbility");

        baseDamage = serializedObject.FindProperty("baseDamage");
        castingTime = serializedObject.FindProperty("castingTime");
        cooldown = serializedObject.FindProperty("cooldown");
        duration = serializedObject.FindProperty("duration");
        attackRange = serializedObject.FindProperty("attackRange");

        hasPassiveBonus = serializedObject.FindProperty("hasPassiveBonus");

        bonusLife = serializedObject.FindProperty("bonusLife");
        bonusDamage = serializedObject.FindProperty("bonusDamage");
        bonusDefense = serializedObject.FindProperty("bonusDefense");
        bonusAttackSpeed = serializedObject.FindProperty("bonusAttackSpeed");
        bonusMovementSpeed = serializedObject.FindProperty("bonusMovementSpeed");
        bonusExperience = serializedObject.FindProperty("bonusExperience");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();

        EditorGUILayout.LabelField("General Info", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(abilityName, new GUIContent("Name"));
        if (abilityName.stringValue == string.Empty)
        {
            EditorGUILayout.HelpBox("Caution: No name specified. Please name the ability!", MessageType.Warning);
        }
        EditorGUILayout.PropertyField(abilityIcon, new GUIContent("Ability icon"));
        EditorGUILayout.PropertyField(description, new GUIContent("Description"));

        EditorGUILayout.PropertyField(isUnlockedAbility, new GUIContent("Is the ability unlocked?"));
        EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(isActiveAbility, new GUIContent("Is it an active ability?"));
        EditorGUILayout.Space(5);

        if(isActiveAbility.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Active ability stats", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(baseDamage, new GUIContent("Damage"));
            EditorGUILayout.PropertyField(castingTime, new GUIContent("Casting time"));
            EditorGUILayout.PropertyField(cooldown, new GUIContent("Cooldown"));
            EditorGUILayout.PropertyField(duration, new GUIContent("Duration"));
            EditorGUILayout.PropertyField(attackRange, new GUIContent("Attack range"));

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(hasPassiveBonus, new GUIContent("Passive bonuses?"));
        EditorGUILayout.Space(10);

        if (hasPassiveBonus.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Passive bonus stats", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(bonusLife, new GUIContent("Bonus life"));
            EditorGUILayout.PropertyField(bonusDamage, new GUIContent("Bonus damage"));
            EditorGUILayout.PropertyField(bonusDefense, new GUIContent("Bonus defense"));
            EditorGUILayout.PropertyField(bonusAttackSpeed, new GUIContent("Bonus attack speed"));
            EditorGUILayout.PropertyField(bonusMovementSpeed, new GUIContent("Bonus movement speed"));
            EditorGUILayout.PropertyField(bonusExperience, new GUIContent("Bonus experience"));

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }

}
