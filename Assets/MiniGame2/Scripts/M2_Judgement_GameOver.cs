using UnityEngine;

public partial class M2_Judgement : MonoBehaviour
{
    void GameOver()
    {
        Debug.Log("게임 오버 발생");
        moveAim.StopMoving();
        gameOverTriggered = true;
    }

    // 🔹 게임오버 판정 업데이트
    void UpdateGameOverCheck()
    {
        float aimAngle = moveAim.aimangle;
        bool isClockwise = moveAim.isClockwise;

        // 🔸 AngleManager에서 현재 Aim 앞에 있는 가장 가까운 타겟 가져오기
        M2_TargetInfo next = angleManager.GetNextTarget(aimAngle, isClockwise);
        if (next == null || next.targetObject == null) return;
        Debug.Log($"가장 가까운 타겟: {next.targetObject.name} at {next.angle}°");

        float currentDiff = next.GetDirectionalAngleDifference(aimAngle, isClockwise);
        float normCurrentDiff = NormalizeAngleDiff(currentDiff);

        if (previousDiffNullable == null)
        {
            previousDiffNullable = currentDiff;
            currentClosestTarget = next;

            previousAimAngle = aimAngle;
            isFirstUpdate = false;
        }
        else
        {
            float lastFiredDiff = lastFiredTarget != null
                ? lastFiredTarget.GetDirectionalAngleDifference(aimAngle, isClockwise)
                : currentDiff;
            float normLastFiredDiff = NormalizeAngleDiff(lastFiredDiff);

            // 🔹 발사 시점 각도보다 현재 차이가 tolerance 이상 커졌으면 GameOver
            if (normCurrentDiff > normLastFiredDiff + tolerance)
            {
                Debug.Log($"게임 오버: 각도 초과 (현재 {normCurrentDiff:F2} > Fire시점 {normLastFiredDiff:F2} + {tolerance})");
                GameOver();
                return;
            }

            currentClosestTarget = next;
            previousDiffNullable = currentDiff;
        }

        // 🔹 타겟을 지나쳤는지 체크
        if (currentClosestTarget != null)
        {
            float targetAngle = currentClosestTarget.angle;
            float currentAimAngle = moveAim.aimangle;

            if (!isFirstUpdate &&
                HasPassedTarget(previousAimAngle, currentAimAngle, targetAngle, isClockwise, tolerance))
            {
                Debug.Log($"게임 오버: 타겟 {targetAngle:F2}° 지나침!");
                GameOver();
                return;
            }

            previousAimAngle = currentAimAngle;
        }
    }
}
