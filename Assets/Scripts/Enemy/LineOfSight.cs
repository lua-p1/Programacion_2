using UnityEngine;
public static class LineOfSight
{
    public static bool IsOnSight(Vector3 start, Vector3 end,LayerMask obstacle)
    {
        var direction = end - start;
        return !Physics.Raycast(start, direction.normalized, direction.magnitude, obstacle);
    }
}
