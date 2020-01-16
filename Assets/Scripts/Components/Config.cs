using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    // コンフィグクラス

    bool viewConfig { get; set; } = false;
    // サウンド再生フラグ
    public bool isSound { get; private set; }

    Vector2 beginPos;

    [SerializeField] ImageMove configImage;
    [SerializeField] ImageOnOff imageOnOff;

    void Start()
    {
        isSound = SaveData.GetBool("soundOnOff", true);
        beginPos = configImage.transform.localPosition;
        imageOnOff.ChangeSetting(isSound);
    }

    public void ViewConfig()
    {
        if (viewConfig)
        {
            viewConfig = false;
            beginPos.x += 100;
            configImage.MoveImage(beginPos, 0.5f);
        }
        else
        {
            viewConfig = true;
            beginPos.x -= 100;
            configImage.MoveImage(beginPos, 0.5f);
        }
    }
    // サウンドのON/OFF設定
    public void SetSound()
    {
        if (isSound) isSound = false;
        else isSound = true;
        SaveData.SetBool("soundOnOff", isSound);
        imageOnOff.ChangeSetting(isSound);
    }
}
