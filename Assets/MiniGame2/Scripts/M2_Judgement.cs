using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    public M2_MoveAim moveAim;
    public M2_AngleManager angleManager;
    public M2_ScoreManager scoreManager; // 점수 관리 스크립트 추가

    float hitThreshold = 13f;
    public int score = 0;

    private TargetInfo currentClosestTarget = null;
    private float? previousDiffNullable = null; // null 가능 float로 선언

    public bool justFired = false;
    private bool gameOverTriggered = false;

    public M2_GenerateTarget generateTarget;  // 클래스 멤버 변수로 추가하세요
    private bool waitingForNextTargets = false;

    public void Fire()
    {
        justFired = true;
        currentClosestTarget = null;
        previousDiffNullable = null;

        if (moveAim == null || angleManager == null || angleManager.generatedTargets == null) return;
        if (angleManager.generatedTargets.Count == 0) return;

        float aimAngle = moveAim.aimangle;
        bool isClockwise = moveAim.isClockwise;

        TargetInfo target = angleManager.GetNextTarget(aimAngle, isClockwise);
        if (target == null || target.targetObject == null) return;

        float diff = target.GetDirectionalAngleDifference(aimAngle, isClockwise);
        float realDiff = Mathf.Min(360f - diff, diff); // Normalize to 0~180

        if (realDiff <= hitThreshold)
        {
            int earned = scoreManager.CalculateScore(realDiff);
            score += earned;

            Debug.Log($"Fire Hit! 타겟: {target.targetObject.name}, " +
                      $"diff: {diff:F2}°, realDiff: {realDiff:F2}°, 점수: +{earned}, 총점: {score}");

            Destroy(target.targetObject);
            angleManager.generatedTargets.Remove(target);

            if (angleManager.generatedTargets.Count == 0)
            {
                moveAim.ToggleDirection();
                Debug.Log("타겟 모두 제거. 방향 전환");

                waitingForNextTargets = true;  // 자동 생성 대기 상태 진입
                //Invoke(nameof(GenerateNextTargets), 0.5f);  // 타겟 자동 생성 예약
            }
        }
        else
        {
            Debug.Log($"Fire Missed! realDiff: {realDiff:F2}° > threshold {hitThreshold}");
            GameOver();
            return;
        }

        Invoke(nameof(ResetJustFired), 0.5f);
    }


    void GenerateNextTargets()
    {
        if (generateTarget != null)
        {
            generateTarget.GeneratingTarget();
            Debug.Log("새 타겟 자동 생성");
        }

        // 상태 초기화
        justFired = false;
        currentClosestTarget = null;
        previousDiffNullable = null;
        gameOverTriggered = false;
        waitingForNextTargets = false;
    }


    void Update()
    {
        if (angleManager == null || moveAim == null || angleManager.generatedTargets == null) return;

        // ✅ 자동 생성 대기 상태에서 타겟이 완전히 사라진 경우 새 타겟 생성
        Debug.Log($"{waitingForNextTargets} 123");
        if (waitingForNextTargets && angleManager.generatedTargets.Count == 0)
        {
            GenerateNextTargets();  // 바로 호출
            return;
        }

        // 이 조건은 자동 생성 대기 상태가 아닐 때만 처리
        if (justFired || gameOverTriggered) return;

        if (angleManager.generatedTargets.Count == 0)
        {
            currentClosestTarget = null;
            previousDiffNullable = null;
            return;
        }

        float aimAngle = moveAim.aimangle;
        bool isClockwise = moveAim.isClockwise;

        TargetInfo next = angleManager.GetNextTarget(aimAngle, isClockwise);
        if (next == null || next.targetObject == null)
        {
            currentClosestTarget = null;
            previousDiffNullable = null;
            return;
        }

        float currentDiff = next.GetDirectionalAngleDifference(aimAngle, isClockwise);
        if (float.IsNaN(currentDiff) || float.IsInfinity(currentDiff)) currentDiff = 360f;

        float normCurrentDiff = NormalizeAngleDiff(currentDiff);

        if (previousDiffNullable == null)
        {
            previousDiffNullable = currentDiff;
            currentClosestTarget = next;
            return;
        }

        float normPreviousDiff = NormalizeAngleDiff(previousDiffNullable.Value);

        if (normCurrentDiff > normPreviousDiff + hitThreshold)
        {
            Debug.Log($"게임 오버: 각도 초과 {normCurrentDiff} > {normPreviousDiff} + {hitThreshold}");
            GameOver();
            gameOverTriggered = true;
            return;
        }

        currentClosestTarget = next;
        previousDiffNullable = currentDiff;
    }




    float NormalizeAngleDiff(float angle)
    {
        if (float.IsInfinity(angle) || float.IsNaN(angle))
            return float.MaxValue;

        angle = Mathf.Abs(angle);

        return Mathf.Min(angle, 360f - angle);
    }


    void GameOver()
    {
        Debug.Log("게임 오버 발생!");
        moveAim.stopMoving();  // Aim 이동 중지
        // 추가로 게임 오버 UI 처리 등 여기에 구현 가능
    }


    void ResetJustFired()
    {
        justFired = false;
        Debug.Log("justFired 상태 해제");
    }
}
