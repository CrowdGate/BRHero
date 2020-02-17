using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CheckPoint : MonoBehaviour
{
    // チェックポイントクラス

    [SerializeField] int checkNo = 1;
    [SerializeField] GameObject sign;
    [SerializeField] SpriteRenderer checkText;

    public bool isCheck { get; private set; } = false;

    public event Action OnHitCheckPoint;

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

        checkText.enabled = false;
    }

    // キャラがチェックポイントを通過したか
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character" && !isCheck)
        {
            Sound.PlaySe("CheckPoint", 3);
            isCheck = true;
            SaveData.SetBool("CheckPoint_" + Stage.currentStageNo + "_" + checkNo, true);
            OnHitCheckPoint?.Invoke();
            sign.transform.DOLocalRotate(Vector3.zero, 0.5f).SetEase(Ease.Linear);
            checkText.enabled = true;
            checkText.transform.DOLocalMoveY(1.5f, 1f).SetEase(Ease.Linear).OnComplete(() => {
                checkText.enabled = false;
            });
        }
    }
}
