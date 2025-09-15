using System.Collections.Generic;
using UnityEngine;

public class M2_AngleManager : MonoBehaviour
{
    public List<M2_TargetInfo> generatedTargets = new();

    public void ClearTargets()
    {
        generatedTargets.Clear();
    }

    public void AddTarget(GameObject obj, float angle)
    {
        //Debug.Log($"AddTarget 호출: {obj.name} at angle {angle}°");
        generatedTargets.Add(new M2_TargetInfo(obj, angle));
        //Debug.Log(generatedTargets[generatedTargets.Count - 1].targetObject.name);
    }

    // 앞에 있는 타겟만 필터링해서 정렬하도록 수정
    public void SortTargetsByAimWithDirection(float aimAngle, bool clockwise)
    {
        return;
        // 앞에 있는 타겟만 추출
        List<M2_TargetInfo> frontTargets = generatedTargets.FindAll(t => t.IsInFrontOfAim(aimAngle, clockwise));

        // 앞에 있는 타겟만 정렬
        frontTargets.Sort((a, b) =>
        {
            float aDiff = a.GetDirectionalAngleDifference(aimAngle, clockwise);
            float bDiff = b.GetDirectionalAngleDifference(aimAngle, clockwise);
            return aDiff.CompareTo(bDiff);
        });

        Debug.Log("앞에 있는 타겟만 정렬된 목록:");
        for (int i = 0; i < frontTargets.Count; i++)
        {
            var t = frontTargets[i];
            float diff = t.GetDirectionalAngleDifference(aimAngle, clockwise);
            Debug.Log($"{i + 1}. {t.targetObject.name}, angle={t.angle}, diff={diff:F2}");
        }

        // 원본 리스트는 그대로 유지하거나, 필요 시 앞에 있는 타겟만 따로 관리할 수도 있습니다.
    }

    // 앞에 있는 타겟 중 가장 가까운 타겟 반환
    public M2_TargetInfo GetNextTarget(float aimAngle, bool clockwise)
    {
        List<M2_TargetInfo> frontTargets = generatedTargets.FindAll(t => t.IsInFrontOfAim(aimAngle, clockwise));
        //Debug.Log($"GetNextTarget 호출: aimAngle={aimAngle}, clockwise={clockwise}, 앞에 있는 타겟 수={frontTargets.Count}");
        if (frontTargets.Count == 0) return null;

        frontTargets.Sort((a, b) =>
        {
            float aDiff = a.GetDirectionalAngleDifference(aimAngle, clockwise);
            float bDiff = b.GetDirectionalAngleDifference(aimAngle, clockwise);
            return aDiff.CompareTo(bDiff);
        });

        //Debug.Log($"가장 가까운 타: {frontTargets[0].angle}");
        return frontTargets[0];
    }

    public void PrintAllAngles()
    {
        if (generatedTargets.Count == 0)
        {
            //Debug.Log("저장된 타겟이 없습니다.");
            return;
        }

        //Debug.Log("저장된 타겟 각도 리스트:");
        for (int i = 0; i < generatedTargets.Count; i++)
        {
            //Debug.Log($"{i + 1}. {generatedTargets[i].angle} degrees");
        }
    }
}
