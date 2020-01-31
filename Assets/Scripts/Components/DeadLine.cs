using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeadLine : MonoBehaviour
{
    // 死亡判定ラインクラス

    Collider collider;

    public event Action OnGameOver;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 炎が一定ラインを越えたらゲームオーバーを返す
        if (other.tag == "Fire")
        {
            collider.enabled = false;
            OnGameOver?.Invoke();
        }
    }

    public void GameEnd()
    {
        collider.enabled = false;
    }
}
