using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // メインゲーム進行管理クラス

    [SerializeField, HeaderAttribute("メインゲームボタンリスト")]
    List<GameButton> gameButtons = new List<GameButton>();

    [SerializeField, HeaderAttribute("メインゲームデータ")]
    PlayerProvider playerProvider;
    [SerializeField] Norma norma;
    [SerializeField] Slider slider;

    Judgement judgement;
    CanvasManager canvasManager;
    Stage stage;

    int stageNo { get; set; } = 1;

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

        stageNo = PlayerPrefs.GetInt("CurrentStageNo", 1);

        // UGUIの切り替え
        canvasManager.SetGame();

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
        // 操作キャラの回転情報を確定
        playerProvider.Judgement();
        // 達成率を取得
        int rate = judgement.NormaJudgement(playerProvider.allRotateList, norma.normaList);
        slider.value = rate;
        Debug.Log("rate = " + rate);

        // 成功だったら演出コルーチンを実行
        int rank = stage.GetStageClear(stageNo, rate);

        if (rank > 0) StartCoroutine(SuccsessRoutine(rank));
        else StartCoroutine(FailedRoutine());
    }

    // 成功演出
    IEnumerator SuccsessRoutine(int rank)
    {
        // UGUIの切り替え
        canvasManager.SetComplete();

        Debug.Log("成功演出");


        yield return null;
    }
    // 失敗演出
    IEnumerator FailedRoutine()
    {
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
