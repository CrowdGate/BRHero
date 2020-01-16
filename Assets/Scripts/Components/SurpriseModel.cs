using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SurpriseModel : MonoBehaviour
{
    // サプライズボックス景品クラス

    private void Start()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    // チェストが開けられたときの動作
    public void OpenChest()
    {
        gameObject.transform.DOScale(new Vector3(1, 1, 1), 1f);
    }
}
