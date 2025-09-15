using UnityEngine;

public partial class M2_Judgement : MonoBehaviour
{
    float NormalizeAngleDiff(float angle)
    {
        if (float.IsInfinity(angle) || float.IsNaN(angle)) return float.MaxValue;
        angle = Mathf.Abs(angle);
        return Mathf.Min(angle, 360f - angle);
    }

    bool HasPassedTarget(float? prev, float current, float tolerance)
    {
        return current > prev && current < 360f - tolerance;
    }

    bool IsAngleInRange(float angle, float start, float end)
    {
        angle = (angle + 360f) % 360f;
        start = (start + 360f) % 360f;
        end = (end + 360f) % 360f;

        if (start < end)
            return angle >= start && angle <= end;
        else
            return angle >= start || angle <= end;
    }
}
