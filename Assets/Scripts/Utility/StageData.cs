using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObject/StageData")]
public class StageData : ScriptableObject
{
    [SerializeField] List<StageState> StageList;
    public const string classPass = "Data/StageData";

    public enum STAGE_TYPE
    {
        NORMAL,     // 通常
        BOSS,       // ボス
    };

    public StageState GetStage(int stageNo)
    {
        var stageData = StageList.Find(p => p.stageNo == stageNo);

        var res = new StageState
        {
            stageNo = stageData.stageNo,
            chapterNo = stageData.chapterNo,
            type = stageData.type,
            startPos = stageData.startPos,
            endPos = stageData.endPos,
            checkPointList = stageData.checkPointList,
        };

        return res;
    }
    public int GetStageNoMax()
    {
        return StageList.Count;
    }
    public STAGE_TYPE GetStageType(int stageNo)
    {
        return StageList.Find(p => p.stageNo == stageNo).type;
    }
    public Vector3 GetStartPos(int stageNo)
    {
        return StageList.Find(p => p.stageNo == stageNo).startPos;
    }
    public Vector3 GetEndPos(int stageNo)
    {
        return StageList.Find(p => p.stageNo == stageNo).endPos;
    }
    public List<Vector3> GetCheckPointList(int stageNo)
    {
        return StageList.Find(p => p.stageNo == stageNo).checkPointList;
    }
}

[System.Serializable]
public class StageState
{
    public int stageNo = 1;                                         // ステージ番号
    public int chapterNo = 1;                                       // チャプター番号
    public StageData.STAGE_TYPE type = StageData.STAGE_TYPE.NORMAL; // ステージタイプ
    public Vector3 startPos = new Vector3();                        // キャラクター開始地点
    public Vector3 endPos = new Vector3();                          // キャラクター終了地点
    public List<Vector3> checkPointList = new List<Vector3>();      // チェックポイント設置地点リスト
}