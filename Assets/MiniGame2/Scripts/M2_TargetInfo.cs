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

        // float 오차 허용 (정확히 aim 위에 있는 경우도 앞에 있다고 간주)
        if (Mathf.Abs(diff) < 0.0001f || Mathf.Abs(diff - 360f) < 0.0001f)
            return true;

        if (isClockwise)
            return diff >= 0f && diff < 180f;
        else
            return diff >= 180f && diff <= 360f;
    }

}
