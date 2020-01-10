using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] CanvasGroup game;
    [SerializeField] CanvasGroup complete;
    [SerializeField] CanvasGroup failed;

    public void SetGame()
    {
        SetView(game, true);
        SetView(complete, false);
        SetView(failed, false);
    }
    public void SetComplete()
    {
        SetView(game, false);
        SetView(complete, true);
        SetView(failed, false);
    }
    public void SetFailed()
    {
        SetView(game, false);
        SetView(complete, false);
        SetView(failed, true);
    }

    void SetView(CanvasGroup group, bool OnOff)
    {
        group.alpha = OnOff ? 1f : 0f;
        group.blocksRaycasts = OnOff;
        group.interactable = OnOff;
    }
}
