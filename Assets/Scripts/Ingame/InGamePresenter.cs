using System;
using System.Reflection;
using R3;
using UnityEngine;

namespace CardGame
{
    public class InGamePresenter : MonoBehaviour
    {
        private InGameModel model;
        
        [SerializeField]
        private InGameView view;
        
        [SerializeField]
        private CardPlayPresenter cardPlayPresenter;
        
        //現在のターン
        private int currentTurn;

        private void Start()
        {
            model = new InGameModel();

            ChangeState(InGameEnum.GameState.DrawCards);
            
            Bind();
        }

        private void Bind()
        {
            cardPlayPresenter.FinishedPlayerTurn.Where(x=>x)
                .Subscribe(_ =>
                {
                    currentTurn++;
                })
                .AddTo(this);
            
            cardPlayPresenter.IsDate.Where(x =>x)
                .Subscribe(_=>ChangeState(InGameEnum.GameState.Date))
                .AddTo(this);
            
            
        }

        private InGameEnum.GameState ChangeState(InGameEnum.GameState state)
        {
            switch (state)
            {
                case InGameEnum.GameState.DrawCards:
                    Debug.Log("State: DrawCards");
                    // カード配布処理
                    break;
                case InGameEnum.GameState.PlayerTurn:
                    Debug.Log("State: PlayerTurn");
                    // プレイヤーがカードを選ぶフェーズ
                    break;
                case InGameEnum.GameState.CardEffect:
                    Debug.Log("State: CardEffect");
                    // カード効果の実行処理
                    break;
                case InGameEnum.GameState.Date:
                    Debug.Log("State: Date");
                    // デートイベント開始
                    break;
                case InGameEnum.GameState.Dialogue:
                    Debug.Log("State: Dialogue");
                    // 会話演出や分岐表示
                    break;
                case InGameEnum.GameState.EnemyTurn:
                    Debug.Log("State: EnemyTurn");
                    // 女神の反応や行動処理
                    break;
                case InGameEnum.GameState.CheckStatus:
                    Debug.Log("State: CheckStatus");
                    // 好感度、SPなどの状態更新
                    break;
                case InGameEnum.GameState.FinishTurn:
                    Debug.Log("State: FinishTurn");
                    //ターンを進める
                    InGameManager.Instance.NextTurn();
                    break;
                case InGameEnum.GameState.Confession:
                    Debug.Log("State: Confession");
                    // 告白フェーズ（選択肢や演出）
                    break;
                case InGameEnum.GameState.Ending:
                    Debug.Log("State: Ending");
                    // エンディングの分岐と表示
                    break;
                case InGameEnum.GameState.GameOver:
                    Debug.Log("State: GameOver");
                    // ゲームオーバー処理
                    break;
            }
            model.ChangeState(state);
            return state;
        }
    } 
}