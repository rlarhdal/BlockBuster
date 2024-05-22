using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBlock
{
    [SerializeField] int hp;  //벽돌 내구도 



    public GameObject[] itemPrefabs;

    private SpriteRenderer SpriteRenderer;
    public Sprite hidBlock;

    private System.Random rand = new System.Random();


    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage() //공과 충돌시 호출될 메소드
    {
        hp -= 1;

        //만약에 hp가 0이아니면 깨지면 프리펩 변경 
        if (hp != 0)
        {
            SpriteRenderer.sprite = hidBlock;
        }
        else if (hp <= 0)
        {
            Death();
            if (rand.Next(100) < 10) SpawnItem();
        }


    }

    private void SpawnItem()
    {
        int randomValue = rand.Next(1, 101);

        GameObject itemPrefab = null;

        if (randomValue <= 30)
        {
            itemPrefab = itemPrefabs[0];
        }
        else if (randomValue <= 60)
        {
            itemPrefab = itemPrefabs[1];
        }
        else if (randomValue <= 90)
        {
            itemPrefab = itemPrefabs[2];
        }
        else if (randomValue <= 100)
        {
            itemPrefab = itemPrefabs[3];
        }

        // 아이템 스폰 위치 설정 (현재 블록의 위치로 설정)
        Vector2 spawnPosition = transform.position;

        // 선택된 아이템을 스폰 위치에 생성
        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }

    public void Death()
    {
        Destroy(gameObject);

        GameManager.instance.currentScore += 100;

        GameManager.instance.blockCnt--;
        GameManager.instance.CheckBlockCnt();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.instance.PlayeSfx("Bounce_wall");
        if (collision.gameObject.CompareTag("Ball"))  //collision 충돌받은것 볼
        {
            TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Beam"))
        {
            TakeDamage();
        }
    }

}
