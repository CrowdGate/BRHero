using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTransform : MonoBehaviour
{

    private Mesh _tmpMesh;                                      // ゲーム中に利用するメッシュのキャッシュデータ
    private MeshFilter _skinnedMeshRenderer;                    // メッシュ情報を取得するためのComponent参照先。SkinnedMeshRendererかMesh Filiterのどちらか
    private List<GameObject> _points = new List<GameObject>();  // シーン上に生成されるCube（＝メッシュの各頂点の位置）を管理するリスト

    private void Start()
    {

        // SkinnedMeshRenderer を取得
        // MeshRendererとMeshFilterの場合は、MeshFilterから取得
        _skinnedMeshRenderer = GetComponent<MeshFilter>();

        // メッシュ情報を取得
        var _originalMesh = _skinnedMeshRenderer.sharedMesh;

        // ゲーム上で変形させるためのメッシュデータを複製して利用
        _tmpMesh = Instantiate(_originalMesh);

        // 取得したメッシュ情報を利用してCubeを生成
        foreach (var pos in _tmpMesh.vertices)
        {
            // PrimitiveなCubeを生成
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            // Cubeの位置とメッシュの頂点の位置を揃える
            obj.transform.localPosition = pos;

            // Cubeのサイズを小さく
            obj.transform.localScale = Vector3.one * 0.1f;

            // Cubeに名前とタグを設定
            obj.name = "point";
            obj.tag = "MeshPoint";

            // Cubeのコライダーのサイズを再計算
            obj.GetComponent<BoxCollider>().size = obj.transform.localScale;

            // Cubeの描画をオフ
            obj.GetComponent<MeshRenderer>().enabled = false;

            // Cubeをモデルの子オブジェクトに登録
            obj.transform.SetParent(transform);

            // 頂点管理用のリストに追加
            _points.Add(obj);
        }

    }

    void Update()
    {
        // メッシュの頂点数を取得
        int len = _skinnedMeshRenderer.sharedMesh.vertices.Length;

        // 頂点数分の配列を生成
        Vector3[] vec3 = new Vector3[len];

        // for文で頂点の数だけループ
        // メッシュの各頂点の位置をシーン上のCubeの位置から取得
        for (int i = 0; i < len; i++)
        {
            vec3[i] = _points[i].transform.localPosition;
        }

        // 同期させた頂点情報をメッシュの頂点データに反映
        _tmpMesh.vertices = vec3;

        // 再計算
        _tmpMesh.RecalculateNormals();
        _tmpMesh.RecalculateBounds();

        // オブジェクトのメッシュに再計算したものを反映
        _skinnedMeshRenderer.sharedMesh = _tmpMesh;
    }
}