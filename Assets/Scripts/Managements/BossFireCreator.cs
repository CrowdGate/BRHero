using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossFireCreator : MonoBehaviour
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] List<BossFire> effectList = new List<BossFire>();

    public event Action OnGameOver;
    public event Action<float> OnFireScaleUp;

    private void Start()
    {
        effectList.ForEach(fire => {
            fire.OnHit += () => {
                hitEffect.transform.localPosition = fire.transform.localPosition;
                hitEffect.Play();
            };
            fire.OnDead += () => {
                hitEffect.transform.localPosition = fire.transform.localPosition;
                hitEffect.Play();
            };
            fire.OnGameOver += () => {
                OnGameOver?.Invoke();
                effectList.ForEach(effect => {
                    effect.Stop();
                });
            };
            fire.OnFireScaleUp += (alpha) => {
                OnFireScaleUp?.Invoke(alpha);
            };
        });
    }

    public void GameClear()
    {
        effectList.ForEach(fire => {
            fire.Stop();
        });
    }
}
