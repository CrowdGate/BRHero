using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossFireCreator : MonoBehaviour
{
    [SerializeField] List<BossFire> effectList = new List<BossFire>();

    public event Action OnGameOver;
    public event Action OnDeadBossFire;
    public event Action<float> OnFireDanger;

    float dangerCount = 0;

    private void Start()
    {
        effectList.ForEach(fire => {
            fire.OnHit += () => {
            };
            fire.OnDead += () => {
                OnDeadBossFire?.Invoke();
            };
            fire.OnGameOver += () => {
                OnGameOver?.Invoke();
                effectList.ForEach(effect => {
                    effect.Stop();
                });
            };
        });
    }

    private void Update()
    {
        dangerCount = 0;

        effectList.ForEach(fire => {
            if (fire.isFire)
            {
                dangerCount += fire.timeCount;
            }
        });

        if (dangerCount > 12f)
        {
            OnFireDanger?.Invoke(1f);
        }
        else if (dangerCount > 6f)
        {
            OnFireDanger?.Invoke(0.8f);
        }
        else
        {
            OnFireDanger?.Invoke(0f);
        }
    }

    public BossFire GetBossFire()
    {
        BossFire bossFire = new BossFire();


        for (int index = 0; index < 10; index++)
        {
            int random = UnityEngine.Random.Range(0, effectList.Count);

            if (!effectList[random].isFire)
            {
                bossFire = effectList[random];
                break;
            }
        }

        return bossFire;
    }

    public void GameClear()
    {
        effectList.ForEach(fire => {
            fire.Stop();
        });
    }
}
