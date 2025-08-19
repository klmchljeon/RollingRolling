// TargetInfo.cs (순수 개별 타겟 정보만 유지)
using UnityEngine;


[System.Serializable]
public class M2_TargetInfo : MonoBehaviour
{
    public GameObject targetObject;
    public float angle;

    public M2_TargetInfo(GameObject obj, float angle)
    {
        this.targetObject = obj;
        this.angle = angle;
    }

    public float GetDirectionalAngleDifference(float aim, bool isClockwise)
    {
        float diff = (angle - aim + 360f) % 360f;
        return isClockwise ? diff : (360f - diff) % 360f;
    }

    public bool IsInFrontOfAim(float aim, bool isClockwise)
    {
        float diff = (angle - aim + 360f) % 360f;
        if (isClockwise)
            return diff > 0f && diff < 180f;
        else
            return diff > 180f && diff < 360f;
    }
}
