using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class BossFire : MonoBehaviour
{
    // 炎エフェクトクラス

    [SerializeField] ParticleSystem effect;
    Collider collider;

    int life = 0;

    // 燃えているか
    public bool isFire { get; private set; } = false;
    public float timeCount { get; private set; } = 0;

    public event Action OnHit;
    public event Action OnDead;
    public event Action OnGameOver;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (isFire)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= 10)
            {
                isFire = false;
                timeCount = 0;
                OnGameOver?.Invoke();
            }
        }
    }

    public void Play()
    {
        effect.Play();
        life = 1;
        timeCount = 0;
        isFire = true;
    }
    public bool IsPlay()
    {
        return effect.isPlaying;
    }
    public void Stop()
    {
        effect.Stop();
        life = 0;
        timeCount = 0;
        isFire = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 水オブジェクトと衝突したらヒットを返す
        if (other.tag == "Water" && life > 0)
        {
            life--;
            if (life == 0)
            {
                Stop();
                OnDead?.Invoke();
            }
            else
            {
                OnHit?.Invoke();
            }
        }
        else if (other.tag == "FireBall" && life == 0)
        {
            Play();
        }
    }
}
