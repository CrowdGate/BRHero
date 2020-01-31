using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    private static Skin myInstance;

    public static int currentSkinNo { get; private set; }

    static SkinData skinData;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeBeforeSceneLoad()
    {
        var skin = new GameObject("Skin", typeof(Stage));
        myInstance = skin.GetComponent<Skin>();
        DontDestroyOnLoad(skin);

        skinData = Resources.Load<SkinData>(SkinData.classPass);
        currentSkinNo = PlayerPrefs.GetInt("CurrentSkinNo", 1);
    }

    public static Skin Instance
    {
        get
        {
            return myInstance;
        }
        set { }
    }

    // 使用するスキンNoを設定
    public static void SetCurrentSkinNo(int skinNo)
    {
        currentSkinNo = skinNo;
        PlayerPrefs.SetInt("CurrentSkinNo", skinNo);
    }
    public static SkinState GetSkinState(int skinNo)
    {
        return skinData.GetSkin(skinNo);
    }
}
