using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hose : MonoBehaviour
{
    // 水鉄砲(ホース)クラス

    [SerializeField] MeshRenderer line;
    [SerializeField] Collider collier;
    [SerializeField] ParticleSystem waterEffect;

    private void Start()
    {
        line.enabled = false;
        collier.enabled = false;
    }

    public void SetView()
    {
        line.enabled = true;
        collier.enabled = false;
    }

    public IEnumerator ShotRoutine(Action OnEnd)
    {
        line.enabled = false;
        collier.enabled = true;
        waterEffect.Play();

        yield return new WaitWhile(() => waterEffect.isPlaying);

        waterEffect.Stop();
        collier.enabled = false;
        OnEnd?.Invoke();
    }
}
