using System.Linq;
using UnityEngine;

public static class MapLoader
{
    public static MapState LoadPreset(Transform t)
    {
        var mapState = new MapState();
        var nodes = t.GetComponentsInChildren<MapNodePreset>().ToList();
        foreach(var n in nodes)
        {
            var index = nodes.IndexOf(n);
            mapState.nodes.Add(new MapNodeState
            {
                id = index,
                x = n.transform.position.x,
                y = n.transform.position.y,
                z = n.transform.position.z
            });
        
            foreach(var j in n.join)
                mapState.joints.Add(new MapJoint
                {
                    from = index,
                    to = nodes.IndexOf(j),
                });
        
        }
        return mapState;
    }
}