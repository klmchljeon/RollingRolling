using System.Collections.Generic;
using UnityEngine;

public partial class M2_Judgement : MonoBehaviour
{
    public M2_MoveAim moveAim;
    public M2_AngleManager angleManager;
    public M2_ScoreManager scoreManager;
    public M2_GenerateTarget generateTarget;

    float hitThreshold = 15f;
    public int score = 0;

    private M2_TargetInfo currentClosestTarget = null;
    private float? previousDiffNullable = null;

    private bool justFired = false;
    private bool gameOverTriggered = false;
    private bool waitingForNextTargets = false;

    private float previousAimAngle;
    private bool isFirstUpdate = true;

    private float tolerance = 15f;
    private int fireSkipFrames = 0;

    private M2_TargetInfo lastFiredTarget = null;

    void Start()
    {
        ResetRoundState();
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

        // 🔹 GameOver 판정 보조 메서드 호출
        UpdateGameOverCheck();
    }

}
