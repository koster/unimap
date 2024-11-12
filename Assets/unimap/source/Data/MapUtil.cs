using System.Collections.Generic;

public static class MapUtil
{
    // Create a HashSet to store visited nodes to avoid cycles
    static HashSet<int> visited = new HashSet<int>();

    public static bool CanReach(MapState map, int nodeA, int nodeB)
    {
        visited.Clear();
        // Call a helper function that performs DFS
        return DFS(map, nodeA, nodeB, visited);
    }

    private static bool DFS(MapState map, int currentNode, int targetNode, HashSet<int> visited)
    {
        // If we have reached the target node, return true
        if (currentNode == targetNode)
        {
            return true;
        }

        // Mark the current node as visited
        visited.Add(currentNode);

        // Get all joints that start from the current node
        foreach (var joint in map.joints)
        {
            if (joint.from == currentNode && !visited.Contains(joint.to))
            {
                // Recursively visit the next node
                if (DFS(map, joint.to, targetNode, visited))
                {
                    return true;
                }
            }
        }

        // If no path is found, return false
        return false;
    }
}