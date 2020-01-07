using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTransform : MonoBehaviour
{

    public Mesh _mesh;
    public MeshFilter _skinnedMeshRenderer;
    private List<GameObject> _points = new List<GameObject>();

    private void Start()
    {

        _skinnedMeshRenderer = GetComponent<MeshFilter>();

        //頂点の設定
        _mesh = _skinnedMeshRenderer.mesh;

        foreach (var pos in _mesh.vertices)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.localScale = Vector3.one * 0.01f;
            obj.name = "point";
            obj.AddComponent<BoxCollider>();
            obj.GetComponent<MeshRenderer>().enabled = false;
            obj.tag = "MeshPoint";
            obj.transform.SetParent(transform);
            obj.transform.localPosition = pos;
            _points.Add(obj);
        }

    }

    void Update()
    {
        int len = _skinnedMeshRenderer.sharedMesh.vertices.Length;
        Vector3[] vec3 = new Vector3[len];
        for (int i = 0; i < len; i++)
        {
            vec3[i] = _points[i].transform.localPosition;
        }

        _mesh.vertices = vec3;

        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        _skinnedMeshRenderer.sharedMesh = _mesh;
    }
}