using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Boss : MonoBehaviour, IBlock
{
    private BossAnimationController anim;
    private Movement movement;
    private ObjectPool objPool;
    private Rigidbody2D rigid;
    private CapsuleCollider2D capsuleCollider;

    public GameObject[] players;

    [Header("# Boss")]
    [SerializeField] private int bossMaxHp = 10;
    [SerializeField] private int bossCurrentHP;
    private float speed = 2f;

    [Header("# Bullet")]
    private float maxShotDelay = 1f;
    private float currentShotDelay;
    [SerializeField] private Transform bulletPos;

    bool isAlive = true;
    bool isSpawn = true;

    private void Awake()
    {
        objPool = GetComponent<ObjectPool>();
        anim = GetComponent<BossAnimationController>();
        movement = GetComponent<Movement>();
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        bossCurrentHP = bossMaxHp;
    }

    private void Start()
    {
        //등장씬
        players = GameManager.instance.players;
    }

    private void Update()
    {
        if (isSpawn)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            if (transform.position.y < 2f)
            {
                isSpawn = false;
                capsuleCollider.isTrigger = false;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }

            StartCoroutine(WaitForSec());
        }
        else
        {
            Reload();
            Attack();
        }

    }


    private void Reload()
    {
        currentShotDelay += Time.deltaTime;
    }

    private void Attack()
    {
        // 보스가 살아있는지 확인
        if (!isAlive) return;
        if (currentShotDelay < maxShotDelay) return;

        anim.Attack();

        GameObject bullet = objPool.SpawnFromPool("Bullet");
        bullet.transform.position = bulletPos.position;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        // 공격 대상 지정
        int index = Random.Range(0, players.Length);
        Vector3 dirVec = players[index].transform.position - transform.position; // 바라보는 방향

        // 총알 이동
        rigid.AddForce(dirVec * 0.5f, ForceMode2D.Impulse);

        currentShotDelay = 0;
    }


    public void TakeDamage()
    {
        bossCurrentHP -= 1;

        anim.Hit();

        if (bossCurrentHP == 0) Death();
    }

    public void Death()
    {
        SoundManager.instance.bgmPlayer.Stop();
        SoundManager.instance.PlayeSfx("Victory");
        isAlive = false;
        anim.Dead();
        Invoke("DestroyGameObject", 1f);
        GameManager.instance.currentScore += 500;
        GameManager.instance.ClearGame();
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.instance.PlayeSfx("BossHit");
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage();
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1f);
    }
}
