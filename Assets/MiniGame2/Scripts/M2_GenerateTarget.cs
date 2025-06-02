using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_GenerateTarget : MonoBehaviour
{
    public GameObject target;
    private M2_AngleManager angleManager;
    public M2_MoveAim moveAim;

    private int round = 0;  // 라운드 카운터 추가

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();  // 모든 Start() 호출이 끝난 다음 실행됨

        // 연결 안 됐을 경우 찾기
        if (angleManager == null) { 
            angleManager = GetComponent<M2_AngleManager>();
            Debug.Log(angleManager);
        }
        if (moveAim == null)
            moveAim = FindObjectOfType<M2_MoveAim>();

        if (angleManager == null)// || moveAim == null)
        {
            Debug.LogError("angleManager 또는 moveAim을 찾을 수 없습니다.");
            yield break;
        }

        GeneratingTarget();
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
        Debug.Log($"함수 호출 {gameObject.name}");
        angleManager.ClearTargets();

        int targetNumber = Random.Range(1, 3); // 1 또는 2개
        List<float> existingAngles = new List<float>();

        for (int i = 0; i < targetNumber; i++)
        {
            float angle = MakeTarget(existingAngles);
            existingAngles.Add(angle);
        }

        angleManager.SortTargetsByAimWithDirection(moveAim.aimangle, moveAim.isClockwise);

        // 라운드 증가 및 MoveAim 속도 증가 호출
        round++;
        if (moveAim != null)
        {
            moveAim.IncreaseSpeedByRound(round);
        }
    }
}
