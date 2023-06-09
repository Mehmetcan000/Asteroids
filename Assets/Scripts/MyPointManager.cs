using System.Collections.Generic;
using UnityEngine;

public class MyPointManager : MonoBehaviour
{
    [SerializeField] private List<MyPoint> _list;

    public Vector3 GetPosition()
    {
        var index = Random.Range(0, _list.Count);
        var current = _list[index];

        var nextIndex = Random.Range(0, current.listNext.Count);
        var next = current.listNext[nextIndex];

        return current.transform.position + (next.transform.position - current.transform.position) * Random.value;

    }
}