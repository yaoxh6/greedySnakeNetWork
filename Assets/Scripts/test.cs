using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public CtrlSnake con;
    public CtrlSnake others;//对方操控的蛇
    void Start()
    {
        NetManager.AddMsgListener("MsgMove", OnMsgMoveReceived);
        NetManager.AddMsgListener("MsgInit", OnMsgInitReceived);
        NetManager.AddMsgListener("MsgFood", OnMsgFoodReceived);

        initWall();
    }
    void OnMsgFoodReceived(MsgBase msg)
    {
        MsgFood received = (MsgFood)msg;//得到食物的更新许可
        Vector3Int newPos = new Vector3Int(received.x, received.y, received.z);
        con.generateFood(newPos);
    }

    void OnMsgInitReceived(MsgBase msg)
    {
        MsgInit received = (MsgInit)msg;
        Debug.Log(received.x + "在位置初始化");
        InitOthers(received.x);//初始化一个敌人
    }
    public void InitMyself()
    {
        GameObject player = new GameObject("play1");
        CtrlSnake ctrlSnake = player.AddComponent<CtrlSnake>();
     //   ctrlSnake.randomID = ;
        ctrlSnake.init(Random.Range(-4, 4));
        con = ctrlSnake;
        con.underControl = true;
        Debug.Log(con.underControl);
        SendInit();//使得对方也能初始化
    }
    void InitOthers(int random)
    {
        GameObject player = new GameObject("play2");
        CtrlSnake ctrlSnake = player.AddComponent<CtrlSnake>();
     //   ctrlSnake.randomID = random;//修改地点
        ctrlSnake.init(random);
        others = ctrlSnake;
        others.underControl = false;
    }
    void OnMsgMoveReceived(MsgBase msg)
    {
        MsgMove received = (MsgMove)msg;

          Debug.Log(received.x);
          Debug.Log(received.y);
          Debug.Log(received.z);
        if (received.x == con.randomID)
        {
            if (received.y == 1)
            {
                con.move(Direction.UP);
            }
            if (received.y == 2)
            {
                con.move(Direction.RIGHT);
            }
            if (received.y == 3)
            {
                con.move(Direction.DOWN);
            }
            if (received.y == 4)
            {
                con.move(Direction.LEFT);
            }
        }else if (received.x == others.randomID)
        {
            if (received.y == 1)
            {
                others.move(Direction.UP);
            }
            if (received.y == 2)
            {
                others.move(Direction.RIGHT);
            }
            if (received.y == 3)
            {
                others.move(Direction.DOWN);
            }
            if (received.y == 4)
            {
                others.move(Direction.LEFT);
            }
        }

    }

    public void Connect()
    {
        NetManager.Connect("127.0.0.1", 8888);
    }
    public void SendInit()
    {
        MsgInit init = new MsgInit();
        init.x = con.randomID;
        init.y = 1;
        init.z = con.randomID;
        NetManager.Send(init);//向服务器发送这条消息
    }
    public void SendData()
    {
        MsgMove test = new MsgMove();
        test.x = con.randomID;
        test.y = con.keycodes[0];
        con.keycodes.RemoveAt(0);
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
        NetManager.Update();
        if (con!=null&&con.keycodes.Count != 0)
        {
            SendData();
        }
    }
}
