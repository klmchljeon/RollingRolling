using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_AngleManager : MonoBehaviour
{
    public List<float> angleList = new List<float>();
    

    float tmp;

    public void SetTmp(float angle)
    {
        tmp = angle;
    }

    void RecordAngleAgain(float angle)
    {
        angleList.Add(angle);
    }

    public void RecordAngle()
    {
        RecordAngleAgain(tmp);
    }

    public void ClearAngle()
    {
        angleList.Clear();
    }

    public void PrintAllAngles()
    {
        if (angleList.Count == 0)
        {
            Debug.Log("����� ������ �����ϴ�.");
            return;
        }

        Debug.Log("����� ���� ����Ʈ:");
        for (int i = 0; i < angleList.Count; i++)
        {
            Debug.Log($"{angleList[i]} degrees");
        }
    }
}
