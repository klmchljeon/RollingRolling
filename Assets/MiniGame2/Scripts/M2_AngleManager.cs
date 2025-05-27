using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class M2_AngleManager : MonoBehaviour
{
    public List<TargetInfo> generatedTargets = new();

    public void ClearTargets()
    {
        generatedTargets.Clear();
    }

    public void AddTarget(GameObject obj, float angle)
    {
        generatedTargets.Add(new TargetInfo(obj, angle));
    }

    public void SortTargetsByAimWithDirection(float aimAngle, bool clockwise)
    {
        generatedTargets.Sort((a, b) =>
            a.GetDirectionalAngleDifference(aimAngle, clockwise)
            .CompareTo(b.GetDirectionalAngleDifference(aimAngle, clockwise)));
    }

    public TargetInfo GetNextTarget(float aimAngle, bool clockwise)
    {
        if (generatedTargets.Count == 0) return null;

        SortTargetsByAimWithDirection(aimAngle, clockwise);
        return generatedTargets[0];
    }


    public void PrintAllAngles()
    {
        if (generatedTargets.Count == 0)
        {
            Debug.Log("저장된 타겟이 없습니다.");
            return;
        }

        Debug.Log("저장된 타겟 각도 리스트:");
        for (int i = 0; i < generatedTargets.Count; i++)
        {
            Debug.Log($"{i + 1}. {generatedTargets[i].angle} degrees");
        }
    }
}
