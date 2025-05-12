using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_BackgroundRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationspeed;
    public float reverseinterval;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f,0f,rotationspeed*Time.deltaTime);
    }
}
