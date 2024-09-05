using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtensions 
{
    // Compare two colors and return true if they are approximately equal
    public static bool IsApproximately(this Color color, Color otherColor, float threshold = 0.02f)
    {
        return Mathf.Abs(color.r - otherColor.r) < threshold &&
               Mathf.Abs(color.g - otherColor.g) < threshold &&
               Mathf.Abs(color.b - otherColor.b) < threshold;
    }
}
