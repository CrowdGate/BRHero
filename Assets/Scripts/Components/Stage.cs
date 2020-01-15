using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // ステージクラス
    StageData stageData;

    // ステージNo上限値
    public int stageNoMax { get; private set; }

    void Start()
    {
        stageData = Resources.Load<StageData>(StageData.classPass);
        stageNoMax = stageData.GetStageNoMax();
    }

    // 対象のステージの達成率を返す(0:失敗 1:★ 1:★★ 3:★★★)
    public int GetStageClear(int stageNo, int rate)
    {
        if (stageData.GetStageNormaMax(stageNo) <= rate) return 3;
        else if (stageData.GetStageNormaMid(stageNo) <= rate) return 2;
        else if (stageData.GetStageNormaMin(stageNo) <= rate) return 1;
        else return 0;
    }
}
