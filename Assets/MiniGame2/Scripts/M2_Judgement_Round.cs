using UnityEngine;

public partial class M2_Judgement : MonoBehaviour
{
    void GenerateNextTargets()
    {
        if (generateTarget != null)
        {
            generateTarget.GeneratingTarget();
            Debug.Log("货 鸥百 积己 肯丰");
        }

        ResetRoundState();
    }

    void ResetRoundState()
    {
        justFired = false;
        currentClosestTarget = null;
        previousDiffNullable = null;
        gameOverTriggered = false;
        waitingForNextTargets = false;
        isFirstUpdate = true;
        fireSkipFrames = 0;
        lastFiredTarget = null;

        if (moveAim != null)
            previousAimAngle = moveAim.aimangle;
    }
}
