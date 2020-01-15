using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionProvider : MonoBehaviour
{
    // リアクションキャラ提供クラス

    Reaction reaction { get; set; }

    [SerializeField] ReactionCamera reactionCamera;

    private int stageNo = 1;
    public bool isPlaying { get; private set; } = false;

    StageData stageData;

    private void Start()
    {
        stageNo = PlayerPrefs.GetInt("CurrentStageNo", 1);
        stageData = Resources.Load<StageData>(StageData.classPass);

        // ノルマの読み込み
        var prefab = Resources.Load<GameObject>("Reaction/Reaction_" + stageNo);
        var obj = Instantiate(prefab, transform.position, Quaternion.identity);

        obj.transform.parent = this.transform;
        Reaction reactionObj = obj.GetComponent<Reaction>();
        reaction = reactionObj;

        // リアクションカメラ移動アクション
        reaction.OnMoveCamera += () => {
            reactionCamera.MoveCamera(stageData.GetReactionCameraPos(stageNo), 1f);
        };
    }

    public IEnumerator ReactionRoutine(int stageNo)
    {
        // アニメーション再生開始
        isPlaying = true;
        reaction.PlayAnimation(stageNo);
        AnimatorStateInfo stateInfo = reaction.animator.GetCurrentAnimatorStateInfo(0);

        yield return null;

        yield return new WaitWhile(() => stateInfo.normalizedTime < 1f);

        isPlaying = false;
    }
}
