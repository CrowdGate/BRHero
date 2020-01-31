using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class GoalPoint : MonoBehaviour
{
    // ゴールポイントクラス

    float startPosX = 0;
    public float endPosX { get; private set; } = 0;
    public bool isBegin { get; private set; } = false;

    [SerializeField] Transform throwPos;
    [SerializeField] Fireball fireball;

    public event Action OnGoal;
    public event Action OnGameStart;

    Collider collider;
    Animator animator;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        animator.Play("Idle_SexyDance");

        if (Stage.isCheckPoint) startPosX = Stage.stageState.endPos.x;
        else startPosX = Stage.stageState.startPos.x + 10f;
        transform.position = new Vector3(startPosX, transform.position.y, transform.position.z);

        endPosX = Stage.stageState.endPos.x;

        fireball.OnFireHit += () => {
            fireball.Stop(transform.localPosition);
        };
    }

    // キャラがゴールしたか
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            OnGoal?.Invoke();
            collider.enabled = false;
        }
    }

    // ゲーム開始演出
    public IEnumerator BeginRoutine()
    {
        isBegin = true;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        animator.Play("GrenadeThrow");

        yield return new WaitForSeconds(1.5f);

        fireball.Play(throwPos.position, 1f);

        yield return new WaitForSeconds(1f);

        animator.Play("Run_Static");

        var anime2 = DOTween.Sequence();
        anime2.Append(transform.DOLocalRotate(new Vector3(0, 90, 0), 0.3f));
        anime2.Join(transform.DOMoveX(endPosX, 2f));

        yield return new WaitWhile(() => anime2.IsPlaying());

        animator.Play("Idle_SexyDance");
        isBegin = false;
        OnGameStart?.Invoke();
        transform.DOLocalRotate(new Vector3(0, -90, 0), 0.3f);
    }
    public void GameStart()
    {
        OnGameStart?.Invoke();
    }
    public void PlayResult()
    {
        animator.Play("Run_Static");
        transform.DOLocalRotate(new Vector3(0, 90, 0), 0.3f);
        transform.DOMoveX(200, 5f);
    }
    public void GameOver()
    {
        collider.enabled = false;
    }
}
