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

    [SerializeField] GameObject block; //벽돌 생성
    [SerializeField] Transform blockParent;

    [SerializeField] Vector2 pos;   //벽돌 생성 위치 
    [SerializeField] Vector2 offset; //벽돌 간격

    [SerializeField] int row;   // 행
    [SerializeField] int col;   //열

    [SerializeField] private GameObject bossPrefab; //보스 프리팹


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
        Lv_1 =new Vector2(0,4); // 0~4 까지 뽑을수있는 코드를만들어야한다 
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
        // 보스 음악 재생
        soundManager.ChangeBgm(BgmType.Boss);

        // 보스 생성
        Instantiate(bossPrefab, new Vector3(0f, 6f, 0f), Quaternion.identity, blockParent);
        //Boss boss = bossObj.GetComponent<Boss>();

        // 보스 등장
        //boss.ChangeState(BossState.MoveToAppearPoint);
    }
}
