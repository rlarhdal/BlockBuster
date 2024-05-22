using System.Linq;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public float fallSpeed = 3f; // 떨어지는 속도

    void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // 아이템의 현재 y 위치가 -5.5보다 작으면 삭제
        if (transform.position.y < -5.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paddle")) // 패들과 충돌했을 때
        {
            GameObject nearestPaddle = FindNearestPaddle(transform.position); // 위치 비교로 효과를 Paddle에만 적용
            ApplyEffect(nearestPaddle);
            Destroy(gameObject);
        }
    }

    public virtual void ApplyEffect(GameObject player)
    {
        // 공통 기능 추가가능
    }

    private GameObject FindNearestPaddle(Vector2 itemPosition)
    {
        // Paddle 태그 오브젝트 모두 배열에 추가
        GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");

        // Paddle과의 거리를 오름차 순으로 나열하고 가장 가까운 걸 가져옴
        GameObject nearestPaddle = paddles.OrderBy(paddle => Vector2.Distance(itemPosition, (Vector2)paddle.transform.position)).FirstOrDefault();

        return nearestPaddle;
    }
}
