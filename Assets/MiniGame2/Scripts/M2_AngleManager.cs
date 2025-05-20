using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_AngleManager : MonoBehaviour
{
    private List<float> angleList = new List<float>();
    public IReadOnlyList<float> AngleList => angleList;

    float tmp;

    public void SetTmp(float angle)
    {
        tmp = angle;
    }

    void _recordAngle(float angle)
    {
        angleList.Add(angle);
    }

    public void RecordAngle()
    {
        _recordAngle(tmp);
    }

    public void ClearAngle()
    {
        angleList.Clear();
    }

    public void PrintAllAngles()
    {
        if (angleList.Count == 0)
        {
            Debug.Log("저장된 각도가 없습니다.");
            return;
        }

        Debug.Log("저장된 각도 리스트:");
        for (int i = 0; i < angleList.Count; i++)
        {
            Debug.Log($"{angleList[i]} degrees");
        }
    }
}
