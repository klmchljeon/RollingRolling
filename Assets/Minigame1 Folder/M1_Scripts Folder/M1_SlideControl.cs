using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class M1_SlideControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject object1; // 기본 오브젝트
    public GameObject object2; // 슬라이드 중 오브젝트

    public GroundChecker groundChecker;

    Sprite tmpSptite;
    Vector2 tmpOffest;
    Vector2 tmpSize;

    Vector3 tmpPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (groundChecker == null || !groundChecker.isGrounded) return;

        tmpSptite = object1.GetComponent<SpriteRenderer>().sprite;
        tmpOffest = object1.GetComponent<CapsuleCollider2D>().offset;
        tmpSize = object1.GetComponent<CapsuleCollider2D>().size;
        //tmpPosition = object1.GetComponent<Transform>().position;

        object1.GetComponent<SpriteRenderer>().sprite = object2.GetComponent<SpriteRenderer>().sprite;
        object1.GetComponent<CapsuleCollider2D>().offset = object2.GetComponent<CapsuleCollider2D>().offset;
        object1.GetComponent<CapsuleCollider2D>().size = object2.GetComponent<CapsuleCollider2D>().size;
        //object1.GetComponent<Transform>().position = object2.GetComponent<Transform>().position;

        //object2.transform.position = object1.transform.position;
        //object2.transform.rotation = Quaternion.Euler(0f,0f,12f);


        //object1.SetActive(false);
        //object2.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (groundChecker == null || !groundChecker.isGrounded) return;

        object1.GetComponent<SpriteRenderer>().sprite = tmpSptite;
        object1.GetComponent<CapsuleCollider2D>().offset = tmpOffest;
        object1.GetComponent<CapsuleCollider2D>().size = tmpSize;
        //object1.GetComponent<Transform>().position = tmpPosition;

        //object1.transform.position = object2.transform.position;
        //object2.transform.rotation = object1.transform.rotation;

        //object1.SetActive(true);
        //object2.SetActive(false);
    }
}
