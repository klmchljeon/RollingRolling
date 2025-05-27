using UnityEngine;

public class TargetInfo
{
    public GameObject targetObject;
    public float angle;

    public TargetInfo(GameObject obj, float angle)
    {
        this.targetObject = obj;
        this.angle = angle;
    }

    public float GetDirectionalAngleDifference(float aimAngle, bool isClockwise)
    {
        if (isClockwise)
            return (angle - aimAngle + 360f) % 360f;
        else
            return (aimAngle - angle + 360f) % 360f;
    }
}
