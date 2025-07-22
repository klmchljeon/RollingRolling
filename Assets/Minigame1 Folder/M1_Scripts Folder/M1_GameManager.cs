using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static M1_GameManager Instance;
    public float reverseTime = 30f;
    public SpriteRenderer backgroundRenderer;
    public Sprite newBackground;
    private float timer = 0f;
    private bool reversed = false;
    public int CurrentDirection { get; private set; } = 1;
    public Transform player;
    public Vector3 playerNewPosition;
    public M1_BackgroundRotation backgroundRotator;

    public M1_ScreenFader screenFader; //페이드 연출용

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); //페이드 연출을 위한 오브젝트 삭제제
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!reversed && timer >= reverseTime)
        {
            reversed = true;
            /*CurrentDirection = -1;

            if (backgroundRenderer != null && newBackground != null) //배경전환
            {
                backgroundRenderer.sprite = newBackground;
            }
            if (player != null)
            {
                player.position = playerNewPosition;
            }*/
            StartCoroutine(DoTransition());
        }
    }

    IEnumerator DoTransition()
    {
        //1.화면 어둡게
        yield return StartCoroutine(screenFader.FadeOut(0.2f));
        //2.남아있는 장애물 삭제
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            Destroy(enemy);
        }
        //3. 플레이어 위치 변경
        if (player != null)
            player.position = playerNewPosition;
        //4.배경전환
        if (backgroundRenderer != null && newBackground != null)
            backgroundRenderer.sprite = newBackground;
        //5.방향반전
        CurrentDirection = -1;
        //배경회전관련
        if (backgroundRotator != null)
            backgroundRotator.SetDirectiong(CurrentDirection);
        //6.화면밝게
            yield return StartCoroutine(screenFader.FadeIn(0.8f));
    }
}
