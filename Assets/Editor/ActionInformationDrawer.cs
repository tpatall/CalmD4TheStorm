using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ActionInformation))]
public class ActionInformationDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        // 7 custom properties including label + 2 pixels inbetween
        int amountOfProperties = 7;
        return EditorGUIUtility.singleLineHeight * (amountOfProperties + 1) + (amountOfProperties * 3);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        Rect labelPosition = new Rect(position.min.x, position.min.y, position.size.x, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(labelPosition, label);

        Rect actionTypeRect = new Rect(position.x, position.y + 20, position.width, 16);
        Rect energyCostRect = new Rect(position.x, position.y + 40, position.width, 16);

        var actionType = property.FindPropertyRelative("ActionType");

        EditorGUI.indentLevel++;

        EditorGUI.PropertyField(actionTypeRect, property.FindPropertyRelative("ActionType"));
        EditorGUI.PropertyField(energyCostRect, property.FindPropertyRelative("EnergyCost"));

        Rect targetTypeRect, diceTypeRect, diceCountRect, upgradeTypeRect, upgradeCostRect;
        SerializedProperty upgradeType;
        switch ((ActionIcon) actionType.enumValueIndex) {
            case ActionIcon.ATTACK:
                targetTypeRect = new Rect(position.x, position.y + 60, position.width, 16);
                diceTypeRect = new Rect(position.x, position.y + 80, position.width, 16);
                diceCountRect = new Rect(position.x, position.y + 100, position.width, 16);
                upgradeTypeRect = new Rect(position.x, position.y + 120, position.width, 16);
                
                EditorGUI.PropertyField(targetTypeRect, property.FindPropertyRelative("TargetType"));
                EditorGUI.PropertyField(diceTypeRect, property.FindPropertyRelative("DiceType"));
                EditorGUI.PropertyField(diceCountRect, property.FindPropertyRelative("DiceCount"));

                upgradeType = property.FindPropertyRelative("UpgradeType");

                EditorGUI.PropertyField(upgradeTypeRect, property.FindPropertyRelative("UpgradeType"));
                
                if ((UpgradeType) upgradeType.enumValueIndex != UpgradeType.NONE) {
                    upgradeCostRect = new Rect(position.x, position.y + 140, position.width, 16);
                    EditorGUI.PropertyField(upgradeCostRect, property.FindPropertyRelative("UpgradeCost"));
                }

                break;
            case ActionIcon.BLOCK:
                diceTypeRect = new Rect(position.x, position.y + 60, position.width, 16);
                diceCountRect = new Rect(position.x, position.y + 80, position.width, 16);
                upgradeTypeRect = new Rect(position.x, position.y + 100, position.width, 16);

                EditorGUI.PropertyField(diceTypeRect, property.FindPropertyRelative("DiceType"));
                EditorGUI.PropertyField(diceCountRect, property.FindPropertyRelative("DiceCount"));

                upgradeType = property.FindPropertyRelative("UpgradeType");

                EditorGUI.PropertyField(upgradeTypeRect, property.FindPropertyRelative("UpgradeType"));

                if ((UpgradeType)upgradeType.enumValueIndex != UpgradeType.NONE) {
                    upgradeCostRect = new Rect(position.x, position.y + 120, position.width, 16);
                    EditorGUI.PropertyField(upgradeCostRect, property.FindPropertyRelative("UpgradeCost"));
                }
                break;
            case ActionIcon.POISON:
                targetTypeRect = new Rect(position.x, position.y + 60, position.width, 16);
                diceTypeRect = new Rect(position.x, position.y + 80, position.width, 16);
                upgradeTypeRect = new Rect(position.x, position.y + 100, position.width, 16);
                
                EditorGUI.PropertyField(targetTypeRect, property.FindPropertyRelative("TargetType"));
                EditorGUI.PropertyField(diceTypeRect, property.FindPropertyRelative("DiceType"));

                upgradeType = property.FindPropertyRelative("UpgradeType");

                EditorGUI.PropertyField(upgradeTypeRect, property.FindPropertyRelative("UpgradeType"));

                if ((UpgradeType)upgradeType.enumValueIndex != UpgradeType.NONE) {
                    upgradeCostRect = new Rect(position.x, position.y + 120, position.width, 16);
                    EditorGUI.PropertyField(upgradeCostRect, property.FindPropertyRelative("UpgradeCost"));
                }
                break;
            case ActionIcon.DEBUFF:
                targetTypeRect = new Rect(position.x, position.y + 54, position.width, 16);
                
                EditorGUI.PropertyField(targetTypeRect, property.FindPropertyRelative("TargetType"));
                break;
            case ActionIcon.HEAL:
                diceTypeRect = new Rect(position.x, position.y + 60, position.width, 16);
                diceCountRect = new Rect(position.x, position.y + 80, position.width, 16);
                upgradeTypeRect = new Rect(position.x, position.y + 100, position.width, 16);
                
                EditorGUI.PropertyField(diceTypeRect, property.FindPropertyRelative("DiceType"));
                EditorGUI.PropertyField(diceCountRect, property.FindPropertyRelative("DiceCount"));

                upgradeType = property.FindPropertyRelative("UpgradeType");

                EditorGUI.PropertyField(upgradeTypeRect, property.FindPropertyRelative("UpgradeType"));

                if ((UpgradeType)upgradeType.enumValueIndex != UpgradeType.NONE) {
                    upgradeCostRect = new Rect(position.x, position.y + 120, position.width, 16);
                    EditorGUI.PropertyField(upgradeCostRect, property.FindPropertyRelative("UpgradeCost"));
                }
                break;
            default:
                break;
        }

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }
}
