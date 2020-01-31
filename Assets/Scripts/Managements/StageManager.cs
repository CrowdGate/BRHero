using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager : MonoBehaviour
{
    // ステージマネージャー(中継)クラス

    // ノーマルステージ関連
    [SerializeField, HeaderAttribute("ノーマルステージ関連")] public GoalPoint goalPoint;
    [SerializeField] List<CheckPoint> checkPointList = new List<CheckPoint>();
    [SerializeField] public List<FireCreator> fireCreatorList = new List<FireCreator>();


    // ボスステージ関連
    [SerializeField, HeaderAttribute("ボスステージ関連")] public Boss boss;
    [SerializeField] public List<BossFireCreator> bossFireCreatorList = new List<BossFireCreator>();

    private void Start()
    {
        if (goalPoint != null)
        {
            goalPoint.OnGameStart += () => {
                fireCreatorList.ForEach(creator => {
                    creator.GameStart();
                });
            };
        }
    }

    public void BossGameClear()
    {
        bossFireCreatorList.ForEach(creator => {
            creator.GameClear();
        });
    }
}
