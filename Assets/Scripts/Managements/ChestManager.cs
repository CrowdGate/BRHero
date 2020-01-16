using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChestManager : MonoBehaviour
{
    [SerializeField, HeaderAttribute("サプライズボックスボタンリスト")]
    List<GameButton> gameButtons = new List<GameButton>();

    [SerializeField, HeaderAttribute("各種データ")]
    Animator chestAnimator;

    [SerializeField] CanvasGroup btn_Claim;
    [SerializeField] CanvasGroup btn_Next;
    [SerializeField] CanvasGroup btn_End;

    SurpriseModel model;

    private void Start()
    {
        // チェスト待機アニメーション再生
        chestAnimator.Play("ChestWait");

        // サプライズ報酬の読み込み
        var prefab = Resources.Load<GameObject>("Surprise/Surprise_1");
        var obj = Instantiate(prefab, transform.position, Quaternion.identity);

        obj.transform.parent = this.transform;
        SurpriseModel surprise = obj.GetComponent<SurpriseModel>();
        model = surprise;
        model.transform.position += new Vector3(0, -0.5f, -0.4f);

        // 各種ボタンの設定
        foreach (var button in gameButtons)
        {
            if (button.GetButtonType() == GameButton.Type.Chest_Open)
            {
                button.OnMyType += (type) => GetSurprise();
            }
            else if (button.GetButtonType() == GameButton.Type.Chest_Home)
            {
                button.OnMyType += (type) => End();
            }
        }

        SetView(btn_Claim, true);
        SetView(btn_Next, true);
        SetView(btn_End, false);
    }

    void GetSurprise()
    {
        // リワード動画閲覧

        StartCoroutine(OpenChestRoutine());
    }
    void End()
    {
        SceneManager.LoadScene("Game");
    }

    IEnumerator OpenChestRoutine()
    {
        SetView(btn_Claim, false);
        SetView(btn_Next, false);

        chestAnimator.Play("ChestOpen");

        yield return new WaitForSeconds(0.6f);

        model.OpenChest();

        yield return new WaitForSeconds(1f);

        SetView(btn_End, true);

    }

    void SetView(CanvasGroup group, bool OnOff)
    {
        group.alpha = OnOff ? 1f : 0f;
        group.blocksRaycasts = OnOff;
        group.interactable = OnOff;
    }
}
