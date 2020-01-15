using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    // メインゲーム進行管理クラス

    [SerializeField, HeaderAttribute("メインゲームボタンリスト")]
    List<GameButton> gameButtons = new List<GameButton>();

    [SerializeField, HeaderAttribute("メインゲームデータ")]
    PlayerProvider playerProvider;
    [SerializeField] NormaProvider normaProvider;
    [SerializeField] ReactionProvider reactionProvider;

    Judgement judgement;
    CanvasManager canvasManager;
    Stage stage;
    CameraManager cameraManager;
    GameUGUI gameUGUI;
    CompleteUGUI compUGUI;

    int stageNo = 1;
    int compRate = 0;

    private void Awake()
    {
        SetResolution(1024); // 反映はフレーム更新後
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        judgement = GetComponent<Judgement>();
        canvasManager = GetComponent<CanvasManager>();
        stage = GetComponent<Stage>();
        cameraManager = GetComponent<CameraManager>();
        gameUGUI = GetComponent<GameUGUI>();
        compUGUI = GetComponent<CompleteUGUI>();

        stageNo = PlayerPrefs.GetInt("CurrentStageNo", 1);

        // UGUIの切り替え
        canvasManager.SetGame();
        // カメラの切り替え
        cameraManager.ViewMainCamera();

        // 各種ボタンの設定
        foreach (var button in gameButtons)
        {
            if (button.GetButtonType() == GameButton.Type.Dicide)
            {
                button.OnMyType += (type) => ConfirmJudgement();
            }
            else if (button.GetButtonType() == GameButton.Type.Retry)
            {

            }
            else if (button.GetButtonType() == GameButton.Type.Skip)
            {

            }
        }
    }

    // 成否判定処理
    void ConfirmJudgement()
    {
        gameUGUI.OnDicide();

        // 操作キャラの回転情報を確定
        playerProvider.SetRotateEnd();
        // 達成率を取得
        compRate = judgement.NormaJudgement(playerProvider.allRotateList, normaProvider.norma.normaList);

        // 成功だったら演出コルーチンを実行
        int rank = stage.GetStageClear(stageNo, compRate);

        if (rank > 0) StartCoroutine(SuccsessRoutine(rank));
        else StartCoroutine(FailedRoutine());
    }

    // 成功演出
    IEnumerator SuccsessRoutine(int rank)
    {
        // カメラの切り替え
        cameraManager.ViewReactionCamera();

        //yield return new WaitForSeconds(1f);

        // 成功演出再生
        StartCoroutine(reactionProvider.ReactionRoutine(stageNo));

        yield return new WaitForSeconds(5f);

        cameraManager.ViewMainCamera();

        // UGUIの切り替え
        canvasManager.SetComplete();
        compUGUI.SetRateState(compRate);

        int nextStageNo = stageNo + 1;
        if (nextStageNo > stage.stageNoMax) nextStageNo = 1;
        PlayerPrefs.SetInt("CurrentStageNo", nextStageNo);

        Debug.Log("成功演出");


        yield return null;
    }
    // 失敗演出
    IEnumerator FailedRoutine()
    {
        // 失敗演出
        playerProvider.PlayFailed();

        yield return new WaitForSeconds(2f);

        // UGUIの切り替え
        canvasManager.SetFailed();

        Debug.Log("失敗演出");


        yield return null;
    }

    void SetResolution(float baseResolution)
    {
        float screenRate = baseResolution / Screen.height;
        if (screenRate > 1) screenRate = 1;
        int width = (int)(Screen.width * screenRate);
        int height = (int)(Screen.height * screenRate);

        Screen.SetResolution(width, height, true);
    }
}
