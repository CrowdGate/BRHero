using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (!Stage.isCheckPoint) start.x = Stage.GetStartPos().x + differencePosX;
            else start.x = Stage.GetStartPos().x - 2f;
        }
        else if (Stage.stageState.type == StageData.STAGE_TYPE.BOSS)
        {
            start.z = Stage.GetStartPos().z - 50f;
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
}
