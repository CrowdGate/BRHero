using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUGUI : MonoBehaviour
{
    [SerializeField] BossLife bossLife;
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] CheckPointIcon checkPointIcon;

    public void SetBossLife(int life)
    {
        bossLife.SetState(life);
    }
    public void Damage(int damageNum)
    {
        bossLife.Damage(damageNum);
    }
    public void SetTMP(bool onOff)
    {
        tutorialText.enabled = onOff;
    }
    public void SetCheckPointIcon()
    {
        checkPointIcon.SetIcons();
    }
}
