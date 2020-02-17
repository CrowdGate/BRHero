using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fire : MonoBehaviour
{
    // 炎エフェクトクラス

    [SerializeField] ParticleSystem effect;
    Collider collider;

    [SerializeField] int life = 1;

    public event Action OnHit;
    public event Action OnDead;
    public event Action OnFire;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
    public void Play()
    {
        effect.Play();
    }
    public bool IsPlay()
    {
        return effect.isPlaying;
    }
    public void Stop()
    {
        effect.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 放水と衝突したら
        if (other.tag == "Water")
        {
            life--;
            if (life == 0)
            {
                Stop();
                collider.enabled = false;
                OnDead?.Invoke();
            }
            else
            {
                OnHit?.Invoke();
            }
        }
        // 演出用炎と衝突したら
        else if (other.tag == "FireBall")
        {
            OnFire?.Invoke();
        }
    }
}
