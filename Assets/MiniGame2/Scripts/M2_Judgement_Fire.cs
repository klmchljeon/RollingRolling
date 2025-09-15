using UnityEngine;

public partial class M2_Judgement : MonoBehaviour
{
    public void Fire()
    {

        if (moveAim == null || angleManager == null || angleManager.generatedTargets == null) return;
        if (angleManager.generatedTargets.Count == 0) return;


        justFired = true;
        fireSkipFrames = 1;

        currentClosestTarget = null;
        previousDiffNullable = null;

        float aimAngle = moveAim.aimangle;
        bool isClockwise = moveAim.isClockwise;

        //Debug.Log($"Fire! Aim Angle: {aimAngle}°, Direction: {(isClockwise ? "Clockwise" : "Counterclockwise")}");
        M2_TargetInfo target = next;
        //Debug.Log(target.targetObject.name);
        if (target == null || target.targetObject == null) return;

        //Debug.Log("fire123");

        lastFiredTarget = new M2_TargetInfo(target.targetObject, target.angle);

        float diff = target.GetDirectionalAngleDifference(aimAngle, isClockwise);
        float realDiff = Mathf.Min(360f - diff, diff);

        if (realDiff <= hitThreshold)
        {
            int earned = scoreManager.CalculateScore(realDiff);
            score += earned;

            cur_score.text = $"Score: {score}";
            //베스트 스코어 갱신 로직

            //Debug.Log($"Fire Hit! 타겟: {target.targetObject.name}, diff: {realDiff:F2}°, 점수: +{earned}");

            Destroy(target.targetObject);
            angleManager.generatedTargets.Remove(target);

            // 🔹 Fire 후 남은 상태 강제 초기화
            currentClosestTarget = null;
            previousDiffNullable = null;

            if (angleManager.generatedTargets.Count == 0)
                waitingForNextTargets = true;
        }
        else
        {
            //Debug.Log($"Fire Missed! realDiff: {realDiff:F2}° > threshold {hitThreshold}");
            GameOver();
        }

        // 🔹 Fire 직후 판정 스킵 시간 살짝 늘림
        Invoke(nameof(ResetJustFired), 0.3f);
    }

    void ResetJustFired()
    {
        justFired = false;
    }

}
