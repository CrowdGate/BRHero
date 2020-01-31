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
    [SerializeField] CameraControll cameraControll;

    Animator animator;
    IKControl ikControl;
    PlayerMove playerMove;
    Ragdoll ragdoll;
    Collider collider;

    bool isMove = false;

    public event Action OnGameOver;

    event Action OnShotEnd;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        ikControl = GetComponent<IKControl>();
        playerMove = GetComponent<PlayerMove>();
        ragdoll = GetComponent<Ragdoll>();
        collider = GetComponent<Collider>();

        animator.Play("Idle");
        animator.SetLayerWeight(1, 0);
    }

    private void Start()
    {
        ikHandle.SetOnOff(false);

        if (Stage.stageState.type == StageData.STAGE_TYPE.NORMAL)
        {
            cameraControll.SetCameraLocalPos(new Vector3(10, 3, 0));
            cameraControll.SetCameraLocalRotate(new Vector3(0, -90, 0));
            cameraControll.SetOrthographicSize(20f);
            ikHandle.transform.localPosition = new Vector3(-10, 5, 0);
            ikHandle.distanceZ = 10f;
            ikHandle.transform.localRotation = Quaternion.Euler(0, 90, 0);
            ikHandle.SetView(false);
        }
        else if (Stage.stageState.type == StageData.STAGE_TYPE.BOSS)
        {
            cameraControll.transform.localPosition = new Vector3(0, 3, 0);
            cameraControll.transform.localRotation = Quaternion.Euler(0, 0, 0);
            cameraControll.SetOrthographicSize(20f);
            ikHandle.transform.localPosition = new Vector3(0, 5, 15);
            ikHandle.distanceZ = 15f;
            ikHandle.transform.localRotation = Quaternion.Euler(0, 0, 0);
            ikHandle.SetView(false);
        }

        // タップ判定
        TouchInput.Started += info =>
        {
            if (isMove)
            {
                //if (Stage.stageState.type == StageData.STAGE_TYPE.NORMAL)
                //{
                    Time.timeScale = 0.5f;
                    ikHandle.SetOnOff(true);
                    ikHandle.SetView(true);
                //}

                hose.SetView();
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
                //if (Stage.stageState.type == StageData.STAGE_TYPE.NORMAL)
                //{
                    ikHandle.SetOnOff(false);
                    ikHandle.SetView(false);
                //}

                StartCoroutine(hose.ShotRoutine(OnShotEnd));
            }
        };

        // ゲームオーバー発火時処理
        deadLine.OnGameOver += () => {
            OnGameOver?.Invoke();
        };

        OnShotEnd += () => {
            Time.timeScale = 1f;

            if (Stage.stageState.type == StageData.STAGE_TYPE.NORMAL) ikControl.OffActiveIK();
        };
    }

    public IEnumerator NormalGameBegin()
    {
        transform.DOMoveX(Stage.stageState.startPos.x - 2, 1f);
        animator.Play("Run_Static");

        yield return new WaitForSeconds(1f);

        animator.Play("Idle");

        yield return new WaitForSeconds(3f);

        transform.DOJump(transform.position, 1, 1, 0.5f);
    }
    public IEnumerator BossGameBegin()
    {
        transform.DOMoveZ(Stage.stageState.startPos.x, 2f);
        animator.Play("Run_Static");

        yield return new WaitForSeconds(2f);

        animator.Play("Idle");
        ikHandle.SetOnOff(true);
        ikControl.OnActiveIK(ikHandle.transform.position, ikHandle.transform.rotation);
    }
    public void NormalGameStart()
    {
        playerMove.SetMove(true);
        animator.Play("Run_Static");

        isMove = true;
    }
    public void BossGameStart()
    {
        isMove = true;
    }
    public void GameOver()
    {
        Time.timeScale = 1f;
        collider.enabled = false;
        animator.enabled = false;
        ragdoll.SetKinematic(false);

        ikHandle.SetOnOff(false);
        ikHandle.SetView(false);
        isMove = false;

        deadLine.GameEnd();
    }
    public void NormalGameClear()
    {
        collider.enabled = false;

        Time.timeScale = 1f;
        ikControl.OffActiveIK();
        animator.SetLayerWeight(1, 0);

        ikHandle.SetOnOff(false);
        ikHandle.SetView(false);
        isMove = false;

        deadLine.GameEnd();

        playerMove.SetMove(false);
        animator.Play("Idle");
    }
    public void BossGameClear()
    {
        collider.enabled = false;

        Time.timeScale = 1f;
        ikControl.OffActiveIK();
        animator.SetLayerWeight(1, 0);

        ikHandle.SetOnOff(false);
        ikHandle.SetView(false);
        isMove = false;

        deadLine.GameEnd();

        animator.Play("Idle");
    }

    public void NormalResult()
    {
        animator.Play("Run_Static");
        transform.DOMoveX(200, 5f);
    }
}
