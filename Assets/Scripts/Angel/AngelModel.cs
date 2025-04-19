using UnityEngine;

/// <summary>
/// 恋愛対象キャラクター「Angel」のパラメーター管理クラス
/// </summary>
public class AngelModel
{
    // プレイヤーに対する好意度
    private float affinity;
    public float Affinity => affinity;

    // プレイヤーに対する信頼度
    private float trust;
    public float Trust => trust;

    // 嫉妬度（他のキャラと仲良くすると上がる）
    private float jealousy;
    public float Jealousy => jealousy;

    // ライバル度（他キャラとの競合）
    private float rivalry;
    public float Rivalry => rivalry;

    // シナリオでの秘密度（未開示の情報など）
    private float secretLevel;
    public float SecretLevel => secretLevel;

    // キャラクターの感情（例：怒り・喜びなどの状態値）
    private float emotion;
    public float Emotion => emotion;

    public void UpdateParameter(Parameters parameters)
    {
        affinity = parameters.affinity;
        trust = parameters.trust;
        jealousy = parameters.jealousy;
        rivalry = parameters.rivalry;
        secretLevel = parameters.secretLevel;
        emotion = parameters.emotion;
    }

    // すべての値を初期化
    public void Initialize()
    {
        affinity = 0f;
        trust = 0f;
        jealousy = 0f;
        rivalry = 0f;
        secretLevel = 0f;
        emotion = 0f;
    }

    public AngelModel()
    {
        affinity = 0f;
        trust = 0f;
        jealousy = 0f;
        rivalry = 0f;
        secretLevel = 0f;
        emotion = 0f;
    }
}　　