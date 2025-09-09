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
        return true; // Debugging: 항상 true 반환
        Debug.Log($"IsInFrontOfAim 호출: target angle={angle}, aim={aim}, isClockwise={isClockwise}");
        float diff = (angle - aim + 360f) % 360f;
        if (isClockwise)
            return diff > 0f && diff < 180f;
        else
            return diff > 180f && diff < 360f;
    }
}
