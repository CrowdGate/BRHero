using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkinManager : MonoBehaviour
{
    // スキン画面管理クラス

    [SerializeField, HeaderAttribute("スキン画面ボタンリスト")]
    List<GameButton> gameButtons = new List<GameButton>();

    List<SkinIcon> skinIconList = new List<SkinIcon>();

    [SerializeField] GameObject pointContent;
    [SerializeField] GameObject surpriseContent;

    [SerializeField] CanvasGroup pointCanvas;
    [SerializeField] CanvasGroup surpriseCanvas;

    public event Action OnHome;
    public event Action<int> OnSelect;

    void Start()
    {
        var prefab = Resources.Load<GameObject>("Start/Skin_Icon");

        for (int index = 1; index <= Stage.stageNoMax; index++)
        {
            GameObject obj = (GameObject)Instantiate(prefab, new Vector3(), Quaternion.identity);
            obj.transform.SetParent(pointContent.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            SkinIcon icon = obj.GetComponent<SkinIcon>();
            icon.name = index.ToString();
            icon.SetState();
            icon.OnSelect += (skinNo) => OnSelect?.Invoke(skinNo);
            skinIconList.Add(icon);
        }

        // 各種ボタンの設定
        foreach (var button in gameButtons)
        {
            if (button.GetButtonType() == GameButton.Type.Skin_Home)
            {
                button.OnMyType += (type) => OnHome?.Invoke();
            }
            else if (button.GetButtonType() == GameButton.Type.Skin_Craim)
            {
            }
            else if (button.GetButtonType() == GameButton.Type.Skin_TypePoint)
            {
                button.OnMyType += (type) => ChangeSkinPoint();
            }
            else if (button.GetButtonType() == GameButton.Type.Skin_TypeSurprise)
            {
                button.OnMyType += (type) => ChangeSkinSurprise();
            }
        }

        SetView(pointCanvas, true);
        SetView(surpriseCanvas, false);
    }

    void ChangeSkinPoint()
    {
        SetView(pointCanvas, true);
        SetView(surpriseCanvas, false);
    }
    void ChangeSkinSurprise()
    {
        SetView(pointCanvas, false);
        SetView(surpriseCanvas, true);
    }

    void SetView(CanvasGroup group, bool OnOff)
    {
        group.alpha = OnOff ? 1f : 0f;
        group.blocksRaycasts = OnOff;
        group.interactable = OnOff;
    }
}
