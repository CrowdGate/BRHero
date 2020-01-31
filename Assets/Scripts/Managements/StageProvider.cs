using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageProvider : MonoBehaviour
{
    // ステージ提供クラス

    public StageManager stageManager { get; private set; }

    [SerializeField] ParticleSystem gameOverEffect;

    private void Awake()
    {
        // ステージの読み込み
        int stageNo = Stage.currentStageNo;
        var prefab = Resources.Load<GameObject>("Stage/Stage_" + stageNo);
        var obj = Instantiate(prefab, transform.position, Quaternion.identity);
        obj.transform.parent = this.transform;
        stageManager = obj.GetComponent<StageManager>();
    }

    public void NormalGameBegin()
    {
        StartCoroutine(stageManager.goalPoint.BeginRoutine());
    }
    public void BossGameBegin()
    {
        StartCoroutine(stageManager.boss.BeginRoutine());
    }

    public void GameStart()
    {
        stageManager.goalPoint.GameStart();
    }
    public bool IsNormalBegin()
    {
        return stageManager.goalPoint.isBegin;
    }
    public void NormalGameOver()
    {
        gameOverEffect.Play();
        stageManager.goalPoint.GameOver();
    }
    public void BossGameOver()
    {
        gameOverEffect.Play();
        stageManager.boss.GameOver();
    }
}
