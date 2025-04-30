using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Ingame
{
    public class InGamePresenter : MonoBehaviour
    {
        private InGameModel model;
        
        [SerializeField]
        private InGameView view;
        
        [SerializeField]
        private CardPlayPresenter cardPlayPresenter;
        
        [SerializeField]
        private AngelPresenter angelPresenter;

        //ゲーム開始時のドロープール
        [SerializeField] private List<CardScriptableObject.cardTypes> startCardPool = new List<CardScriptableObject.cardTypes>();

        //ゲーム開始時にカードを引く枚数
        [SerializeField] private int startGetCardNum = 5;
        

        private void Start()
        {
            model = new InGameModel();

            ChangeState(InGameEnum.GameState.DrawCards);
            
            Bind();
        }

        private void Bind()
        {
            model.CurrentIngameState.Subscribe(x => InGameManager.Instance.ChangeState(x))
                .AddTo(this);
            
            view.TurnEndButton.OnClickAsObservable()
                .Where(_=>model.CurrentIngameState.Value == InGameEnum.GameState.PlayerTurn)
                .Subscribe(_ =>
                {
                    ChangeState(InGameEnum.GameState.EnemyTurn);
                })
                .AddTo(this);
            
            view.TalkButton.OnClickAsObservable()
                .Where(_=>model.CurrentIngameState.Value == InGameEnum.GameState.PlayerTurn)
                .Subscribe(_=>ChangeState(InGameEnum.GameState.Talk))
                .AddTo(this);
        }

        private void ChangeState(InGameEnum.GameState state)
        {
            model.ChangeState(state);
            switch (state)
            {
                case InGameEnum.GameState.DrawCards:
                    Debug.Log("State: DrawCards");
                    
                    //ゲーム開始時のドロー
                    cardPlayPresenter.StartDrawCards(startCardPool,startGetCardNum);
                    
                    //UniTaskで演出を入れる
                    ChangeState(InGameEnum.GameState.PlayerTurn);
                    break;
                case InGameEnum.GameState.PlayerTurn:
                    Debug.Log("State: PlayerTurn");
                    // プレイヤーがカードを選ぶフェーズ
                    break;
                case InGameEnum.GameState.CardEffect:
                    Debug.Log("State: CardEffect");
                    // カード効果の実行処理
                    ChangeState(InGameEnum.GameState.PlayerTurn);
                    break;
                case InGameEnum.GameState.Date:
                    Debug.Log("State: Date");
                    // デートイベント開始
                    break;
                case InGameEnum.GameState.Talk:
                    Debug.Log("State: Dialogue");
                    // 会話演出や分岐表示
                    
                    ChangeState(InGameEnum.GameState.EnemyTurn);
                    break;
                case InGameEnum.GameState.EnemyTurn:
                    Debug.Log("State: EnemyTurn");
                    // 女神の反応や行動処理UniTaskで
                    
                    //会話の返り値を利用
                    
                    // angelPresenter.UpdateAngel(parameter);
                    // cardPlayPresenter.AddCard(card);
                    
                    ChangeState(InGameEnum.GameState.CheckStatus);
                    break;
                case InGameEnum.GameState.CheckStatus:
                    Debug.Log("State: CheckStatus");
                    // 好感度、SPなどの状態更新
                    
                    ChangeState(InGameEnum.GameState.FinishTurn);
                    break;
                case InGameEnum.GameState.FinishTurn:
                    Debug.Log("State: FinishTurn");
                    //ターンを進める
                    InGameManager.Instance.NextTurn();
                    
                    ChangeState(InGameEnum.GameState.PlayerTurn);
                    break;
                
                case InGameEnum.GameState.Confession:
                    Debug.Log("State: Confession");
                    // 告白フェーズ（選択肢や演出）UniTaskで告白フェーズが終われば
                    
                    ChangeState(InGameEnum.GameState.CheckStatus);
                    
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
        }
    } 
}