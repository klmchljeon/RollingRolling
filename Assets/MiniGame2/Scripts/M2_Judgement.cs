using TMPro;
using System.Collections.Generic;
using UnityEngine;

public partial class M2_Judgement : MonoBehaviour
{
    [SerializeField] private TMP_Text cur_score;
    [SerializeField] private TMP_Text best_score;

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

    private bool isFirstUpdate = true;

    private float tolerance = 15f;
    private int fireSkipFrames = 0;

    private M2_TargetInfo lastFiredTarget = null;
    private M2_TargetInfo next;

    void Start()
    {
        cur_score.text = "Score: 0";
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

                //float aimAngle = moveAim.aimangle;
                //bool isClockwise = moveAim.isClockwise;
                //adjTarget = angleManager.GetNextTarget(aimAngle, isClockwise);

                previousDiffNullable = null;
                currentClosestTarget = null;
                lastFiredTarget = null;
                waitingForNextTargets = false;

                Debug.Log("[Judgement] 새로운 라운드 시작, 상태 초기화 완료");
            }
            return;
        }

        if (fireSkipFrames > 0)
        {
            fireSkipFrames--;
            return;
        }

        if (justFired || gameOverTriggered) return;

        UpdateGameOverCheck();
    }
}
