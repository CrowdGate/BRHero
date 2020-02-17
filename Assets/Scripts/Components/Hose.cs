using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hose : MonoBehaviour
{
    // 水鉄砲(ホース)クラス

    [SerializeField] List<WaterCollider> waterColliderList = new List<WaterCollider>();
    [SerializeField] ParticleSystem waterEffect;
    [SerializeField] ParticleSystem waterHit;


    private void Start()
    {
        waterColliderList.ForEach(water => {
            water.OnHitWater += (target) => {
                waterHit.transform.position = target;
                waterHit.Play();
            };
        });
    }

    public IEnumerator ShootRoutine(Vector3 pos, Action OnEnd)
    {
        WaterCollider useCollider = new WaterCollider();
        waterColliderList.ForEach(water => {
            if (!water.isUse)
            {
                useCollider = water;
            }
        });

        if (useCollider == null) yield break;

        Sound.PlaySe("WaterShoot", 0);
        useCollider.Shoot(waterEffect.transform.localPosition, pos);
        waterEffect.Play();

        yield return new WaitWhile(() => waterEffect.isPlaying);

        OnEnd?.Invoke();
    }

    public void GameEnd()
    {
        waterColliderList.ForEach(water => {
            water.GameEnd();
        });
    }
}
