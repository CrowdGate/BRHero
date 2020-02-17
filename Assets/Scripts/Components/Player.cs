using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Player : MonoBehaviour
{
    // 操作キャラクラス

    [SerializeField] Hose hose;
    [SerializeField] DeadLine deadLine;
    [SerializeField] IKHandle ikHandle;
    [SerializeField] Animator animator;
    [SerializeField] Ragdoll ragdoll;
    [SerializeField] IKControl ikControl;

    Collider collider;

    bool isMove = false;

    public event Action OnGameOver;

    event Action OnShootEnd;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        animator.Play("Idle");
        animator.SetLayerWeight(1, 0);

        ikHandle.SetOnOff(false);

        // タップ判定
        TouchInput.Started += info =>
        {
            if (isMove)
            {
                ikHandle.SetView(true);

                ikControl.OnActiveIK(ikHandle.transform.position, ikHandle.transform.rotation);
            }
        };
        TouchInput.Moved += info =>
        {
            if (isMove)
            {
                ikControl.OnActiveIK(ikHandle.transform.position, ikHandle.transform.rotation);
            }
        };
        TouchInput.Ended += info =>
        {
            if (isMove)
            {
                ikHandle.SetView(false);
                animator.Play("Shoot", 1);

                StartCoroutine(hose.ShootRoutine(ikHandle.transform.position, OnShootEnd));

                //Vector3 pos = ikHandle.transform.position - transform.position;
                //Debug.DrawRay(transform.position, pos, Color.red, 10f);
            }
        };

        // ゲームオーバー発火時処理
        deadLine.OnGameOver += () => {
            OnGameOver?.Invoke();
        };

        // 撃ち終わったらIKを切る
        OnShootEnd += () => {
            if (Stage.stageState.type == StageData.STAGE_TYPE.NORMAL) ikControl.OffActiveIK();
        };
    }

    public IEnumerator NormalGameBegin()
    {
        animator.Play("Run_Static");

        yield return new WaitForSeconds(1f);

        animator.Play("Idle");
        yield return new WaitForSeconds(1f);

        transform.DOJump(transform.position, 1, 1, 0.5f).SetEase(Ease.Linear);
    }
    public IEnumerator BossGameBegin()
    {
        animator.Play("Run_Static");

        yield return new WaitForSeconds(2f);

        animator.Play("Idle");
    }
    public void NormalGameStart()
    {
        animator.Play("Run_Static");
        ikHandle.SetOnOff(true);
        isMove = true;
    }
    public void BossGameStart()
    {
        animator.transform.DOLocalRotate(new Vector3(0, 45, 0), 0.2f);
        ikHandle.SetOnOff(true);
        isMove = true;
    }
    public void GameOver()
    {
        collider.enabled = false;
        animator.enabled = false;
        ragdoll.SetKinematic(false);

        ikHandle.SetOnOff(false);
        ikHandle.SetView(false);
        isMove = false;

        deadLine.GameEnd();
        hose.GameEnd();
    }
    public void NormalGameClear()
    {
        collider.enabled = false;

        ikControl.OffActiveIK();
        animator.SetLayerWeight(1, 0);

        ikHandle.SetOnOff(false);
        ikHandle.SetView(false);
        isMove = false;

        deadLine.GameEnd();
        hose.GameEnd();

        animator.Play("Idle");
    }
    public void BossGameClear()
    {
        collider.enabled = false;

        ikControl.OffActiveIK();
        animator.SetLayerWeight(1, 0);

        ikHandle.SetOnOff(false);
        ikHandle.SetView(false);
        isMove = false;

        deadLine.GameEnd();
        hose.GameEnd();

        animator.Play("Jump");

        Vector3 pos = transform.position;
        pos.z = 5f;
        var anime = DOTween.Sequence();
        anime.Append(transform.DOJump(pos, 2f, 1, 1f).SetEase(Ease.Linear).OnComplete(() => {
            animator.Play("Dance");
        }));
        anime.Join(transform.DORotate(new Vector3(0, 90, 0), 0.2f)).SetEase(Ease.Linear);
    }

    public void NormalResult()
    {
        animator.Play("Run_Static");
        transform.DOMoveX(200, 5f).SetEase(Ease.Linear);
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void JumpRotate()
    {
        Quaternion def = animator.transform.localRotation;
        animator.transform.DOLocalRotate(new Vector3(360, 0, 0), 4f, RotateMode.FastBeyond360).SetRelative().SetEase(Ease.Linear).OnComplete(() => {
            animator.transform.localRotation = def;
        });
    }
    

    public void SetBossPhase2(Transform target)
    {
        ikHandle.SetOnOff(false);
        ikHandle.SetView(false);
        hose.GameEnd();
        Vector3 targetPos = target.position;
        targetPos.y += 1;
        ikHandle.transform.position = targetPos;
        ikControl.OnActiveIK(targetPos, target.rotation);

        animator.transform.DOLocalRotate(new Vector3(0, 90, 0), 0.2f);
    }
}
