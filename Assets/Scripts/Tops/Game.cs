using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // メインゲーム進行管理クラス

    enum GAME_STATE
    {
        START,
        SKIN,
        PLAY,
        COMPLETE,
        FAILED,
        SURPRISE,
    };

    GAME_STATE state = GAME_STATE.START;

    [SerializeField, HeaderAttribute("メインゲームボタンリスト")]
    List<GameButton> gameButtons = new List<GameButton>();

    [SerializeField, HeaderAttribute("メインゲームデータ")]
    PlayerProvider playerProvider;
    [SerializeField] NormaProvider normaProvider;
    [SerializeField] ReactionProvider reactionProvider;
    [SerializeField] ChestManager chestManager;

    Judgement judgement;
    CanvasManager canvasManager;
    Stage stage;
    Config config;
    CameraManager cameraManager;
    GameUGUI gameUGUI;
    CompleteUGUI compUGUI;
    Result result;

    int stageNo = 1;
    int compRate = 0;
    int surpriseGetCount = 0;

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
        config = GetComponent<Config>();
        cameraManager = GetComponent<CameraManager>();
        gameUGUI = GetComponent<GameUGUI>();
        compUGUI = GetComponent<CompleteUGUI>();
        result = GetComponent<Result>();

        stageNo = PlayerPrefs.GetInt("CurrentStageNo", 1);
        surpriseGetCount = PlayerPrefs.GetInt("SurpriseGetCount", 0);


        var gameMode = PlayerPrefs.GetString("GameMode", GAME_STATE.START.ToString());

        // UGUIの切り替え
        if (gameMode == GAME_STATE.START.ToString())
        {
            canvasManager.SetStart();
            playerProvider.InitStart();
        }
        else if (gameMode == GAME_STATE.PLAY.ToString())
        {
            canvasManager.SetGame();
            playerProvider.GameStart();
        }

        // カメラの切り替え
        cameraManager.ViewMainCamera();

        // 各種ボタンの設定
        foreach (var button in gameButtons)
        {
            if (button.GetButtonType() == GameButton.Type.Dicide)
            {
                button.OnMyType += (type) => ConfirmJudgement();
            }
            else if (button.GetButtonType() == GameButton.Type.Skip)
            {

            }
            else if (button.GetButtonType() == GameButton.Type.Start)
            {
                button.OnMyType += (type) => GameStart();
            }
            else if (button.GetButtonType() == GameButton.Type.Skin)
            {
                button.OnMyType += (type) => Skin();
            }
            else if (button.GetButtonType() == GameButton.Type.Select)
            {
                button.OnMyType += (type) => Select();
            }
            else if (button.GetButtonType() == GameButton.Type.Config_OnOff)
            {
                button.OnMyType += (type) => ConfigOnOff();
            }
            else if (button.GetButtonType() == GameButton.Type.Config_Sound)
            {
                button.OnMyType += (type) => ConfigSound();
            }
        }

        result.OnHome += () => {
            PlayerPrefs.SetString("GameMode", GAME_STATE.START.ToString());
            SceneManager.LoadScene("Game");
        };
        result.OnNext += () => {
            // サプライズボックス報酬が獲得できるか
            if (surpriseGetCount >= 1)
            {
                cameraManager.SurpriseCamera();
                canvasManager.SetSurprise();
            }
            else
            {
                PlayerPrefs.SetString("GameMode", GAME_STATE.PLAY.ToString());
                SceneManager.LoadScene("Game");
            }
        };
        result.OnRetry += () => {
            PlayerPrefs.SetString("GameMode", GAME_STATE.PLAY.ToString());
            SceneManager.LoadScene("Game");
        };
    }

    // ゲーム開始処理
    void GameStart()
    {
        playerProvider.GameStart();
        // UGUIの切り替え
        canvasManager.SetGame();
    }
    // 各種画面切り替え処理
    void Skin()
    {

    }
    void Select()
    {

    }
    // コンフィグ処理
    void ConfigOnOff()
    {
        config.ViewConfig();
    }
    void ConfigSound()
    {
        config.SetSound();
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

        // データの更新
        int nextStageNo = stageNo + 1;
        if (nextStageNo > stage.stageNoMax) nextStageNo = 1;
        PlayerPrefs.SetInt("CurrentStageNo", nextStageNo);

        surpriseGetCount++;
        if (surpriseGetCount < 5) PlayerPrefs.SetInt("SurpriseGetCount", surpriseGetCount);

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
