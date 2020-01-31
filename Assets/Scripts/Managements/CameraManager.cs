using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // カメラ制御系クラス


    private void Start()
    {
        if (Stage.stageState.type == StageData.STAGE_TYPE.NORMAL)
        {
            transform.rotation = Quaternion.Euler(-5, 0, 0);
        }
    }
}
