using UnityEngine;

public partial class M2_Judgement : MonoBehaviour
{
    void GenerateNextTargets()
    {
        if (generateTarget != null)
        {
            generateTarget.GeneratingTarget();
            Debug.Log("�� Ÿ�� ���� �Ϸ�");
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
