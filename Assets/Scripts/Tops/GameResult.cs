using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameResult : MonoBehaviour
{
    // リザルト進行管理クラス (リザルト, サプライズボックス)

    [SerializeField, HeaderAttribute("リザルトボタンリスト")]
    List<GameButton> gameButtons = new List<GameButton>();

    [SerializeField, HeaderAttribute("他データ")]
    Rank rank;
    [SerializeField] ChestManager chestManager;
    [SerializeField] CompleteUGUI compUGUI;

    CanvasManager canvasManager;
    Stage stage;
    CameraManager cameraManager;

    public event Action OnNext;
    public event Action OnRetry;
    public event Action OnHome;

    void Start()
    {
        // 各種ボタンの設定
        foreach (var button in gameButtons)
        {
            if (button.GetButtonType() == GameButton.Type.End_Craim)
            {
                button.OnMyType += (type) => GetCraim();
            }
            else if (button.GetButtonType() == GameButton.Type.End_Next)
            {
                button.OnMyType += (type) => Next();
            }
            else if (button.GetButtonType() == GameButton.Type.End_Retry)
            {
                button.OnMyType += (type) => Retry();
            }
            else if (button.GetButtonType() == GameButton.Type.End_Home)
            {
                button.OnMyType += (type) => Home();
            }
        }
    }

    // リザルト時の各種設定、保存を行う
    public void SetResult(int stageNo, int rankNum, int rateNum)
    {
        // 達成率の設定
        compUGUI.SetRateState(rateNum);
        // ランクの設定
        rank.ResultRank(stageNo, rankNum);
    }

    void GetCraim()
    {

    }
    void Next()
    {
        OnNext?.Invoke();
    }
    void Retry()
    {
        OnRetry?.Invoke();
    }
    void Home()
    {
        OnHome?.Invoke();
    }
}
