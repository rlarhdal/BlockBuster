using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BlcokSpawnController : MonoBehaviour
{
    public static BlcokSpawnController instance;

    private SoundManager soundManager;

    [SerializeField] GameObject block; //���� ����
    [SerializeField] Transform blockParent;

    [SerializeField] Vector2 pos;   //���� ���� ��ġ 
    [SerializeField] Vector2 offset; //���� ����

    [SerializeField] int row;   // ��
    [SerializeField] int col;   //��

    [SerializeField] private GameObject bossPrefab; //���� ������


    Vector2 Lv_1;
    Vector2 Lv_2;
    Vector2 Lv_3;

    [SerializeField] GameObject[] Blocks;  

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        soundManager = SoundManager.instance;


    }


    void Start()
    {
        blockParent = this.transform;
        Lv_1 =new Vector2(0,4); // 0~4 ���� �������ִ� �ڵ带�������Ѵ� 
        Lv_2 = new Vector2(0, 7);
        Lv_3 = new Vector2(4,7);
        CreateBlocks();

    }

    public void MakeBlock(Vector2 num)
    {

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                //Instantiate(Resources.Load<GameObject>($"Block{Random.Range(0,num)}"), new Vector3(pos.x + (j * offset.x), pos.y + (i * offset.y), (0.1f * i) + 0.01f * j), Quaternion.identity, blockParent);
                Instantiate(Blocks[Random.Range((int)num.x,(int)num.y)], new Vector3(pos.x + (j * offset.x), pos.y + (i * offset.y), (0.1f * i) + 0.01f * j), Quaternion.identity, blockParent);

                GameManager.instance.blockCnt++;
            }
        }
    }

    


    public void CreateBlocks()
    {


        if (GameManager.sceneVariable.level == 1)
        {
            MakeBlock(Lv_1);
        }
        else if (GameManager.sceneVariable.level == 2)
        {
            MakeBlock(Lv_2);

        }
        else if (GameManager.sceneVariable.level == 3)
        {
            MakeBlock(Lv_3);

        }

    }

    public void SpawnBoss()
    {
        // ���� ���� ���
        soundManager.ChangeBgm(BgmType.Boss);

        // ���� ����
        Instantiate(bossPrefab, new Vector3(0f, 6f, 0f), Quaternion.identity, blockParent);
        //Boss boss = bossObj.GetComponent<Boss>();

        // ���� ����
        //boss.ChangeState(BossState.MoveToAppearPoint);
    }
}
