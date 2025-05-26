using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class M2_Judgement : MonoBehaviour
{
    public GameObject target;
    public GameObject aim;
    public M2_AngleManager angleManager;
    public M2_MoveAim moveAim;

    private void OnTriggerExit2D(Collider2D other) 
    {
        
    }
}
