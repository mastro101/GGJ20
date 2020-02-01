using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Utility
{
    public static Vector2 ConvertV3InV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }

    public static Vector3 ConvertV2InV3(Vector2 v3)
    {
        return new Vector3(v3.x, v3.y);
    }
}
