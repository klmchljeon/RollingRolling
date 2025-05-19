using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class M3_Move : MonoBehaviour
{
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 100f * speed * Time.deltaTime));
    }
}
