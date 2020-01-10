using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    // リザルト進行クラス

    [SerializeField, HeaderAttribute("リザルトボタンリスト")]
    List<GameButton> gameButtons = new List<GameButton>();

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
        SceneManager.LoadScene("Game");
    }
    void Retry()
    {
        SceneManager.LoadScene("Game");
    }
    void Home()
    {
        SceneManager.LoadScene("Game");
    }
}
