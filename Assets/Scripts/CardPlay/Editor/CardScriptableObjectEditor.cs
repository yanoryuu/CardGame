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
        // 更新開始
        serializedObject.Update();

        // 共通のカードプロパティ
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playCostAffection"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playActionPoints"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardSprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardType"));

        // ▼ additionalEffect の手動描画（自動描画しない）
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Additional Effects", EditorStyles.boldLabel);

        for (int i = 0; i < additionalEffectProp.arraySize; i++)
        {
            var element = additionalEffectProp.GetArrayElementAtIndex(i);
            if (element.managedReferenceValue == null) continue;

            var effectObj = element.managedReferenceValue as AdditionalEffect;
            if (effectObj != null)
            {
                EditorGUILayout.BeginVertical("box");
                effectObj.effectName = EditorGUILayout.TextField("Effect Name", effectObj.effectName); // 名前の表示・編集
                EditorGUILayout.LabelField("Type", effectObj.GetType().Name); // 型名の表示

                if (GUILayout.Button("Remove"))
                {
                    additionalEffectProp.DeleteArrayElementAtIndex(i);
                    break; // リストが変化したのでループを中断
                }

                EditorGUILayout.EndVertical();
            }
        }

        // ▼ ボタンで CostBypassCard を追加
        if (GUILayout.Button("Add CostBypass"))
        {
            additionalEffectProp.arraySize++;
            var newElement = additionalEffectProp.GetArrayElementAtIndex(additionalEffectProp.arraySize - 1);
            newElement.managedReferenceValue = new CostBypassCard
            {
                effectName = "Cost Bypass"
            };
        }

        // ▼ カードタイプによる個別プロパティ表示
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
                EditorGUILayout.PropertyField(serializedObject.FindProperty("searchCardType"));
                break;
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

        // 更新反映
        serializedObject.ApplyModifiedProperties();
    }
}
