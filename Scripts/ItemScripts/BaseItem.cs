using System.Linq;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public float fallSpeed = 3f; // �������� �ӵ�

    void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // �������� ���� y ��ġ�� -5.5���� ������ ����
        if (transform.position.y < -5.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paddle")) // �е�� �浹���� ��
        {
            GameObject nearestPaddle = FindNearestPaddle(transform.position); // ��ġ �񱳷� ȿ���� Paddle���� ����
            ApplyEffect(nearestPaddle);
            Destroy(gameObject);
        }
    }

    public virtual void ApplyEffect(GameObject player)
    {
        // ���� ��� �߰�����
    }

    private GameObject FindNearestPaddle(Vector2 itemPosition)
    {
        // Paddle �±� ������Ʈ ��� �迭�� �߰�
        GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");

        // Paddle���� �Ÿ��� ������ ������ �����ϰ� ���� ����� �� ������
        GameObject nearestPaddle = paddles.OrderBy(paddle => Vector2.Distance(itemPosition, (Vector2)paddle.transform.position)).FirstOrDefault();

        return nearestPaddle;
    }
}
