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
    [SerializeField] GameUGUI gameUGUI;

    CanvasManager canvasManager;
    Config config;

    private void Awake()
    {
        canvasManager = GetComponent<CanvasManager>();
        canvasManager.SetHideGame();

        // 共有ステージデータ設定
        Stage.SetStageFlgs();
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
            // チェックポイント発火処理
            stageProvider.stageManager.OnCheckPoint += () => {
                gameUGUI.SetCheckPointIcon();
            };
            // ゲームオーバー発火時処理
            playerProvider.OnGameOver += () => {
                StartCoroutine(NormalGameOver());
            };

            if (Stage.isChapterBegin)
            {
                mainCamera.SetCameraLocalPos(new Vector3(-8, 5, -10));
                mainCamera.SetCameraLocalRotate(new Vector3(-5, 50, 0));
            }

            // その他設定

            StartCoroutine(NormalGameStart());
        }
        else if (Stage.stageState.type == StageData.STAGE_TYPE.BOSS)
        {
            // Boss関連
            stageProvider.stageManager.boss.OnGameClear += () => {
                StartCoroutine(GameClear());
            };
            stageProvider.stageManager.boss.OnGameOver += () => {
                StartCoroutine(BossGameOver());
            };
            stageProvider.stageManager.boss.OnHitWater += () => {

            };
            stageProvider.stageManager.boss.OnBeginPhase1 += () => {

            };
            stageProvider.stageManager.boss.OnBeginPhase2 += () => {
                playerProvider.player.SetBossPhase2(stageProvider.stageManager.boss.transform);
                stageProvider.stageManager.Phase2();
            };

            // BossFireCreator関連
            stageProvider.stageManager.bossFireCreator.OnGameOver += () => {
                StartCoroutine(BossGameOver());
            };
            stageProvider.stageManager.bossFireCreator.OnFireDanger += (weight) => {
                playerProvider.SetPostProcess(weight);
            };
            stageProvider.stageManager.bossFireCreator.OnDeadBossFire += () => {
                stageProvider.stageManager.boss.DamageFireLife(1);
                gameUGUI.Damage(1);
            };

            // その他設定
            mainCamera.SetCameraLocalPos(new Vector3(-6, 3, -2));
            mainCamera.SetCameraLocalRotate(new Vector3(-5, 80, 0));

            gameUGUI.SetBossLife(stageProvider.stageManager.boss.GetFireLife());

            StartCoroutine(BossGameStart());
        }

        // ステージギミックのアクション管理
        stageProvider.stageManager.gimmickList.ForEach(gimmick => {
            // ジャンプギミック
            gimmick.OnJump += (jumpEndPos) => {
                StartCoroutine(playerProvider.JumpRoutine(jumpEndPos));
            };
        });

        // チュートリアルテキスト表示
        if (Stage.currentStageNo == 1 && Stage.isChapterBegin) gameUGUI.SetTMP(true);
        else gameUGUI.SetTMP(false);
    }

    IEnumerator NormalGameStart()
    {
        if (Stage.isChapterBegin)
        {
            playerProvider.NormalGameBegin();

            yield return new WaitForSeconds(2f);

            stageProvider.NormalGameBegin();

            yield return new WaitWhile(() => stageProvider.IsNormalBegin());

            mainCamera.MoveLocalCamera(new Vector3(2, 3, -10), 1f);
            mainCamera.RotateLocalCamera(new Vector3(-5, 0, 0), 1f);

            yield return new WaitForSeconds(1f);
        }
        else
        {
            stageProvider.GameStart();

            yield return new WaitForSeconds(1f);
        }

        canvasManager.SetGame();
        playerProvider.NormalGameStart();
        Sound.PlaySe("Fire", 2, 0.2f, true);

        if (Stage.currentStageNo == 1 && Stage.isChapterBegin)
        {
            yield return new WaitForSeconds(3f);

            gameUGUI.SetTMP(false);
        }
    }

    IEnumerator BossGameStart()
    {
        playerProvider.BossGameBegin();
        stageProvider.BossGameBegin();

        yield return new WaitForSeconds(2f);

        mainCamera.MoveLocalCamera(new Vector3(2, 3, -10), 1f);
        mainCamera.RotateLocalCamera(new Vector3(-5, 0, 0), 1f);

        yield return new WaitForSeconds(2f);

        canvasManager.SetGame();
        playerProvider.BossGameStart();
        Sound.PlaySe("Fire", 2, 0.2f, true);
    }

    IEnumerator GameClear()
    {
        Sound.StopSe(true);

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
        Sound.StopSe(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Home()
    {
        Sound.StopSe(true);
        SceneManager.LoadScene("Start");
    }
}
