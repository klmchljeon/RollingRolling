using UnityEngine;

public class M2_MoveAim : MonoBehaviour
{
    public Transform center;
    private const float radius = 2f;
    public float speed = 50f;
    public float speedIncreaseAmount = 10f;
    public float maxSpeed = 200f;

    public float aimangle = 0f;
    public bool isClockwise = false;

    private bool isMoving = true;
    void Update()
    {
        if (!isMoving || center == null) return;

        float direction = isClockwise ? -1f : 1f;
        aimangle += direction * speed * Time.deltaTime;
        aimangle = (aimangle + 360f) % 360f;

        float aimrad = aimangle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(aimrad), Mathf.Sin(aimrad), 0f) * radius;
        transform.position = center.position + offset;
    }

    // ���� ���� ���� 5���帶�� �ӵ� ������Ű�� �Լ�
    public void IncreaseSpeedByRound(int round)
    {
        if (round % 5 == 0)
        {
            speed = Mathf.Min(speed + speedIncreaseAmount, maxSpeed);
            Debug.Log($"���� {round}: �ӵ� ����! ���� �ӵ� = {speed}");
        }
    }

    public void ToggleDirection()
    {
        isClockwise = !isClockwise;
    }

    public float GetCurrentAngle()
    {
        return aimangle;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}
