using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckPointIcon : MonoBehaviour
{
    // チェックポイントアイコン管理クラス

    List<Image> icons = new List<Image>();

    int stageNo = 1;

    CanvasGroup group;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        stageNo = Stage.currentStageNo;

        if (Stage.stageState.type == StageData.STAGE_TYPE.NORMAL)
        {
            SetView(group, true);

            for (int i = 0; i < Stage.stageState.checkPointList.Count; i++)
            {
                var prefab = Resources.Load<GameObject>("Game/CheckPointIcon");
                var obj = Instantiate(prefab, transform.position, Quaternion.identity);
                obj.transform.parent = this.transform;
                obj.transform.localScale = Vector3.one;
                icons.Add(obj.GetComponent<Image>());
            }

            for (int i = 0; i < icons.Count; i++)
            {
                if (SaveData.GetBool("CheckPoint_" + stageNo + "_" + (i + 1), false))
                {
                    icons[i].color = Color.green;
                }
                else
                {
                    icons[i].color = Color.white;
                }
            }
        }
        else
        {
            SetView(group, false);
        }
    }

    public void SetIcons()
    {
        for (int i = 0; i < icons.Count; i++)
        {
            if (SaveData.GetBool("CheckPoint_" + stageNo + "_" + (i + 1), false))
            {
                icons[i].color = Color.green;
            }
            else
            {
                icons[i].color = Color.white;
            }
        }
    }

    void SetView(CanvasGroup group, bool OnOff)
    {
        if (group == null) return;
        group.alpha = OnOff ? 1f : 0f;
        group.blocksRaycasts = OnOff;
        group.interactable = OnOff;
    }
}
