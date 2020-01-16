using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Result : MonoBehaviour
{
    // リザルト進行クラス

    [SerializeField, HeaderAttribute("リザルトボタンリスト")]
    List<GameButton> gameButtons = new List<GameButton>();

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
