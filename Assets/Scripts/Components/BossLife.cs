using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLife : MonoBehaviour
{
    // ボスライフ管理クラス

    [SerializeField] Slider slider;
    CanvasGroup group;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (Stage.stageState.type == StageData.STAGE_TYPE.BOSS) SetView(group, true);
        else SetView(group, false);
    }

    public void SetState(int maxHp)
    {
        slider.maxValue = maxHp;
        slider.value = maxHp;
    }

    public void Damage()
    {
        slider.value -= 1;
    }

    void SetView(CanvasGroup group, bool OnOff)
    {
        if (group == null) return;
        group.alpha = OnOff ? 1f : 0f;
        group.blocksRaycasts = OnOff;
        group.interactable = OnOff;
    }
}
