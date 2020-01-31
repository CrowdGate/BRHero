using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank : MonoBehaviour
{
    // ステージランク表示クラス

    //[SerializeField] int stageNo = 0;
    [SerializeField] List<ImageOnOff> imageList;

    int rank { get; set; } = 0;

    // リザルト時のランク設定
    public void ResultRank(int stageNo, int rankNum)
    {
        int num = 0;
        imageList.ForEach(image => {
            num++;
            if (num <= rankNum) image.ChangeSetting(true);
            else image.ChangeSetting(false);
        });

        // クリアランクを保存
        int clearRank = PlayerPrefs.GetInt("ClearRank_" + stageNo, 0);
        if (rankNum > clearRank) PlayerPrefs.SetInt("ClearRank_" + stageNo, rankNum);
    }

    // ランクの設定
    public void SetRank(int stageNo)
    {
        rank = PlayerPrefs.GetInt("ClearRank_" + stageNo, 0);

        int num = 0;
        imageList.ForEach(image => {
            num++;
            if (num <= rank) image.ChangeSetting(true);
            else image.ChangeSetting(false);
        });
    }
}
