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
    bool isFire = false;
    float timeCount = 0;

    public event Action OnHit;
    public event Action OnDead;
    public event Action OnGameOver;
    public event Action<float> OnFireScaleUp;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (isFire)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= 2)
            {
                if (life >= 5)
                {
                    isFire = false;
                    timeCount = 0;
                    OnGameOver?.Invoke();
                }
                else
                {
                    timeCount = 0;
                    life++;
                    transform.DOScaleX(transform.localScale.x + 1, 1f);
                    transform.DOScaleY(transform.localScale.y + 1, 1f);
                    OnFireScaleUp?.Invoke(5 / life);
                }
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
                timeCount = 0;
                transform.DOScaleX(transform.localScale.x - 1, 1f);
                transform.DOScaleY(transform.localScale.y - 1, 1f);
                OnHit?.Invoke();
            }
        }
        else if (other.tag == "Fire" && life == 0)
        {
            Play();
        }
    }
}
