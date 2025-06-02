using System.Collections.Generic;
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
        {
            float aDiff = a.GetDirectionalAngleDifference(aimAngle, clockwise);
            float bDiff = b.GetDirectionalAngleDifference(aimAngle, clockwise);
            return aDiff.CompareTo(bDiff);
        });

        Debug.Log("���ĵ� Ÿ�� ���:");
        for (int i = 0; i < generatedTargets.Count; i++)
        {
            var t = generatedTargets[i];
            float diff = t.GetDirectionalAngleDifference(aimAngle, clockwise);
            Debug.Log($"{i + 1}. {t.targetObject.name}, angle={t.angle}, diff={diff:F2}");
        }
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
            Debug.Log("����� Ÿ���� �����ϴ�.");
            return;
        }

        Debug.Log("����� Ÿ�� ���� ����Ʈ:");
        for (int i = 0; i < generatedTargets.Count; i++)
        {
            Debug.Log($"{i + 1}. {generatedTargets[i].angle} degrees");
        }
    }
}
