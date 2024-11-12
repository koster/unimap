using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapSkin
{
    public MapNodeView node;
    public MapJointView joint;
}

public class MapView : MonoBehaviour
{
    private MapSkin mySkin;
    private MapState myState;

    private List<MapJointView> joints = new List<MapJointView>();
    private List<MapNodeView> nodes = new List<MapNodeView>();

    public void Init(MapState state, MapSkin skin)
    {
        myState = state;

        foreach (var s in state.nodes)
        {
            var nodePf = Instantiate(skin.node, transform);
            nodePf.transform.position = new Vector3(s.x, s.y, s.z);
            nodePf.state = s;
            nodes.Add(nodePf);
        }

        foreach (var j in state.joints)
        {
            var jointPf = Instantiate(skin.joint, transform);
            jointPf.from = FindNodeById(j.from);
            jointPf.to = FindNodeById(j.to);
            jointPf.Refresh();
            joints.Add(jointPf);
        }
    }

    public void SetPassedPoint(int stationNode)
    {
        foreach (var n in myState.nodes)
            nodes[n.id].SetTraversible(
                n.id != stationNode && MapUtil.CanReach(myState, stationNode, n.id)
            );

        foreach (var j in joints)
        {
            if (j.from.state.id == stationNode)
            {
                // mark next nodes
                j.to.isNext = true;
            }

            j.SetReachable(j.from.isReachable || j.from.state.id == stationNode);
        }
    }

    private MapNodeView FindNodeById(int jTo)
    {
        foreach (var mnv in nodes)
            if (mnv.state.id == jTo)
                return mnv;
        return null;
    }

    public Transform GetNode(int mn)
    {
        return nodes[mn].transform;
    }
}