using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardScriptableObject))]
public class CardScriptableObjectEditor : Editor
{
    private SerializedProperty additionalEffectProp;

    private void OnEnable()
    {
        // リスト型プロパティ（AdditionalEffect）を取得
        additionalEffectProp = serializedObject.FindProperty("additionalEffect");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // 共通プロパティの描画
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playCostAffection"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playActionPoints"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardSprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardType"));

        var useReqProp = serializedObject.FindProperty("useRequirmentPlayerParameter");
        EditorGUILayout.PropertyField(useReqProp);

        if (useReqProp.boolValue)
        {
            var paramProp = serializedObject.FindProperty("requirmentPlayerParameter");
            if (paramProp != null)
            {
                EditorGUILayout.PropertyField(paramProp, true);
            }
            else
            {
                EditorGUILayout.HelpBox("requirmentPlayerParameter is null or could not be found.", MessageType.Warning);
            }
        }

        // AdditionalEffect の手動描画
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Additional Effects", EditorStyles.boldLabel);

        if (additionalEffectProp != null)
        {
            for (int i = 0; i < additionalEffectProp.arraySize; i++)
            {
                var element = additionalEffectProp.GetArrayElementAtIndex(i);
                if (element.managedReferenceValue == null) continue;

                var effectObj = element.managedReferenceValue as AdditionalEffect;
                if (effectObj != null)
                {
                    EditorGUILayout.BeginVertical("box");
                    effectObj.effectName = EditorGUILayout.TextField("Effect Name", effectObj.effectName);
                    EditorGUILayout.LabelField("Type", effectObj.GetType().Name);

                    if (GUILayout.Button("Remove"))
                    {
                        additionalEffectProp.DeleteArrayElementAtIndex(i);
                        break;
                    }

                    EditorGUILayout.EndVertical();
                }
            }

            if (GUILayout.Button("Add CostBypass"))
            {
                additionalEffectProp.arraySize++;
                var newElement = additionalEffectProp.GetArrayElementAtIndex(additionalEffectProp.arraySize - 1);
                newElement.managedReferenceValue = new CostBypassCard
                {
                    effectName = "Cost Bypass"
                };
            }
        }

        // カードタイプごとの個別フィールド描画
        var cardTypeProp = serializedObject.FindProperty("cardType");
        var cardType = (CardScriptableObject.cardTypes)cardTypeProp.enumValueIndex;

        switch (cardType)
        {
            case CardScriptableObject.cardTypes.ManaUp:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("affectionUpNum"));
                break;
            case CardScriptableObject.cardTypes.ParameterChange:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("addParameterNum"));
                break;
            case CardScriptableObject.cardTypes.ActionIncrease:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("addAP"));
                break;
            case CardScriptableObject.cardTypes.Debuff:
            case CardScriptableObject.cardTypes.Persistent:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("keepTurns"));
                break;
            case CardScriptableObject.cardTypes.ReturnFromGrave:
            case CardScriptableObject.cardTypes.Search:
            case CardScriptableObject.cardTypes.NoAffectionPenalty:
            case CardScriptableObject.cardTypes.HandSwap:
            case CardScriptableObject.cardTypes.DrawAndTrash:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("searchCardType"));
                break;
            case CardScriptableObject.cardTypes.TrashDiscard:
            case CardScriptableObject.cardTypes.CardExchange:
            case CardScriptableObject.cardTypes.Dating:
                EditorGUILayout.HelpBox("このカードタイプに特有の処理が必要です（個別フィールド未設定）", MessageType.Info);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}