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
        //Debug.Log($"AddTarget ȣ��: {obj.name} at angle {angle}��");
        generatedTargets.Add(new M2_TargetInfo(obj, angle));
        //Debug.Log(generatedTargets[generatedTargets.Count - 1].targetObject.name);
    }

    // �տ� �ִ� Ÿ�ٸ� ���͸��ؼ� �����ϵ��� ����
    public void SortTargetsByAimWithDirection(float aimAngle, bool clockwise)
    {
        return;
        // �տ� �ִ� Ÿ�ٸ� ����
        List<M2_TargetInfo> frontTargets = generatedTargets.FindAll(t => t.IsInFrontOfAim(aimAngle, clockwise));

        // �տ� �ִ� Ÿ�ٸ� ����
        frontTargets.Sort((a, b) =>
        {
            float aDiff = a.GetDirectionalAngleDifference(aimAngle, clockwise);
            float bDiff = b.GetDirectionalAngleDifference(aimAngle, clockwise);
            return aDiff.CompareTo(bDiff);
        });

        Debug.Log("�տ� �ִ� Ÿ�ٸ� ���ĵ� ���:");
        for (int i = 0; i < frontTargets.Count; i++)
        {
            var t = frontTargets[i];
            float diff = t.GetDirectionalAngleDifference(aimAngle, clockwise);
            Debug.Log($"{i + 1}. {t.targetObject.name}, angle={t.angle}, diff={diff:F2}");
        }

        // ���� ����Ʈ�� �״�� �����ϰų�, �ʿ� �� �տ� �ִ� Ÿ�ٸ� ���� ������ ���� �ֽ��ϴ�.
    }

    // �տ� �ִ� Ÿ�� �� ���� ����� Ÿ�� ��ȯ
    public M2_TargetInfo GetNextTarget(float aimAngle, bool clockwise)
    {
        List<M2_TargetInfo> frontTargets = generatedTargets.FindAll(t => t.IsInFrontOfAim(aimAngle, clockwise));
        //Debug.Log($"GetNextTarget ȣ��: aimAngle={aimAngle}, clockwise={clockwise}, �տ� �ִ� Ÿ�� ��={frontTargets.Count}");
        if (frontTargets.Count == 0) return null;

        frontTargets.Sort((a, b) =>
        {
            float aDiff = a.GetDirectionalAngleDifference(aimAngle, clockwise);
            float bDiff = b.GetDirectionalAngleDifference(aimAngle, clockwise);
            return aDiff.CompareTo(bDiff);
        });

        //Debug.Log($"���� ����� Ÿ: {frontTargets[0].angle}");
        return frontTargets[0];
    }

    public void PrintAllAngles()
    {
        if (generatedTargets.Count == 0)
        {
            //Debug.Log("����� Ÿ���� �����ϴ�.");
            return;
        }

        //Debug.Log("����� Ÿ�� ���� ����Ʈ:");
        for (int i = 0; i < generatedTargets.Count; i++)
        {
            //Debug.Log($"{i + 1}. {generatedTargets[i].angle} degrees");
        }
    }
}
