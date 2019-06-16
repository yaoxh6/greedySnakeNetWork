using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public CtrlSnake con;
    void Start()
    {
        GameObject player = new GameObject("play1");
        CtrlSnake ctrlSnake = player.AddComponent<CtrlSnake>();
        ctrlSnake.init();
        con = ctrlSnake;
        initWall();
    }


    public void Connect()
    {
        NetManager.Connect("127.0.0.1", 8888);
    }

    public void SendData()
    {
        MsgMove test = new MsgMove();
        test.x = 4;
        test.y = 2;
        test.z = 3;
        NetManager.Send(test);

    }
    void initWall()
    {
        for(int i = -5; i <= 5; i++)
        {
            for(int j = -5; j <= 5; j++)
            {
                if(i == -5 || j == -5 || i ==5 || j == 5)
                {
                    GameObject wall = Instantiate(Resources.Load("Prefabs/Wall", typeof(GameObject)), new Vector3(i, 1, j), Quaternion.identity, null) as GameObject;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
    
    }
}
