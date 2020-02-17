using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // ステージクラス

    private static Stage myInstance;

    // ステージデータ詳細
    static StageData stageData;
    public static StageState stageState;

    public static int currentStageNo { get; private set; }
    // ステージNo上限値
    public static int stageNoMax { get; private set; }

    public static bool isCheckPoint { get; private set; }
    public static bool isChapterBegin { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeBeforeSceneLoad()
    {
        var stage = new GameObject("Stage", typeof(Stage));
        myInstance = stage.GetComponent<Stage>();
        DontDestroyOnLoad(stage);

        currentStageNo = PlayerPrefs.GetInt("CurrentStageNo", 1);
        //currentStageNo = 4;
        stageData = Resources.Load<StageData>(StageData.classPass);
    }

    public static Stage Instance
    {
        get
        {
            return myInstance;
        }
        set { }
    }

    // 最新のクリアステージNoを設定
    public static void SetClearStageNo()
    {
        if (currentStageNo < stageNoMax)
        {
            currentStageNo++;
        }
        else
        {
            currentStageNo = 1;

            // 仮対応なので本実装では使用不可にする
            PlayerPrefs.DeleteAll();
        }

        PlayerPrefs.SetInt("CurrentStageNo", currentStageNo);
        stageState = stageData.GetStage(currentStageNo);
        stageNoMax = stageData.GetStageNoMax();
        isCheckPoint = false;
    }

    // ステージ情報の設定
    public static void SetStageFlgs()
    {
        // 現在のステージ情報を取得
        stageState = stageData.GetStage(currentStageNo);
        stageNoMax = stageData.GetStageNoMax();
        isCheckPoint = false;

        // ステージ頭のチャプターか
        if (stageState.chapterNo == 1) isChapterBegin = true;
        else isChapterBegin = false;

        // チェックポイントを通過済みか
        int checkNo = 1;
        stageState.checkPointList.ForEach(point => {
            if (SaveData.GetBool("CheckPoint_" + currentStageNo + "_" + checkNo, false))
            {
                isCheckPoint = true;
                isChapterBegin = false;
            }
            checkNo++;
        });
    }

    // ステージ開始位置を取得
    public static Vector3 GetStartPos()
    {
        Vector3 pos = stageState.startPos;
        int checkNo = 1;

        if (!isCheckPoint) return pos;

        stageState.checkPointList.ForEach(point => {
            if (SaveData.GetBool("CheckPoint_" + currentStageNo + "_" + checkNo, false))
            {
                // 手前の配置から上書きしていく
                pos = point;
                isCheckPoint = true;
            }
            checkNo++;
        });

        return pos;
    }
}
