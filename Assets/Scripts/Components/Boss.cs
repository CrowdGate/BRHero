using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Boss : MonoBehaviour
{
    // BOSS制御クラス

    [SerializeField] Fireball fireball;
    [SerializeField] ParticleSystem fireEffect;
    [SerializeField] Ragdoll ragdoll;
    [SerializeField] Animator animator;

    [SerializeField] int fireLife = 5;
    [SerializeField] int myLife = 1;

    public int fireNum = 0;

    Collider collider;

    bool isMove = false;

    public event Action OnGameOver;
    public event Action OnGameClear;
    public event Action OnHitWater;
    public event Action OnShootFire;
    public event Action OnBeginPhase1;
    public event Action OnBeginPhase2;
    public event Action OnGetBoss;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        collider.enabled = false;
        fireNum = fireLife;
    }

    // ゲーム開始演出
    public IEnumerator BeginRoutine()
    {
        var anime = DOTween.Sequence();

        // 走って目的地まで向かう
        anime.Append(transform.DOMoveX(9, 2f).SetEase(Ease.Linear));
        transform.localRotation = Quaternion.Euler(0, 90, 0);
        animator.Play("Run_Static");

        yield return new WaitWhile(() => anime.IsPlaying());

        anime = DOTween.Sequence();

        // 振りむき待機モーションに切り替え
        anime.Append(transform.DOLocalRotate(new Vector3(0, -90, 0), 0.2f));
        animator.Play("Idle");

        yield return new WaitWhile(() => anime.IsPlaying());

        anime = DOTween.Sequence();

        // ジャンプを2回して怒りを表す
        anime.Append(transform.DOLocalJump(transform.position, 2f, 2, 1f).SetEase(Ease.Linear));

        yield return new WaitWhile(() => anime.IsPlaying());

        anime = DOTween.Sequence();

        // ビルのほうに振り向く

        anime.Append(transform.DOLocalRotate(new Vector3(0, -45, 0), 0.2f));

        yield return new WaitWhile(() => anime.IsPlaying());

        OnShootFire?.Invoke();

        isMove = true;
        StartCoroutine(Phase1());
    }

    // ビルを狙うフェーズ
    IEnumerator Phase1()
    {
        float time = 0;
        OnBeginPhase1?.Invoke();

        while (true)
        {
            if (!isMove) yield break;

            time += Time.deltaTime;

            if (time >= 2f && fireNum > 0)
            {
                OnShootFire?.Invoke();
                time = 0;
            }

            if (fireLife == 0)
            {
                StartCoroutine(Phase2());

                yield break;
            }

            yield return null;
        }
    }

    // プレイヤーを狙うフェーズ
    IEnumerator Phase2()
    {
        var anime = DOTween.Sequence();

        // プレイヤー側に振り向く
        anime.Append(transform.DOLocalRotate(new Vector3(0, -90, 0), 0.5f).SetEase(Ease.Linear));

        yield return new WaitWhile(() => anime.IsPlaying());

        // ジャンプを1回して怒りを表す
        anime.Append(transform.DOLocalJump(transform.position, 2f, 1, 0.5f).SetEase(Ease.Linear));

        yield return new WaitWhile(() => anime.IsPlaying());

        // 炎を纏い突進の構え
        animator.Play("Stun");
        fireball.Stop(transform.localPosition);
        fireEffect.Play();

        OnBeginPhase2?.Invoke();

        yield return new WaitForSeconds(2f);

        // プレイヤーに向かって突進開始
        animator.Play("Walk");
        transform.DOMoveX(5f, 3f).SetEase(Ease.Linear);

        collider.enabled = true;

        while (true)
        {
            if (!isMove) yield break;

            // 自身のライフが0になったらゲームクリア
            if (myLife == 0)
            {
                StartCoroutine(GameClear());
                yield break;
            }

            yield return null;
        }
    }


    // 敗北演出
    public void GameOver()
    {
        isMove = false;
        collider.enabled = false;
        fireball.Stop(transform.localPosition);
        animator.Play("Idle");
        transform.DOLocalRotate(new Vector3(0, -180, 0), 0.5f).SetEase(Ease.Linear);
    }


    // 勝利演出
    IEnumerator GameClear()
    {
        isMove = false;

        fireball.Stop(transform.localPosition);

        ragdoll.SetKinematic(false);
        transform.DOPause();
        animator.enabled = false;

        yield return new WaitForSeconds(2f);

        OnGameClear?.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        // 水オブジェクトと衝突したらヒットを返す
        if (other.tag == "Water")
        {
            myLife--;
            fireEffect.Stop();

            OnHitWater?.Invoke();
        }
        // プレイヤーと衝突したらゲームオーバー
        else if (other.tag == "Character")
        {
            OnGameOver?.Invoke();
        }
        else if (other.tag == "Police")
        {
            collider.enabled = false;
            OnGetBoss?.Invoke();
        }
    }

    // ボスの火炎発射
    public IEnumerator ShootFire(Vector3 targetPos)
    {
        fireball.Play(targetPos, 1f);
        animator.Play("Shoot");

        yield return new WaitForSeconds(1f);

        fireball.Stop(transform.position);
        animator.Play("Idle");
    }

    public void DamageFireLife(int damageNum)
    {
        fireLife -= damageNum;
    }
    public int GetFireLife()
    {
        return fireLife;
    }
}
