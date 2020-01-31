using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinModel : MonoBehaviour
{
    // 人形スキン反映クラス

    [SerializeField] GameObject point;
    [SerializeField] SkinnedMeshRenderer pointHead;
    [SerializeField] SkinnedMeshRenderer pointBody;
    [SerializeField] GameObject surprise;
    [SerializeField] SkinnedMeshRenderer surpriseBody;

    private void Start()
    {
        //int skinNo = Skin.currentSkinNo;
        int skinNo = 5;
        SkinState state = Skin.GetSkinState(skinNo);

        if (state.type == SkinData.SKIN_TYPE.POINT)
        {
            point.SetActive(true);
            surprise.SetActive(false);

            pointHead.sharedMesh = state.skinHead.sharedMesh;
            Material[] headMaterials = pointHead.materials;
            for (int i = 0; i < headMaterials.Length; i++){
                if (i < state.skinHead.materials.Length) headMaterials[i] = state.skinHead.materials[i];
            }
            pointHead.materials = headMaterials;
            pointBody.sharedMesh = state.skinBody.sharedMesh;
            Material[] bodyMaterials = state.skinBody.materials;
            //pointBody.materials = bodyMaterials;
        }
        else if (state.type == SkinData.SKIN_TYPE.SURPRISE)
        {
            point.SetActive(false);
            surprise.SetActive(true);

            surpriseBody.sharedMesh = state.skinBody.sharedMesh;
            surpriseBody.materials = state.skinBody.materials;
        }
    }
}
