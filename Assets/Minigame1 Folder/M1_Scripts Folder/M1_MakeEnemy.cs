using System.Collections;
using System.Collections.Generic;
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
            GameObject Enemy = Instantiate(EnemyList[Random.Range(0, 4)]);
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
            Destroy(Enemy, 4f);
        }
    }
}
