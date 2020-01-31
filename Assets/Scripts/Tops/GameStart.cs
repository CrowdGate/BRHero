using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // ゲームスタート進行管理クラス (タイトル, スキン, ステージ選択)

    [SerializeField, HeaderAttribute("ゲームスタートボタンリスト")]
    List<GameButton> gameButtons = new List<GameButton>();

    CanvasManager canvasManager;


    private void Start()
    {
        canvasManager = GetComponent<CanvasManager>();
        canvasManager.SetStart();

        // 各種ボタンの設定
        foreach (var button in gameButtons)
        {
            if (button.GetButtonType() == GameButton.Type.Start)
            {
                button.OnMyType += (type) => Game();
            }
        }
    }

    // ゲーム開始処理
    void Game()
    {
        SceneManager.LoadScene("Game");
    }
}
