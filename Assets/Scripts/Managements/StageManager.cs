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
    [SerializeField] public List<Gimmick> gimmickList = new List<Gimmick>();


    // ボスステージ関連
    [SerializeField, HeaderAttribute("ボスステージ関連")] public Boss boss;
    [SerializeField] public BossFireCreator bossFireCreator;
    [SerializeField] BossClearGimmick bossClearGimmick;

    public event Action OnCheckPoint;

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

        if (boss != null)
        {
            boss.OnShootFire += () => {
                BossFire bossFire = bossFireCreator.GetBossFire();
                if (bossFire != null)
                {
                    StartCoroutine(boss.ShootFire(bossFire.transform.position));
                    boss.fireNum--;
                }
            };

            boss.OnGetBoss += () => {
                boss.transform.parent = bossClearGimmick.GetHuman().transform;
            };
        }

        checkPointList.ForEach(check => {
            check.OnHitCheckPoint += () => {
                OnCheckPoint?.Invoke();
            };
        });

        if (bossClearGimmick != null)
        {
            bossClearGimmick.gameObject.SetActive(false);
        }
    }

    public void Phase2()
    {
        bossFireCreator.GameClear();
    }

    public void BossGameClear()
    {
        bossClearGimmick.gameObject.SetActive(true);
        StartCoroutine(bossClearGimmick.BossClearRoutine());
    }
}
