using System.Collections.Generic;
using UnityEngine;

public class M2_GenerateTarget : MonoBehaviour
{
    public GameObject target;
    public M2_AngleManager angleManager;
    public M2_MoveAim moveAim;

    private int round = 0;  // ���� ī���� �߰�

    private void Start()
    {
        angleManager = GetComponent<M2_AngleManager>();
    }

    float MakeTarget(List<float> existingAngles)
    {
        float minAngleGap = 30f;
        float minDistanceFromAim = 30f;
        float angle = Random.Range(0, 360);
        int safety = 0;

        while (true)
        {
            bool isFarEnough = true;

            if (moveAim != null)
            {
                float aimDiff = Mathf.Abs(angle - moveAim.aimangle);
                float aimDist = Mathf.Min(360 - aimDiff, aimDiff);
                if (aimDist < minDistanceFromAim)
                    isFarEnough = false;
            }

            foreach (float existing in existingAngles)
            {
                float diff = Mathf.Abs(angle - existing);
                float dist = Mathf.Min(360 - diff, diff);
                if (dist < minAngleGap)
                {
                    isFarEnough = false;
                    break;
                }
            }

            if (isFarEnough || safety++ > 100) break;
            angle = Random.Range(0, 360);
        }

        float rad = angle * Mathf.Deg2Rad;
        Vector3 pos = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * 2;

        GameObject newTarget = Instantiate(target);
        newTarget.transform.SetParent(transform);
        newTarget.transform.position = pos;

        angleManager.AddTarget(newTarget, angle);
        return angle;
    }


    public void GeneratingTarget()
    {
        angleManager.ClearTargets();

        int targetNumber = Random.Range(1, 3); // 1 �Ǵ� 2��
        List<float> existingAngles = new List<float>();

        for (int i = 0; i < targetNumber; i++)
        {
            float angle = MakeTarget(existingAngles);
            existingAngles.Add(angle);
        }

        angleManager.SortTargetsByAimWithDirection(moveAim.aimangle, moveAim.isClockwise);

        // ���� ���� �� MoveAim �ӵ� ���� ȣ��
        round++;
        if (moveAim != null)
        {
            moveAim.IncreaseSpeedByRound(round);
        }
    }
}
