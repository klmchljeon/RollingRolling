using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    public M2_MoveAim moveAim;
    public M2_AngleManager angleManager;

    public float hitThreshold = 13f;
    public int score = 0;

    private TargetInfo currentClosestTarget = null;
    private float previousDiff = Mathf.Infinity;

    public bool justFired = false;

    // 외부에서 이 함수를 호출 (예: Fire 버튼)
    public void Fire()
    {
        if (moveAim == null || angleManager == null || angleManager.generatedTargets == null) return;
        if (angleManager.generatedTargets.Count == 0) return;

        float aimAngle = moveAim.aimangle;
        bool isClockwise = moveAim.isClockwise;

        List<TargetInfo> toRemove = new List<TargetInfo>();
        bool hitAny = false;

        foreach (var target in angleManager.generatedTargets.ToArray())
        {
            if (target == null || target.targetObject == null) continue;

            float diff = target.GetDirectionalAngleDifference(aimAngle, isClockwise);
            float realDiff = Mathf.Min(360f - diff, diff); // Normalize to 0~180

            if (realDiff <= hitThreshold)
            {
                int earned = CalculateScore(realDiff);
                score += earned;
                hitAny = true;

                Debug.Log($"Fire Hit! 타겟: {target.targetObject.name}, " +
                          $"diff: {diff:F2}°, realDiff: {realDiff:F2}°, 점수: +{earned}, 총점: {score}");

                toRemove.Add(target);
            }
            else
            {
                Debug.Log($"Missed target: {target.targetObject.name}, realDiff: {realDiff:F2}° > threshold {hitThreshold}");
            }
        }

        foreach (var target in toRemove)
        {
            if (target.targetObject != null)
            {
                Destroy(target.targetObject);
            }
            angleManager.generatedTargets.Remove(target);
        }

        if (!hitAny)
        {
            Debug.Log("게임 오버: 아무 타겟도 맞추지 못함.");
            GameOver();
            return;
        }

        if (angleManager.generatedTargets.Count == 0 && toRemove.Count > 0)
        {
            moveAim.ToggleDirection();
            Debug.Log("타겟 모두 제거. 방향 전환");
        }

        justFired = true;
        currentClosestTarget = null;
        previousDiff = Mathf.Infinity;
        Invoke(nameof(ResetJustFired), 0.3f);
    }





    public float distanceThreshold = 5f;  // 적절히 조정 필요
    private bool gameOverTriggered = false;

    void Update()
    {
        if (angleManager == null || moveAim == null || angleManager.generatedTargets == null) return;
        if (angleManager.generatedTargets.Count == 0)
        {
            currentClosestTarget = null;
            previousDiff = Mathf.Infinity;
            return;
        }

        float aimAngle = moveAim.aimangle;
        bool isClockwise = moveAim.isClockwise;

        TargetInfo next = angleManager.GetNextTarget(aimAngle, isClockwise);
        if (next == null || next.targetObject == null)
        {
            currentClosestTarget = null;
            previousDiff = Mathf.Infinity;
            return;
        }

        float currentDiff = next.GetDirectionalAngleDifference(aimAngle, isClockwise);

        // currentDiff가 유효한 값인지 체크
        if (float.IsInfinity(currentDiff) || float.IsNaN(currentDiff))
        {
            Debug.LogWarning("currentDiff가 유효하지 않음. 기본값 360으로 설정");
            currentDiff = 360f;
        }


        float normCurrentDiff = NormalizeAngleDiff(currentDiff);
        float normPreviousDiff = NormalizeAngleDiff(previousDiff);

        float positionDistance = Vector3.Distance(moveAim.transform.position, next.targetObject.transform.position);

        if (next != currentClosestTarget)
        {
            currentClosestTarget = next;
            previousDiff = currentDiff;
            // 여기서 바로 return하지 말고 판정 진행
        }

        if (!gameOverTriggered)
        {
            if (normCurrentDiff > normPreviousDiff + hitThreshold)
            {
                Debug.Log($"게임 오버: 각도 변화 초과 {normCurrentDiff} > {normPreviousDiff} + {hitThreshold}");
                GameOver();
                gameOverTriggered = true;
                return;
            }

            if (positionDistance > distanceThreshold)
            {
                Debug.Log($"게임 오버: 거리 초과 {positionDistance} > {distanceThreshold}");
                GameOver();
                gameOverTriggered = true;
                return;
            }
        }

        previousDiff = currentDiff;
    }


    float NormalizeAngleDiff(float angle)
    {
        if (float.IsInfinity(angle) || float.IsNaN(angle))
            return float.MaxValue;  // 또는 적당한 큰 값

        angle = Mathf.Abs(angle);  // 음수 방지

        return Mathf.Min(angle, 360f - angle);
    }



    void GameOver()
    {
        Debug.Log("게임 오버 발생!");
        // 필요한 경우 버튼 비활성화, 점수 저장, 씬 리셋 등 구현
        enabled = false;
    }

    void ResetJustFired()
    {
        justFired = false;
    }

    int CalculateScore(float realDiff)
    {
        if (realDiff <= 4f)
        {
            return 30;
        }
        else if (realDiff <= 9f)
        {
            return 20;
        }
        else if (realDiff <= 13f)
        {
            return 10;
        }
        else
        {
            return 0;
        }
    }

}
