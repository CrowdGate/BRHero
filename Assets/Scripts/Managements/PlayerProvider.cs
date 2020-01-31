using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerProvider : MonoBehaviour
{
    // 操作キャラ提供クラス

    public Player player { get; private set; }

    [SerializeField] List<PlayerMove> moveList = new List<PlayerMove>();

    public event Action OnGameOver;

    private void Awake()
    {
        // プレイヤーの読み込み
        var prefab = Resources.Load<GameObject>("Player/Player_" + 1);
        var obj = Instantiate(prefab, transform.position, Quaternion.identity);
        obj.transform.parent = this.transform;
        player = obj.GetComponent<Player>();
    }

    private void Start()
    {
        if (Stage.stageState.type == StageData.STAGE_TYPE.NORMAL)
        {
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (Stage.stageState.type == StageData.STAGE_TYPE.BOSS)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        player.OnGameOver += () => {
            OnGameOver?.Invoke();
        };
    }


    public void NormalGameBegin()
    {
        StartCoroutine(player.NormalGameBegin());
    }
    public void BossGameBegin()
    {
        StartCoroutine(player.BossGameBegin());
    }
    public void NormalGameStart()
    {
        moveList.ForEach(info =>
        {
            info.SetMove(true);
        });

        player.NormalGameStart();
    }
    public void BossGameStart()
    {
        player.BossGameStart();
    }
    public void NormalGameOver()
    {
        moveList.ForEach(info => {
            info.SetMove(false);
        });

        player.GameOver();
    }
    public void BossGameOver()
    {
        player.GameOver();
    }
    public void NormalGameClear()
    {
        moveList.ForEach(info => {
            info.SetMove(false);
        });

        player.NormalGameClear();
    }
    public void BossGameClear()
    {
        player.BossGameClear();
    }
}
