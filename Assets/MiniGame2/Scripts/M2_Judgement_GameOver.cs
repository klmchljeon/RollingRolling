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

        if (previousDiffNullable == null)
        {

            // 🔸 AngleManager에서 현재 Aim 앞에 있는 가장 가까운 타겟 가져오기
            next = angleManager.GetNextTarget(aimAngle, isClockwise);
            if (next == null || next.targetObject == null) return;
            //Debug.Log($"가장 가까운 타겟: {next.targetObject.name} at {next.angle}°, {isClockwise}");

            previousDiffNullable = next.GetDirectionalAngleDifference(aimAngle, isClockwise);
            currentClosestTarget = next;

            return;
        }


        //currentClosestTarget = next;
        //previousDiffNullable = currentDiff;

        // 🔹 타겟을 지나쳤는지 체크
        float curDiff = next.GetDirectionalAngleDifference(aimAngle, isClockwise);

        if (HasPassedTarget(previousDiffNullable, curDiff, tolerance))
        {
            
            //Debug.Log($"게임 오버: 타겟 지나침!");
            GameOver();
            return;
        }
    }
}
