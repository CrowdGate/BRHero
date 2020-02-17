using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Fireball : MonoBehaviour
{
    // ファイアーボールエフェクトクラス

    [SerializeField] ParticleSystem effect;
    [SerializeField] ParticleSystem hitEffect;
    Collider collider;

    public event Action OnCharacterHit;
    public event Action OnWaterHit;
    public event Action OnFireHit;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }
    public void Play(Vector3 targetPos, float time = 3f)
    {
        effect.gameObject.SetActive(true);
        effect.Play();
        transform.DOMove(targetPos, time);
        collider.enabled = true;
    }
    public bool IsPlay()
    {
        return effect.isPlaying;
    }
    public void Stop(Vector3 defaultPos)
    {
        effect.Stop();
        effect.gameObject.SetActive(false);
        transform.DOPause();
        transform.position = defaultPos;
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // キャラオブジェクトと衝突したらヒットを返す
        if (other.tag == "Character")
        {
            hitEffect.Play();
            OnCharacterHit?.Invoke();
        }
        else if (other.tag == "Water")
        {
            OnWaterHit?.Invoke();
        }
        else if (other.tag == "Fire")
        {
            hitEffect.Play();
            OnFireHit?.Invoke();
        }
    }
}
