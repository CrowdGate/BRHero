using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // Canvasの表示切替クラス

    [SerializeField] CanvasGroup game;
    [SerializeField] CanvasGroup complete;
    [SerializeField] CanvasGroup failed;
    //[SerializeField] CanvasGroup surprise;
    [SerializeField] CanvasGroup start;
    [SerializeField] CanvasGroup skin;
    //[SerializeField] CanvasGroup select;

    public void SetGame()
    {
        SetView(game, true);
        SetView(complete, false);
        SetView(failed, false);
        //SetView(surprise, false);
    }
    public void SetComplete()
    {
        SetView(game, false);
        SetView(complete, true);
        SetView(failed, false);
        //SetView(surprise, false);
    }
    public void SetFailed()
    {
        SetView(game, false);
        SetView(complete, false);
        SetView(failed, true);
        //SetView(surprise, false);
    }
    public void SetSurprise()
    {
        SetView(game, false);
        SetView(complete, false);
        SetView(failed, false);
        //SetView(surprise, true);
    }
    public void SetHideGame()
    {
        SetView(game, false);
        SetView(complete, false);
        SetView(failed, false);
        //SetView(surprise, false);
    }
    public void SetStart()
    {
        SetView(start, true);
        SetView(skin, false);
        //SetView(select, false);
    }
    public void SetSkin()
    {
        SetView(start, false);
        SetView(skin, true);
        //SetView(select, false);
    }
    //public void SetSelect()
    //{
    //    SetView(start, false);
    //    SetView(skin, false);
    //    SetView(select, true);
    //}

    void SetView(CanvasGroup group, bool OnOff)
    {
        if (group == null) return;
        group.alpha = OnOff ? 1f : 0f;
        group.blocksRaycasts = OnOff;
        group.interactable = OnOff;
    }
}
