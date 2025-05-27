using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    public M2_MoveAim moveAim;
    public M2_AngleManager angleManager;

    public float hitThreshold = 10f;
    public int score = 0;

    // 외부에서 이 함수를 호출하도록 설정 (예: 버튼 누를 때)
    public void Fire()
    {
        float aimAngle = moveAim.aimangle;
        bool isClockwise = moveAim.isClockwise;

        if (angleManager.generatedTargets.Count == 0) return;

        List<TargetInfo> toRemove = new List<TargetInfo>();

        foreach (var target in angleManager.generatedTargets)
        {
            float diff = target.GetDirectionalAngleDifference(aimAngle, isClockwise);
            Debug.Log($"타겟 각도: {target.angle}, 현재 aim: {aimAngle}, 방향차: {diff}");

            float distance = Mathf.Min(diff, 360f - diff); // 최소 회전 거리

            if (distance <= hitThreshold)
            {
                int earned = CalculateScore(diff);
                score += earned * 10;
                Debug.Log($"Fire Hit! +{earned}점");

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
            Debug.Log("타겟 모두 제거. 방향 전환");
        }
    }

    int CalculateScore(float diff)
    {
        float maxScore = 10f;
        float ratio = Mathf.Clamp01((hitThreshold - diff) / hitThreshold);
        return Mathf.RoundToInt(maxScore * ratio);
    }
}
