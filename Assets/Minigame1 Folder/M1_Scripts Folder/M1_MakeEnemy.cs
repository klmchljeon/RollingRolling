using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class M1_MakeEnemy : MonoBehaviour
{
    public Transform center;
    public GameObject[] EnemyList = new GameObject[4];
    public float timeDiffer;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeDiffer)
        {
            SpawnEnemy(); //이놈 둘이가 추가한거임
            timer = 0;
            /*GameObject Enemy = Instantiate(EnemyList[Random.Range(0, 4)]);
            foreach (Transform t in Enemy.transform)
            {
                if (t.GetComponent<M1_doverotation>() != null)
                {
                    t.GetComponent<M1_doverotation>().centerObject1 = center;
                }

                if (t.GetComponent<M1_stonerotation>() != null)
                {
                    t.GetComponent<M1_stonerotation>().centerObject = center;
                }
            }
            //Enemy.transform.GetChild(0).GetComponent<M1_stonerotation>().centerObject = center;
            timer = 0;
            Destroy(Enemy, 3f); */ //이게 원래코드임

        }

    }

    void SpawnEnemy()
    {
        GameObject Enemy = Instantiate(EnemyList[Random.Range(0, 4)]);
        if (M1_GameManager.Instance.CurrentDirection == -1)
        {
            foreach (Transform t in Enemy.transform)
            {
                Vector3 p = t.position;
                t.position = new Vector3(-p.x, p.y, p.z);
            }
        }
            foreach (Transform t in Enemy.transform)
        {
            if (t.GetComponent<M1_doverotation>() != null)
            {
                t.GetComponent<M1_doverotation>().centerObject1 = center;
                t.GetComponent<M1_doverotation>().SetDirection1(M1_GameManager.Instance.CurrentDirection); //추가한거거

            }

            if (t.GetComponent<M1_stonerotation>() != null)
            {
                t.GetComponent<M1_stonerotation>().centerObject = center;
                t.GetComponent<M1_stonerotation>().SetDirection(M1_GameManager.Instance.CurrentDirection); //요놈도 추가한거
            }
        }
            //Enemy.transform.GetChild(0).GetComponent<M1_stonerotation>().centerObject = center;

            Destroy(Enemy, 5f);
    }
}
