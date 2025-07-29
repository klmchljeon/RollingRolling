using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3_MakeCircle : MonoBehaviour
{
    public GameObject circle;
    public float minRadius = 0.5f;
    public float maxRadius = 1f;
    public float timeDiff;
    public Transform containerTransform;

    public GameObject player; //�÷��̾� ����� �߰�(1��)
    public M3_Player_Stand playerScript; //�÷��̾� ȸ�� ��ũ��Ʈ(�ҷ����� �߰�)(1��)
    public AimController aimController; //������ ��ũ��Ʈ ����� �߰�(1��)

    //�÷��̾ ���� ���ִ� ���� �߰�(2�� ���� �� ���� �߰�)
    private GameObject playerCurrentCircle = null;

    private List<GameObject> circles = new List<GameObject>(); //���� ����Ʈ �߰�(1�� ����)
    float timer = 0;
    private float prevDiameter = 0f;
    private Vector3 prevPosition; //���� ���� ��ġ ����

    // Start is called before the first frame update
    void Start()
    {

        float randomradius = Random.Range(minRadius, maxRadius);
        float diameter = randomradius * 2f;

        prevDiameter = diameter;
        prevPosition = transform.position;

        FirstGen();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeDiff)
        {
            Gen();
        }
    }

    void FirstGen()
    {
        float randomradius = Random.Range(minRadius, maxRadius);
        float diameter = randomradius * 2f;

        Vector3 spawnPosition = Vector3.zero;

        GameObject newcircle = Instantiate(circle);
        newcircle.transform.SetParent(containerTransform);
        newcircle.transform.localPosition = spawnPosition;

        //ȸ�� �ӵ� 0�� ���� �ʵ��� ����(3�� ����)
        float speed = 0f;
        while (Mathf.Abs(speed) < 0.5f)
            speed = Random.Range(-2f, 2f);
        // ���� �ڵ�: new Vector3(4, Random.Range(-2.86f, 2.46f), 0);
        newcircle.GetComponent<M3_Move>().speed = speed;

        newcircle.transform.localScale = new Vector3(diameter, diameter, 1f);
        newcircle.SetActive(true);//�̰� GPT Ž��
        timer = 0;

        //�÷��̾� ���ؼ� Ȱ��ȭ(4�� ����)
        player.SetActive(true);
        aimController.gameObject.SetActive(true);

        //�÷��̾� �ʱ� ��ġ �� ���� �߰�(1��)
        player.transform.position = spawnPosition + Vector3.up * 0.05f; //��¦ ���� ���� -> ����(0.5 -> 0.05)(7.29)
        playerScript.circle = newcircle.transform;
        playerScript.enabled = true;

        //���������� �ʱ� ���� ���� ���� �߰�(1��)
        aimController.center = player.transform;
        aimController.circle = newcircle.transform;

        //���� ���� ����(2�� ���� �� �߰�)
        playerCurrentCircle = newcircle;

        //����Ʈ�� ���(1�� �߰�)
        circles.Add(newcircle);

        // ���� �� ���� ������Ʈ(2�� ���� �� �߰�)
        prevPosition = Vector3.zero;
        prevDiameter = diameter;

        Destroy(newcircle, 10.0f); //ù ���ǵ� ������(���� �߰� �� ����)
    }
    void Gen()
    {
        float randomradius = Random.Range(minRadius, maxRadius);
        float diameter = randomradius * 2f;

        float angleDeg = Random.Range(-45f, 45f);
        float angleRad = angleDeg * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;//normalized �߰���

        // �Ÿ��� ���� ó��
        float distance = (prevDiameter + diameter) * 2.8f; // �ڵ� ����: 0.5f - > 2.8f, ��� �� ������ ���� ����(�� ������ * ����)�� �°� ����
        Vector3 spawnPosition = prevPosition + (Vector3)(direction * distance);

        GameObject newcircle = Instantiate(circle);
        newcircle.transform.SetParent(containerTransform);
        newcircle.transform.localPosition = spawnPosition;
        // ���� �ڵ�: new Vector3(4, Random.Range(-2.86f, 2.46f), 0);

        //ȸ�� �ӵ� 0�� ���� �ʵ��� ����(3�� ����)
        float speed = 0f;
        while (Mathf.Abs(speed) < 0.5f)
            speed = Random.Range(-2f, 2f);
        newcircle.GetComponent<M3_Move>().speed = speed;

        newcircle.transform.localScale = new Vector3(diameter, diameter, 1f);
        newcircle.SetActive(true);//�̰� GPT Ž��
        timer = 0;

        //����Ʈ �߰�(���� �߰���)(1��)
        circles.Add(newcircle);

        // ���� �� ���� ������Ʈ(2�� ����)
        prevPosition = spawnPosition;
        prevDiameter = diameter;

        if (playerCurrentCircle != newcircle)
            Destroy(newcircle, 10.0f);
    }
    
    //2�� ���� �߰�
    public void SetCurrentPlayerCircle(GameObject circle)
    {
        playerCurrentCircle = circle;
    }
}