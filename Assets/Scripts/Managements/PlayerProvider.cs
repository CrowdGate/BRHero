using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Rendering.PostProcessing;

public class PlayerProvider : MonoBehaviour
{
    // 操作キャラ提供クラス

    [SerializeField] PostProcessVolume volume;
    [SerializeField] AnimationCurve curve;

    public Player player { get; private set; }

    PlayerMove playerMove;

    public event Action OnGameOver;

    private void Awake()
    {
        // プレイヤーの読み込み
        var prefab = Resources.Load<GameObject>("Player/Player_" + 1);
        var obj = Instantiate(prefab, transform.position, Quaternion.identity);
        obj.transform.parent = this.transform;
        player = obj.GetComponent<Player>();
        playerMove = GetComponent<PlayerMove>();
    }

    private void Start()
    {
        if (Stage.stageState.type == StageData.STAGE_TYPE.NORMAL)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Stage.stageState.type == StageData.STAGE_TYPE.BOSS)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        player.OnGameOver += () => {
            OnGameOver?.Invoke();
        };

        SetPostProcess(0);
    }

    public void SetPostProcess(float weight)
    {
        volume.weight = weight;
    }


    public void NormalGameBegin()
    {
        transform.DOMoveX(Stage.stageState.startPos.x, 1f).SetEase(Ease.Linear);
        StartCoroutine(player.NormalGameBegin());
    }
    public void BossGameBegin()
    {
        playerMove.DoMove(new Vector3(5,0,0), 2f);
        StartCoroutine(player.BossGameBegin());
    }
    public void NormalGameStart()
    {
        playerMove.SetMove(true);

        player.NormalGameStart();
    }
    public void BossGameStart()
    {
        player.BossGameStart();
    }
    public void NormalGameOver()
    {
        playerMove.SetMove(false);

        player.GameOver();
    }
    public void BossGameOver()
    {
        player.GameOver();
    }
    public void NormalGameClear()
    {
        playerMove.SetMove(false);

        player.NormalGameClear();
    }
    public void BossGameClear()
    {
        player.BossGameClear();
    }

    // ジャンプコルーチン
    public IEnumerator JumpRoutine(Vector3 endPos)
    {
        player.GetAnimator().Play("Jump");

        var anime = DOTween.Sequence();
        anime.Append(transform.DOJump(endPos, 4f, 1, 4f)).SetEase(curve).OnStart(() => {
            player.JumpRotate();
        }) ;
        
        yield return new WaitWhile(() => anime.IsPlaying());

        player.GetAnimator().Play("Run_Static");
    }
}
