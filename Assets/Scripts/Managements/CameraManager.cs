using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // カメラ制御系クラス

    //[SerializeField] Camera main;
    [SerializeField] ReactionCamera main;
    [SerializeField] ReactionCamera reaction;
    [SerializeField] Camera rawImage;

    public void ViewMainCamera(CameraClearFlags flag = CameraClearFlags.SolidColor)
    {
        main.reactionCamera.depth = 1;
        reaction.reactionCamera.depth = 0;
        //main.reactionCamera.clearFlags = flag;
    }
    public void ViewReactionCamera()
    {
        main.reactionCamera.depth = 0;
        reaction.reactionCamera.depth = 1;

        reaction.MoveCamera(new Vector3(-5, 2, -3.5f), 1f);
        reaction.RotateCamera(new Vector3(-10, -60, 0), 1f);

        //main.MoveCamera(new Vector3(-5, 2, -3.5f), 1f);
        //main.RotateCamera(new Vector3(-10, -60, 0), 1f);
    }
    public void SurpriseCamera()
    {
        rawImage.transform.position = new Vector3(20, 0, -2);
    }
}
