using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class WaterCollider : MonoBehaviour
{
    // 水弾判定クラス

    [SerializeField] ParticleSystem waterBeam;

    Collider collider;

    public bool isUse { get; private set; } = false;

    public event Action<Vector3> OnHitWater;

    Vector3 defaultPos = new Vector3();

    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
        collider.enabled = true;

        defaultPos = transform.localPosition;
    }

    public void Shoot(Vector3 startPos, Vector3 targetPos)
    {
        isUse = true;
        collider.enabled = true;
        transform.localPosition = startPos;
        waterBeam.Play();
        transform.DOMove(targetPos, 0.3f).SetEase(Ease.Linear).OnComplete(() => {
            ShootEnd();
        });
    }

    public void ShootEnd()
    {
        waterBeam.Stop();
        isUse = false;
        transform.localPosition = defaultPos;
        collider.enabled = false;
    }

    public void GameEnd()
    {
        transform.localPosition = defaultPos;
        collider.enabled = false;
        waterBeam.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fire" || other.tag == "Boss")
        {
            Sound.PlaySe("WaterHit", 1);
            OnHitWater?.Invoke(other.transform.position);
        }
    }
}
