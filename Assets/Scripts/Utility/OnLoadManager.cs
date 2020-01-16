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

        Debug.unityLogger.logEnabled = false;

        // PlayerPrefs設定
        if (PlayerPrefs.HasKey("GameMode")) PlayerPrefs.SetString("GameMode", "START");
    }

    public static OnLoadManager Instance
    {
        get
        {
            return myInstance;
        }
        set { }
    }
}
