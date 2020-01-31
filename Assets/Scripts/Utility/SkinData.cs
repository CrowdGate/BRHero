using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObject/SkinData")]
public class SkinData : ScriptableObject
{
    [SerializeField] List<SkinState> SkinList;
    public const string classPass = "Data/SkinData";

    public enum SKIN_TYPE
    {
        POINT,      // ポイント交換系
        SURPRISE,   // サプライズボックス系
    };

    public SkinState GetSkin(int skinNo)
    {
        var skinData = SkinList.Find(p => p.skinNo == skinNo);

        var res = new SkinState
        {
            skinNo = skinData.skinNo,
            type = skinData.type,
            skinHead = skinData.skinHead,
            skinBody = skinData.skinBody,
        };

        return res;
    }
    public int GetStageNoMax()
    {
        return SkinList.Count;
    }
    public SKIN_TYPE GetSkinType(int skinNo)
    {
        return SkinList.Find(p => p.skinNo == skinNo).type;
    }
    public SkinnedMeshRenderer GetSkinHead(int skinNo)
    {
        return SkinList.Find(p => p.skinNo == skinNo).skinHead;
    }
    public SkinnedMeshRenderer GetSkinBody(int skinNo)
    {
        return SkinList.Find(p => p.skinNo == skinNo).skinBody;
    }
}

[System.Serializable]
public class SkinState
{
    public int skinNo;                      // スキン番号
    public SkinData.SKIN_TYPE type;         // スキンタイプ
    public SkinnedMeshRenderer skinHead;    // スキン頭
    public SkinnedMeshRenderer skinBody;    // スキン胴体
}