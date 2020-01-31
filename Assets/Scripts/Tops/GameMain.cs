using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
    // メインゲーム進行管理クラス (ゲームプレイ, 成否演出)


    [SerializeField, HeaderAttribute("メインゲームボタンリスト")]
    List<GameButton> gameButtons = new List<GameButton>();

    [SerializeField, HeaderAttribute("メインゲームデータ")]
    PlayerProvider playerProvider;
    [SerializeField] StageProvider stageProvider;
    [SerializeField] CameraControll mainCamera;
    [SerializeField] BossLife bossLife;

    CanvasManager canvasManager;
    Config config;

    private void Awake()
    {
        canvasManager = GetComponent<CanvasManager>();
        canvasManager.SetHideGame();

        // 共有ステージデータ設定
        Stage.SetCheckPointFlg();

    }

    void Start()
    {
        foreach (var button in gameButtons)
        {
            if (button.GetButtonType() == GameButton.Type.Retry)
            {
                button.OnMyType += (type) => Retry();
            }
            else if (button.GetButtonType() == GameButton.Type.End_Retry)
            {
                button.OnMyType += (type) => Retry();
            }
            else if (button.GetButtonType() == GameButton.Type.End_Home)
            {
                button.OnMyType += (type) => Home();
            }
            else if (button.GetButtonType() == GameButton.Type.End_Next)
            {
                button.OnMyType += (type) => Retry();
            }
        }

        if (Stage.stageState.type == StageData.STAGE_TYPE.NORMAL)
        {
            // ゴール発火時処理
            stageProvider.stageManager.goalPoint.OnGoal += () => {
                StartCoroutine(GameClear());
            };
            // ゲームオーバー発火時処理
            playerProvider.OnGameOver += () => {
                StartCoroutine(NormalGameOver());
            };

            if (!Stage.isCheckPoint)
            {
                mainCamera.SetCameraPos(new Vector3(-38, 3, -3));
                mainCamera.SetCameraRotate(new Vector3(-5, 70, 0));
            }
            else
            {
                mainCamera.SetCameraPos(new Vector3(Stage.GetStartPos().x, 3, -10));
            }

            StartCoroutine(NormalGameStart());
        }
        else if (Stage.stageState.type == StageData.STAGE_TYPE.BOSS)
        {
            // ボス撃破時発火処理
            stageProvider.stageManager.boss.OnGameClear += () => {
                StartCoroutine(GameClear());
            };
            // ゲームオーバー発火時処理
            stageProvider.stageManager.boss.OnGameOver += () => {
                StartCoroutine(BossGameOver());
            };
            stageProvider.stageManager.bossFireCreatorList.ForEach(creator => {
                // ゲームオーバー発火時処理
                creator.OnGameOver += () => {
                    StartCoroutine(BossGameOver());
                };
                // 背景変化処理
                creator.OnFireScaleUp += (alpha) => {

                };
            });
            stageProvider.stageManager.boss.OnHitWater += () => {
                bossLife.Damage();
            };

            mainCamera.SetCameraPos(new Vector3(0, 3, -55));
            bossLife.SetState(stageProvider.stageManager.boss.myLife);

            StartCoroutine(BossGameStart());
        }

    }

    IEnumerator NormalGameStart()
    {
        if (!Stage.isCheckPoint) playerProvider.NormalGameBegin();

        yield return new WaitForSeconds(2f);

        if (!Stage.isCheckPoint)
        {
            stageProvider.NormalGameBegin();

            yield return new WaitWhile(() => stageProvider.IsNormalBegin());

            mainCamera.MoveCamera(new Vector3(Stage.GetStartPos().x, 3, -10), 1f);
            mainCamera.RotateCamera(new Vector3(-5, 0, 0), 1f);

            yield return new WaitForSeconds(1f);
        }
        else
        {
            stageProvider.GameStart();

            yield return new WaitForSeconds(1f);
        }

        canvasManager.SetGame();
        playerProvider.NormalGameStart();
    }

    IEnumerator BossGameStart()
    {
        playerProvider.BossGameBegin();
        stageProvider.BossGameBegin();

        mainCamera.MoveCamera(new Vector3(0, 3, -5), 2f);

        yield return new WaitForSeconds(4f);

        canvasManager.SetGame();
        playerProvider.BossGameStart();
    }

    IEnumerator GameClear()
    {
        if (Stage.stageState.type == StageData.STAGE_TYPE.NORMAL)
        {
            playerProvider.NormalGameClear();

            mainCamera.MoveCamera(new Vector3(playerProvider.player.transform.position.x - 10f, 3, 0), 1f);
            mainCamera.RotateCamera(new Vector3(-5, 90, 0), 1f);
            yield return new WaitForSeconds(1f);

            playerProvider.player.NormalResult();
            stageProvider.stageManager.goalPoint.PlayResult();
            yield return new WaitForSeconds(2f);

        }
        else if (Stage.stageState.type == StageData.STAGE_TYPE.BOSS)
        {
            playerProvider.BossGameClear();
            stageProvider.stageManager.BossGameClear();

            yield return new WaitForSeconds(2f);
        }


        canvasManager.SetComplete();
        Stage.SetClearStageNo();
    }
    IEnumerator NormalGameOver()
    {
        playerProvider.NormalGameOver();
        stageProvider.NormalGameOver();

        yield return new WaitForSeconds(1f);

        canvasManager.SetFailed();
    }
    IEnumerator BossGameOver()
    {
        playerProvider.BossGameOver();
        stageProvider.BossGameOver();

        yield return new WaitForSeconds(1f);

        canvasManager.SetFailed();

    }
    void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Home()
    {
        SceneManager.LoadScene("Start");
    }
}
