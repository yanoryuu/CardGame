using UnityEngine;

public class InGameEnum
{
    public enum GameState
    {
        Default,
        DrawCards,       // カード配布フェーズ
        PlayerTurn,      // プレイヤーのターン（カード選択/会話）
        CardEffect,      // カード効果の発動処理
        Date,
        Dialogue,        // 会話シーン（カード効果 or イベント）
        EnemyTurn,       // 女神のターン（女神の反応・手札公開など）
        FinishTurn,      // ターンの終了
        CheckStatus,     // 状態更新（好感度・SP等）
        Confession,      // 告白フェーズ
        Ending,          // エンディング分岐表示
        GameOver         // 好感度0などのバッドエンド
    }
}
