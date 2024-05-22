using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PaddleController : MonoBehaviour
{
    [SerializeField] int hp = 3;
    [SerializeField] SpriteRenderer spriteRenderer;
    private Sprite damagedSprite1;
    private Sprite damagedSprite2;

    public List<Sprite> sprites = new List<Sprite>();

    public static PaddleController instance;
    public float paddleSpeed = 5f;
    public GameObject ballPrefab;

    private bool isPositionInitialized = false; // 위치가 초기화 됐는지 확인

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        damagedSprite1 = sprites[0];
        damagedSprite2 = sprites[1];
    }

    void Update()
    {
        float horizontalInput = 0f;
        bool isPaddle1 = gameObject.name == "Paddle1";
        bool isPaddle2 = gameObject.name == "Paddle2";
        int playerMode = PlayerPrefs.GetInt("PlayerMode");
        GameObject paddle2 = GameObject.Find("Paddle2");

        if (isPaddle1)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal_Paddle1"); // Paddle1 조작
        }

        if (playerMode == 1) // 1인 플레이 모드
        {
            if (paddle2 != null) paddle2.SetActive(false); // Paddle2 비활성화

            if (isPaddle1 && !isPositionInitialized) // Paddle1 위치 초기화
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
                isPositionInitialized = true;
            }
        }

        if (playerMode == 2 && isPaddle2) // 2인 플레이 모드
        {
            horizontalInput = Input.GetAxisRaw("Horizontal_Paddle2"); // Paddle2 조작
        }

        if (Time.timeScale == 0f) return; // 일시 정지 시 멈춤

        Vector2 movement = new Vector2(horizontalInput, 0f) * paddleSpeed * Time.deltaTime;
        transform.Translate(movement);

        float clampMin = -2.45f, clampMax = 2.45f;
        if (transform.localScale.x == 2.25f)
        {
            clampMin = -2.2f;
            clampMax = 2.2f;
        }
        else if (transform.localScale.x == 1f)
        {
            clampMin = -2.6f;
            clampMax = 2.6f;
        }

        float clampedX = Mathf.Clamp(transform.position.x, clampMin, clampMax);
        transform.position = new Vector2(clampedX, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 Bullet 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
            collision.gameObject.SetActive(false);
        }
    }

    public void TakeDamage()
    {
        hp -= 1;
        if (hp <= 0)
        {
            // 게임오버 화면 띄우기

            Destroy(this.gameObject);
        }
        else if (hp == 2)
        {
            spriteRenderer.sprite = damagedSprite1;

        }
        else if (hp == 1)
        {
            spriteRenderer.sprite = damagedSprite2;
        }
    }

    public void ExtendPaddle(float x)
    {
        if (transform.localScale.x != 2.25f) // 이미 크기가 1.5배가 아니라면
        {
            Vector2 currentScale = transform.localScale;
            currentScale.x *= x;
            transform.localScale = currentScale;
        }
    }

    public void ShrinkPaddle(float x)
    {
        if (transform.localScale.x != 1f)
        {
            Vector2 currentScale = transform.localScale;
            currentScale.x /= x;
            transform.localScale = currentScale;
        }
    }

    public void IncreaseBallCount()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        int ballCount = balls.Length;

        if (ballCount >= 3) return;

        Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            SoundManager.instance.PlayeSfx("Bouncel_paddle");
        }
    }
}