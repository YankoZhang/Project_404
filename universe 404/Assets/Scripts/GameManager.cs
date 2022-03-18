using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;




    public Text txt_Shard;
    public GameObject Player;
    public GameObject AI;

    //3D场景计数器
    public float timer = 0;
    private float currentTime = 30f;
    public Text txt_Timer;
    public GameObject Flowchart_over;

    /// <summary>
    /// 上一个检查点的 UniqueId
    /// </summary>
    public string LastCheckpoint = "";

    /// <summary>
    /// 所有已经收集的碎片，包括已经用掉的
    /// </summary>
    public List<string> CollectedShards;

    /// <summary>
    /// 当前可以使用的碎片数量
    /// </summary>
    public int ShardCount;


    private void Start()
    {
        //Player.transform.position = pos.position;
    }

    private void Awake()
    {
        // 不允许多个 manager 存在
        //if (instance != null)
        //{
          //  Destroy(gameObject);
            //return;
        //}

        // manager 重置场景时保存（存储碎片/检查点信息）
        instance = this;
        DontDestroyOnLoad(gameObject);

        // TODO: 重置场景后分配不同物体参数（reference 重置）
    }

    // Update is called once per frame
    void Update()
    {
        txt_Shard.text = "Shard:" + ShardCount;
        
        if (ShardCount == 3)
        {
            AI.SetActive(true);
        }
        //30秒倒计时后激活传送对话
        currentTime -= Time.deltaTime;
        if (currentTime <= timer)
        {
            Cursor.lockState = CursorLockMode.None;
            Flowchart_over.SetActive(true);
            currentTime = 0;
        }
        txt_Timer.text = "Time:" + Math.Round(currentTime, 1);
    } 

    public void SetCanMoveToTrue()
    {
       Player.GetComponent<PlayerMovement>().canMove = true;
    }

    public void SetCanMoveToFalse()
    {
        Player.GetComponent<PlayerMovement>().canMove = false;
    }

    public void To2Dlevel()
    {
        SceneManager.LoadScene(0);
    }

    public void CanTransfer()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 收集一个碎片。
    /// </summary>
    /// <param name="shard">碎片类</param>
    public void CollectShard(Shard shard)
    {
        var id = shard.GetComponent<UniqueId>();

        // 不能重复收集碎片
        if (CollectedShards.Contains(id.uniqueId))
            return;

        CollectedShards.Add(id.uniqueId);
        ShardCount++;
    }

    /// <summary>
    /// 重置当前场景。玩家的位置会设置在上一个存档点（如果有）。
    /// </summary>
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void OnLevelWasLoaded(int level)
    {
        if (LastCheckpoint != null)
        {
            foreach (Checkpoint cp in FindObjectsOfType<Checkpoint>())
            {
                if (cp.GetComponent<UniqueId>().uniqueId == LastCheckpoint)
                {
                    GameObject.FindGameObjectWithTag("Player").transform.position = cp.transform.position;
                    break;
                }    
                    
            }
        }

        foreach (Shard shard in FindObjectsOfType<Shard>())
        {
            if (CollectedShards.Contains(shard.GetComponent<UniqueId>().uniqueId))
                shard.SetCollected(true);
        }
    }
}
