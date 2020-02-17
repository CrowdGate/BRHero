using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadManager : MonoBehaviour
{
    private static OnLoadManager myInstance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeBeforeSceneLoad()
    {
        var dataManager = new GameObject("OnLoadManager", typeof(OnLoadManager));
        myInstance = dataManager.GetComponent<OnLoadManager>();
        DontDestroyOnLoad(dataManager);

        //Debug.unityLogger.logEnabled = false;

        // PlayerPrefs設定
        if (PlayerPrefs.HasKey("GameMode")) PlayerPrefs.SetString("GameMode", "START");

        Sound.LoadSe("Fire", "SE_Fire");
        Sound.LoadSe("WaterHit", "SE_WaterHit");
        Sound.LoadSe("WaterShoot", "SE_WaterShoot");
        Sound.LoadSe("CheckPoint", "SE_CheckPoint");

        SetResolution(1024); // 反映はフレーム更新後
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public static OnLoadManager Instance
    {
        get
        {
            return myInstance;
        }
        set { }
    }

    private static void SetResolution(float baseResolution)
    {
        float screenRate = baseResolution / Screen.height;
        if (screenRate > 1) screenRate = 1;
        int width = (int)(Screen.width * screenRate);
        int height = (int)(Screen.height * screenRate);

        Screen.SetResolution(width, height, true);
    }
}
