using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeshMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public List<GameObject> myPointsList = new List<GameObject>();
    public List<Vector3> myPointsOffsetList = new List<Vector3>();

    bool isDrag;
    bool isDeactiveTrigger;

    private void Start()
    {
        isDrag = false;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDeactiveTrigger) { return; }

        if (other.gameObject.tag == "MeshPoint" && !myPointsList.Contains(other.gameObject))
        {
            myPointsList.Add(other.gameObject);
            myPointsOffsetList.Add(gameObject.transform.position - other.gameObject.transform.position);
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDeactiveTrigger = true;
        Debug.Log("Hoge");
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(eventData.position);
        TargetPos.z = gameObject.transform.position.z;
        gameObject.transform.position = TargetPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Fuga");
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(eventData.position);
        TargetPos.z = gameObject.transform.position.z;
        gameObject.transform.position = TargetPos;

        for (int i = 0; i < myPointsList.Count; i++)
        {
            myPointsList[i].transform.position = gameObject.transform.position - myPointsOffsetList[i];
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Piyo");
    }
}
