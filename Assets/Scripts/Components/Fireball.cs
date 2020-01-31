using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Fireball : MonoBehaviour
{
    // ファイアーボールエフェクトクラス

    [SerializeField] ParticleSystem effect;
    MeshRenderer mesh;
    Collider collider;

    public event Action OnCharacterHit;
    public event Action OnWaterHit;
    public event Action OnFireHit;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        mesh.enabled = false;
        collider.enabled = false;
    }
    public void Play(Vector3 targetPos, float time = 3f)
    {
        effect.Play();
        transform.DOMove(targetPos, time);
        mesh.enabled = true;
        collider.enabled = true;
    }
    public bool IsPlay()
    {
        return effect.isPlaying;
    }
    public void Stop(Vector3 defaultPos)
    {
        effect.Stop();
        transform.DOPause();
        transform.position = defaultPos;
        mesh.enabled = false;
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // キャラオブジェクトと衝突したらヒットを返す
        if (other.tag == "Character")
        {
            OnCharacterHit?.Invoke();
        }
        else if (other.tag == "Water")
        {
            OnWaterHit?.Invoke();
        }
        else if (other.tag == "Fire")
        {
            OnFireHit?.Invoke();
        }
    }
}
