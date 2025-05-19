using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class M2_GenerateTarget : MonoBehaviour
{
    
    public GameObject target; // 타겟 오브젝트를 드래그하여 연결합니다.

    float MakeTarget(float exceptAngle)
    {
        float angle = Random.Range(0, 360); // 0도에서 360도 사이의 랜덤 각도를 생성합니다.
        while (true)
        {
            if (exceptAngle == -1) break; // 예외 각도가 -1이면 무한 루프를 종료합니다.
            
            float diff = Mathf.Abs(angle - exceptAngle); // 예외 각도와의 차이를 계산합니다.
            float d = Mathf.Min(360-diff, diff); // 360도에서의 차이를 고려하여 최소값을 구합니다.

            if (d > 30)
            {
                break;
            }
            else
            {
                angle = Random.Range(0, 360); // 예외 각도와의 차이가 30도 이하이면 다시 랜덤 각도를 생성합니다.
            }
        }
       
        // 타겟을 생성할 위치를 랜덤으로 결정합니다.
        Vector3 randomPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * 2;

        // 타겟을 생성합니다.
        GameObject Newtarget = Instantiate(target);
        Debug.Log(Newtarget.transform.localScale);
        Newtarget.transform.SetParent(transform); // 부모 오브젝트로 설정합니다.
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
