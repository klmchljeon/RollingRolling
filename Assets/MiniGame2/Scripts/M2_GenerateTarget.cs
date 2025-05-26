using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class M2_GenerateTarget : MonoBehaviour
{
    
    public GameObject target; // Ÿ�� ������Ʈ�� �巡���Ͽ� �����մϴ�.
    public M2_AngleManager angleManager; // Ÿ�� ���� ����Ʈ ��ũ��Ʈ�� �巡���Ͽ� �����մϴ�.
    public M2_MoveAim moveAim;

    private void Start()
    {
        angleManager = GetComponent<M2_AngleManager>();
    }

    float MakeTarget(float exceptAngle)
    {
        float angle = Random.Range(0, 360); // 0������ 360�� ������ ���� ������ �����մϴ�.

        while (true)
        {
            if (exceptAngle == -1) break; // ���� ������ -1�̸� ���� ������ �����մϴ�.
            
            float diff = Mathf.Abs(angle - exceptAngle); // ���� �������� ���̸� ����մϴ�.
            float d = Mathf.Min(360-diff, diff); // 360�������� ���̸� ����Ͽ� �ּҰ��� ���մϴ�.

            if (d > 30)
            {
                break;
            }
            else
            {
                angle = Random.Range(0, 360); // ���� �������� ���̰� 30�� �����̸� �ٽ� ���� ������ �����մϴ�.
                
            }

        }
       
        // Ÿ���� ������ ��ġ�� �������� �����մϴ�.
        float rad = angle * Mathf.Deg2Rad;
        Vector3 randomPosition = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * 2;

        // Ÿ���� �����մϴ�.
        GameObject Newtarget = Instantiate(target);
        Debug.Log(Newtarget.transform.localScale);
        Newtarget.transform.SetParent(transform); // �θ� ������Ʈ�� �����մϴ�.
        Newtarget.transform.position = randomPosition;


        angleManager.SetTmp(angle); // Ÿ�� ���� ����Ʈ�� üũ�մϴ�.
        angleManager.RecordAngle();

        return angle;
        
    }



    public void GeneratingTarget()
    {
        int targetNumber = Random.Range(1, 3); // 1 �Ǵ� 2�� �������� �����մϴ�.

        if (targetNumber == 1)
            MakeTarget(-1); // -1�� ���� ������ ����Ͽ� Ÿ���� �����մϴ�.
        else
        {
            float tmp = MakeTarget(-1);
            MakeTarget(tmp);
        }
        
        
        if (angleManager.angleList.Count == 2)
            {
            float anglediff0 = Mathf.Abs(angleManager.angleList[0] - moveAim.aimangle);
            float anglediff1 = Mathf.Abs(angleManager.angleList[1] - moveAim.aimangle);
            float anlgediffm = Mathf.Min(anglediff0, anglediff1);
            float anlgediffM = Mathf.Max(anglediff0, anglediff1);
            angleManager.ClearAngle();
            angleManager.angleList[0] = anlgediffm;
            angleManager.angleList[1] = anlgediffM;
            }

    }
}
