using UnityEngine;


[System.Serializable]
public class M2_TargetInfo
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
        if (isClockwise)
        {
            float diff = (aim + 360f - angle) % 360f;
            return diff;
        }
        else
        {
            float diff = (angle + 360f - aim) % 360f;
            return diff;
        }
    }

    public bool IsInFrontOfAim(float aim, bool isClockwise)
    {
        return true;
        float diff = (angle - aim + 360f) % 360f;

        // float ���� ��� (��Ȯ�� aim ���� �ִ� ��쵵 �տ� �ִٰ� ����)
        if (Mathf.Abs(diff) < 0.0001f || Mathf.Abs(diff - 360f) < 0.0001f)
            return true;

        if (isClockwise)
            return diff >= 0f && diff < 180f;
        else
            return diff >= 180f && diff <= 360f;
    }

}
