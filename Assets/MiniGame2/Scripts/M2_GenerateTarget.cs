using System.Collections;
using UnityEngine;

public class M2_GenerateTarget : MonoBehaviour
{
    public GameObject target;
    public M2_AngleManager angleManager;
    public M2_MoveAim moveAim;

    private void Start()
    {
        angleManager = GetComponent<M2_AngleManager>();
    }

    float MakeTarget(float exceptAngle)
    {
        float angle = Random.Range(0, 360);
        while (true)
        {
            if (exceptAngle == -1) break;
            float diff = Mathf.Abs(angle - exceptAngle);
            float d = Mathf.Min(360 - diff, diff);
            if (d > 30) break;
            angle = Random.Range(0, 360);
        }

        float rad = angle * Mathf.Deg2Rad;
        Vector3 pos = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * 2;

        GameObject newTarget = Instantiate(target);
        newTarget.transform.SetParent(transform);
        newTarget.transform.position = pos;

        // ?? 타겟 정보는 AngleManager에 위임
        angleManager.AddTarget(newTarget, angle);
        return angle;
    }

    public void GeneratingTarget()
    {
        angleManager.ClearTargets();

        int targetNumber = Random.Range(1, 3);
        if (targetNumber == 1)
            MakeTarget(-1);
        else
        {
            float tmp = MakeTarget(-1);
            MakeTarget(tmp);
        }

        // 방향성에 따라 정렬 (필요시)
        angleManager.SortTargetsByAimWithDirection(moveAim.aimangle, moveAim.isClockwise);
    }
}
