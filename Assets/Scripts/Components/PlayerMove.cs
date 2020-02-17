using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] public float speed = 0.1f;
    [SerializeField] public float differencePosX = 0;
    public bool isMove { get; private set; } = false;

    private void Start()
    {
        Vector3 start = transform.position;

        if (Stage.stageState.type == StageData.STAGE_TYPE.NORMAL)
        {
            if (Stage.isChapterBegin) start.x = Stage.GetStartPos().x + differencePosX;
            else start.x = Stage.GetStartPos().x - 2f;
        }
        else if (Stage.stageState.type == StageData.STAGE_TYPE.BOSS)
        {
            start.x = Stage.stageState.startPos.x;
        }

        transform.position = start;
    }

    void FixedUpdate()
    {
        if (isMove)
        {
            gameObject.transform.position += Vector3.right * speed;
        }
    }

    public void SetMove(bool onOff)
    {
        isMove = onOff;
    }

    public void DoMove(Vector3 endPos, float time = 1f)
    {
        transform.DOMove(endPos, time).SetEase(Ease.Linear);
    }
}
