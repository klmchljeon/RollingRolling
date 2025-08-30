using UnityEngine;

public partial class M2_Judgement : MonoBehaviour
{
    float NormalizeAngleDiff(float angle)
    {
        if (float.IsInfinity(angle) || float.IsNaN(angle)) return float.MaxValue;
        angle = Mathf.Abs(angle);
        return Mathf.Min(angle, 360f - angle);
    }

    bool HasPassedTarget(float prev, float current, float target, bool isClockwise, float tolerance)
    {
        float Normalize(float a) => (a + 360f) % 360f;

        prev = Normalize(prev);
        current = Normalize(current);
        target = Normalize(target);

        float lowerBound = Normalize(target - tolerance);
        float upperBound = Normalize(target + tolerance);

        if (IsAngleInRange(current, lowerBound, upperBound)) return false;

        float delta = Mathf.DeltaAngle(prev, current);

        if (isClockwise)
        {
            if (delta < 0) delta += 360f;
            return prev < target && prev + delta >= target;
        }
        else
        {
            if (delta > 0) delta -= 360f;
            return prev > target && prev + delta <= target;
        }
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
