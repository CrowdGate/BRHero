using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkinIcon : MonoBehaviour
{
    // スキンアイコンクラス
    [SerializeField] Image lockIcon;

    GameButton btn;
    public event Action<int> OnSelect;

    int skinNo = 0;

    private void Awake()
    {
        btn = GetComponent<GameButton>();

        if (btn.GetButtonType() == GameButton.Type.Skin_Icon)
        {
            btn.OnMyType += (type) => OnSelect?.Invoke(skinNo);
        }
    }

    public void SetState()
    {
        skinNo = int.Parse(gameObject.name);

        //if (Stage.IsClearStageNo(skinNo))
        //{
        //    btn.SetInteractable(true);
        //    lockIcon.enabled = false;
        //}
        //else
        //{
        //    btn.SetInteractable(false);
        //    lockIcon.enabled = true;
        //}
    }
}
