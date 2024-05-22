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

    private bool isPositionInitialized = false; // ��ġ�� �ʱ�ȭ �ƴ��� Ȯ��

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
            horizontalInput = Input.GetAxisRaw("Horizontal_Paddle1"); // Paddle1 ����
        }

        if (playerMode == 1) // 1�� �÷��� ���
        {
            if (paddle2 != null) paddle2.SetActive(false); // Paddle2 ��Ȱ��ȭ

            if (isPaddle1 && !isPositionInitialized) // Paddle1 ��ġ �ʱ�ȭ
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
                isPositionInitialized = true;
            }
        }

        if (playerMode == 2 && isPaddle2) // 2�� �÷��� ���
        {
            horizontalInput = Input.GetAxisRaw("Horizontal_Paddle2"); // Paddle2 ����
        }

        if (Time.timeScale == 0f) return; // �Ͻ� ���� �� ����

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
        // �浹�� ������Ʈ�� Bullet �±׸� ������ �ִ��� Ȯ��
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
            // ���ӿ��� ȭ�� ����

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
        if (transform.localScale.x != 2.25f) // �̹� ũ�Ⱑ 1.5�谡 �ƴ϶��
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