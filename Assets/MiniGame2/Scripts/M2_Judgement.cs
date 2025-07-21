using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    public M2_MoveAim moveAim;
    public M2_AngleManager angleManager;
    public M2_ScoreManager scoreManager;
    public M2_GenerateTarget generateTarget;

    float hitThreshold = 13f;
    public int score = 0;

    private TargetInfo currentClosestTarget = null;
    private float? previousDiffNullable = null;

    private bool justFired = false;
    private bool gameOverTriggered = false;
    private bool waitingForNextTargets = false;

    private float previousAimAngle;
    private bool isFirstUpdate = true;

    private float tolerance = 15f;
    private int fireSkipFrames = 0;

    private TargetInfo lastFiredTarget = null;  // Fire 시점 타겟 복사 저장

    void Start()
    {
        justFired = false;
        fireSkipFrames = 0;
        isFirstUpdate = true;
        gameOverTriggered = false;
        lastFiredTarget = null;
    }

    public void Fire()
    {
        if (moveAim == null || angleManager == null || angleManager.generatedTargets == null) return;
        if (angleManager.generatedTargets.Count == 0) return;

        justFired = true;
        fireSkipFrames = 5;

        currentClosestTarget = null;
        previousDiffNullable = null;

        float aimAngle = moveAim.aimangle;
        bool isClockwise = moveAim.isClockwise;

        TargetInfo target = angleManager.GetNextTarget(aimAngle, isClockwise);
        if (target == null || target.targetObject == null) return;

        // Fire 시점 타겟 복사
        lastFiredTarget = new TargetInfo(target.targetObject, target.angle);

        float diff = target.GetDirectionalAngleDifference(aimAngle, isClockwise);
        float realDiff = Mathf.Min(360f - diff, diff);

        if (realDiff <= hitThreshold)
        {
            int earned = scoreManager.CalculateScore(realDiff);
            score += earned;

            Debug.Log($"Fire Hit! 타겟: {target.targetObject.name}, diff: {diff:F2}°, 점수: +{earned}");

            Destroy(target.targetObject);
            angleManager.generatedTargets.Remove(target);

            if (angleManager.generatedTargets.Count == 0)
            {
                waitingForNextTargets = true;
            }
        }
        else
        {
            Debug.Log($"Fire Missed! realDiff: {realDiff:F2}° > threshold {hitThreshold}");
            GameOver();
        }

        Invoke(nameof(ResetJustFired), 0.3f);
    }

    void Update()
    {
        if (angleManager == null || moveAim == null || angleManager.generatedTargets == null) return;

        if (angleManager.generatedTargets.Count == 0)
        {
            if (waitingForNextTargets)
            {
                moveAim.ToggleDirection();
                GenerateNextTargets();
            }
            return;
        }

        if (fireSkipFrames > 0)
        {
            fireSkipFrames--;
            return;
        }

        if (justFired || gameOverTriggered) return;

        float aimAngle = moveAim.aimangle;
        bool isClockwise = moveAim.isClockwise;

        TargetInfo next = angleManager.GetNextTarget(aimAngle, isClockwise);
        if (next == null || next.targetObject == null) return;

        float currentDiff = next.GetDirectionalAngleDifference(aimAngle, isClockwise);
        float normCurrentDiff = NormalizeAngleDiff(currentDiff);

        if (previousDiffNullable == null)
        {
            previousDiffNullable = currentDiff;
            currentClosestTarget = next;

            // 최초 Update시 previousAimAngle 초기화 (중요!)
            previousAimAngle = aimAngle;
            isFirstUpdate = false;
        }
        else
        {
            float normPreviousDiff = NormalizeAngleDiff(previousDiffNullable.Value);

            // Fire 시점 타겟과 현재 차이 비교 (기존 판정 보완)
            float lastFiredDiff = lastFiredTarget != null
                ? lastFiredTarget.GetDirectionalAngleDifference(aimAngle, isClockwise)
                : currentDiff;
            float normLastFiredDiff = NormalizeAngleDiff(lastFiredDiff);

            if (normCurrentDiff > normLastFiredDiff + tolerance)
            {
                Debug.Log($"게임 오버: 각도 초과 (현재 {normCurrentDiff:F2} > Fire시점 {normLastFiredDiff:F2} + {tolerance})");
                GameOver();
                return;
            }

            currentClosestTarget = next;
            previousDiffNullable = currentDiff;
        }

        if (currentClosestTarget != null)
        {
            float targetAngle = currentClosestTarget.angle;
            float currentAimAngle = moveAim.aimangle;

            if (HasPassedTarget(previousAimAngle, currentAimAngle, targetAngle, isClockwise, tolerance))
            {
                Debug.Log($"게임 오버: 타겟 {targetAngle:F2}° 지나침!");
                GameOver();
                return;
            }

            previousAimAngle = currentAimAngle;
        }
    }

    void GenerateNextTargets()
    {
        if (generateTarget != null)
        {
            generateTarget.GeneratingTarget();
            Debug.Log("새 타겟 생성 완료");
        }

        justFired = false;
        currentClosestTarget = null;
        previousDiffNullable = null;
        gameOverTriggered = false;
        waitingForNextTargets = false;
        isFirstUpdate = true;
        fireSkipFrames = 0;
        lastFiredTarget = null;
    }

    float NormalizeAngleDiff(float angle)
    {
        if (float.IsInfinity(angle) || float.IsNaN(angle)) return float.MaxValue;
        angle = Mathf.Abs(angle);
        return Mathf.Min(angle, 360f - angle);
    }

    void ResetJustFired()
    {
        justFired = false;
        Debug.Log("justFired 상태 해제");
    }

    void GameOver()
    {
        Debug.Log("게임 오버 발생");
        moveAim.stopMoving();
        gameOverTriggered = true;
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

        if (isClockwise)
        {
            if (current < prev) current += 360f;
            if (target < prev) target += 360f;
            return target > prev && target <= current;
        }
        else
        {
            if (current > prev) current -= 360f;
            if (target > prev) target -= 360f;
            return target < prev && target >= current;
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
