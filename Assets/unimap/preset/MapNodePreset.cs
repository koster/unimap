using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum MapNodeType
{
    START = -1,
    FIGHT = 0,
    QUESTION = 1,
    ELITE = 2,
    LOOT = 3,
    BOSS = 4,
    CAMPFIRE = 5
}

[ExecuteAlways]
public class MapNodePreset : MonoBehaviour
{
    public int distance;
    public MapNodeType type;
    public List<MapNodePreset> join = new List<MapNodePreset>();

    float updateCooldown = 1f;
    float time = 0f;

    void Update()
    {
        time += Time.deltaTime;

        if (time > updateCooldown)
        {
            if (type == MapNodeType.START)
            {
                RefreshDistances(this, 0);
            }
        }
    }

    void RefreshDistances(MapNodePreset mp, int depth)
    {
        mp.distance = depth;
        foreach (var j in mp.join)
            RefreshDistances(j, depth + 1);
    }

    void OnDrawGizmos()
    {
        foreach (var l in join)
        {
            DrawDots(l);

            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, l.transform.position);
        }

#if UNITY_EDITOR
        Handles.Label(transform.position + Vector3.up * 0.35f, type + " " + distance);
#endif

        switch (type)
        {
            case MapNodeType.START:
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(transform.position, 0.25f);
                break;
            case MapNodeType.FIGHT:
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position, 0.25f);
                break;
            case MapNodeType.ELITE:
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(transform.position, 0.25f);
                break;
            case MapNodeType.LOOT:
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(transform.position, 0.25f);
                break;
            case MapNodeType.CAMPFIRE:
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(transform.position, 0.25f);
                break;
            case MapNodeType.BOSS:
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position, 0.35f);
                break;
            case MapNodeType.QUESTION:
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(transform.position, Vector3.one * 0.25f);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void DrawDots(MapNodePreset to)
    {
        var offset = 0.4f;
        var maxDistanceDelta = 0.3f;

        var delta = to.transform.position - transform.position;

        var psp = transform.position + delta.normalized * offset;
        var tgt = to.transform.position - delta.normalized * offset;
        var mag = Vector3.Distance(psp, tgt);

        for (var i = 0; i < mag / maxDistanceDelta; i++)
        {
            Gizmos.color = new Color(0f, 0f, 0f, 0.5f);
            Gizmos.DrawSphere(psp, 0.1f);
            psp = Vector3.MoveTowards(psp, tgt, maxDistanceDelta);
        }
    }
}