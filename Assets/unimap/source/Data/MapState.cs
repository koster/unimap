using System.Collections.Generic;

public class MapState
{
    public List<MapNodeState> nodes = new();
    public List<MapJoint> joints = new();
}

public class MapNodeState
{
    public int id;

    public float x;
    public float y;
    public float z;

    public int nodeType;
    public int distance;

    public bool IsCombat()
    {
        return (MapNodeType)nodeType == MapNodeType.FIGHT || 
               (MapNodeType)nodeType == MapNodeType.ELITE;
    }
    
    public bool IsElite()
    {
        return (MapNodeType)nodeType == MapNodeType.ELITE;
    }
}

public class MapJoint
{
    public int from;
    public int to;
}