using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    public M2_MoveAim moveAim;
    public M2_AngleManager angleManager;

    public float hitThreshold = 10f;
    public int score = 0;

    // �ܺο��� �� �Լ��� ȣ���ϵ��� ���� (��: ��ư ���� ��)
    public void Fire()
    {
        float aimAngle = moveAim.aimangle;
        bool isClockwise = moveAim.isClockwise;

        if (angleManager.generatedTargets.Count == 0) return;

        List<TargetInfo> toRemove = new List<TargetInfo>();

        foreach (var target in angleManager.generatedTargets)
        {
            float diff = target.GetDirectionalAngleDifference(aimAngle, isClockwise);
            Debug.Log($"Ÿ�� ����: {target.angle}, ���� aim: {aimAngle}, ������: {diff}");

            float distance = Mathf.Min(diff, 360f - diff); // �ּ� ȸ�� �Ÿ�

            if (distance <= hitThreshold)
            {
                int earned = CalculateScore(diff);
                score += earned * 10;
                Debug.Log($"Fire Hit! +{earned}��");

                Destroy(target.targetObject);
                toRemove.Add(target);
            }
        }

        foreach (var target in toRemove)
        {
            angleManager.generatedTargets.Remove(target);
        }

        if (angleManager.generatedTargets.Count == 0 && toRemove.Count > 0)
        {
            moveAim.ToggleDirection();
            Debug.Log("Ÿ�� ��� ����. ���� ��ȯ");
        }
    }

    int CalculateScore(float diff)
    {
        float maxScore = 10f;
        float ratio = Mathf.Clamp01((hitThreshold - diff) / hitThreshold);
        return Mathf.RoundToInt(maxScore * ratio);
    }
}
