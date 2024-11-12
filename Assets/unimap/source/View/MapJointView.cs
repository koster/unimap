using System;
using UnityEngine;

public class MapJointView : MonoBehaviour
{
    public MapNodeView from;
    public MapNodeView to;

    public LineRenderer lr;

    void Awake()
    {
        lr.alignment = LineAlignment.TransformZ;
    }

    public void Refresh()
    {
        var direction = (to.transform.position - from.transform.position).normalized;
        var offset = 0.4f;

        lr.SetPosition(0, from.transform.position + direction * offset);
        lr.SetPosition(1, to.transform.position - direction * offset);
    }

    public void SetReachable(bool toIsReachable)
    {
        if (!toIsReachable)
        {
            var lrEndColor = lr.endColor;
            lrEndColor.a = 0.5f;
            lr.endColor = lrEndColor;
            lr.startColor = lrEndColor;
        }
    }
}