using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Boss : MonoBehaviour
{
    // BOSS制御クラス

    [SerializeField] List<Fireball> fireballList = new List<Fireball>();
    [SerializeField] List<Vector3> localMovePosList = new List<Vector3>();
    [SerializeField] FlickerModel model;
    [SerializeField] BigFireball bigFireball;
    [SerializeField] ParticleSystem hitWaterEffect;
    [SerializeField] SkinnedMeshRenderer myMesh;
    [SerializeField] ParticleSystem deadEffect;

    [SerializeField] public int myLife = 5;

    Ragdoll ragdoll;
    Collider collider;
    Animator animator;

    bool isMove = false;
    int index = 0;
    int indexMax = 0;

    public event Action OnGameOver;
    public event Action OnGameClear;
    public event Action OnHitWater;

    private void Awake()
    {
        ragdoll = GetComponent<Ragdoll>();
        collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

        indexMax = fireballList.Count - 1;

        collider.enabled = false;

        fireballList.ForEach(fire => {
            fire.OnCharacterHit += () => {
                OnGameOver?.Invoke();
            };

            fire.OnWaterHit += () => {
                hitWaterEffect.transform.position = fire.transform.position;
                hitWaterEffect.Play();
                fire.Stop(transform.localPosition);
            };
        });
    }

    // ゲーム開始演出
    public IEnumerator BeginRoutine()
    {
        transform.DOMoveZ(13, 2f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        animator.Play("Run_Static");

        yield return new WaitForSeconds(2f);

        var anime = DOTween.Sequence();
        anime.Append(transform.DOMoveY(5f, 2f));
        anime.Join(transform.DOLocalRotate(new Vector3(0, 180, 0), 0.5f));
        animator.Play("Falling");

        yield return new WaitWhile(() => anime.IsPlaying());

        StartCoroutine(BossRoutine());
    }

    IEnumerator BossRoutine()
    {
        isMove = true;

        StartCoroutine(bigFireball.PlayRoutine());
        StartCoroutine(Phase1());

        while (true)
        {
            // 自身のライフが0になったらゲームクリア
            if (myLife == 0)
            {
                StartCoroutine(GameClear());
                yield break;
            }

            yield return null;
        }
    }

    // プレイヤーを狙うフェーズ
    IEnumerator Phase1()
    {
        collider.enabled = true;

        while (true)
        {
            if (!isMove)
            {
                yield break;
            }
            else
            {
                //fireballList[index].Play(new Vector3(0,1,0));

                transform.DOLocalMove(localMovePosList[index], 1f);

                yield return new WaitForSeconds(2f);

                if (!isMove) yield break;

                if (index < indexMax) index++;
                else index = 0;
            }

            yield return null;
        }
    }

    // 敗北演出
    public void GameOver()
    {
        isMove = false;
        collider.enabled = false;
        bigFireball.GameEnd();

        fireballList.ForEach(fire => {
            fire.Stop(transform.localPosition);
        });
    }


    // 勝利演出
    IEnumerator GameClear()
    {
        isMove = false;

        fireballList.ForEach(fire => {
            fire.Stop(transform.localPosition);
        });

        collider.enabled = false;
        bigFireball.GameEnd();

        ragdoll.SetKinematic(false);
        animator.enabled = false;

        yield return new WaitForSeconds(2f);

        myMesh.enabled = false;
        deadEffect.transform.position = new Vector3(deadEffect.transform.position.x, 1, deadEffect.transform.position.z);
        deadEffect.Play();
        OnGameClear?.Invoke();
    }

    IEnumerator HitRoutine()
    {
        myLife--;
        collider.enabled = false;

        hitWaterEffect.transform.position = transform.position;
        hitWaterEffect.Play();
        OnHitWater?.Invoke();

        if (myLife > 0)
        {
            model.SetFlicker();

            yield return new WaitForSeconds(1.5f);

            collider.enabled = true;

        }
        else
        {
            model.SetFlicker();

            yield return new WaitForSeconds(1.5f);

            yield break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 水オブジェクトと衝突したらヒットを返す
        if (other.tag == "Water")
        {
            StartCoroutine(HitRoutine());
        }
    }
}
