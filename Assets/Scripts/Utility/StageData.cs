using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObject/StageData")]
public class StageData : ScriptableObject
{
    [SerializeField] List<StageState> StageList;
    public const string classPass = "Data/StageData";

    public StageState GetStage(int stageNo)
    {
        var stageData = StageList.Find(p => p.stageNo == stageNo);

        var res = new StageState
        {
            stageNo = stageData.stageNo,
            normaMin = stageData.normaMin,
            normaMid = stageData.normaMid,
            normaMax = stageData.normaMax,
        };

        return res;
    }
    public int GetStageNormaMin(int stageNo)
    {
        return StageList.Find(p => p.stageNo == stageNo).normaMin;
    }
    public int GetStageNormaMid(int stageNo)
    {
        return StageList.Find(p => p.stageNo == stageNo).normaMid;
    }
    public int GetStageNormaMax(int stageNo)
    {
        return StageList.Find(p => p.stageNo == stageNo).normaMax;
    }
}

[System.Serializable]
public class StageState
{
    public int stageNo;                         // ステージ番号
    [Range(0, 100)] public int normaMin = 50;   // 必要ノルマ最低値
    [Range(0, 100)] public int normaMid = 70;   // 必要ノルマ中間値
    [Range(0, 100)] public int normaMax = 90;   // 必要ノルマ最高値
}