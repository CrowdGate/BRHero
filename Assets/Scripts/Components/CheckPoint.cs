using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckPoint : MonoBehaviour
{
    // チェックポイントクラス

    [SerializeField] int checkNo = 1;
    [SerializeField] GameObject sign;

    public bool isCheck { get; private set; } = false;

    private void Start()
    {
        // 以前チェックポイントを通過しているか
        isCheck = SaveData.GetBool("CheckPoint_" + Stage.currentStageNo + "_" + checkNo, false);
        if (isCheck)
        {
            sign.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            sign.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }
    }

    // キャラがチェックポイントを通過したか
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character" && !isCheck)
        {
            isCheck = true;
            SaveData.SetBool("CheckPoint_" + Stage.currentStageNo + "_" + checkNo, true);
            sign.transform.DOLocalRotate(Vector3.zero, 0.5f);
        }
    }
}
