using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class M2_GenerateTarget : MonoBehaviour
{
    
    public GameObject target; // Ÿ�� ������Ʈ�� �巡���Ͽ� �����մϴ�.

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
        Vector3 randomPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * 2;

        // Ÿ���� �����մϴ�.
        GameObject Newtarget = Instantiate(target);
        Debug.Log(Newtarget.transform.localScale);
        Newtarget.transform.SetParent(transform); // �θ� ������Ʈ�� �����մϴ�.
        Newtarget.transform.position = randomPosition;
        return angle;
    }



    // Update is called once per frame
    void Update()
    {
    
    }

    public void MakeTargetTwice()
    {
        float tmp = MakeTarget(-1);
        MakeTarget(tmp);
    }
}
