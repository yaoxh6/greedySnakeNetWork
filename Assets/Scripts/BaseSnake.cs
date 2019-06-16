using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction{
    UP,
    RIGHT,
    DOWN,
    LEFT
}


public class BaseSnake : MonoBehaviour{
    private GameObject snakeHead;
    public List<GameObject> snake;
    public bool isDead;
    public bool isEat;
    private Vector3 newBodyPos;
    private Vector3 lastHeadPos;
    public int randomID;
    void Start()
    {
     //   randomID = (int)Random.Range(-4, 4);
    }

    public void init(int randomID)
    {
    //    Debug.Log("@@@" + randomID);
        this.randomID = randomID;
        snakeHead = Instantiate(Resources.Load("Prefabs/BlueSnake", typeof(GameObject)), new Vector3(randomID, 1, randomID), Quaternion.identity, null) as GameObject;
        snakeHead.transform.parent = this.transform;//使得其父物体为自身
        snake = new List<GameObject>();
        snake.Add(snakeHead);
        isDead = false;
        isEat = false;
   
        newBodyPos = new Vector3(randomID, 1, randomID);
        lastHeadPos = new Vector3(randomID, 1, randomID);
    }

    public void move(Direction inputDirection)
    {
        if (isDead)
        {
            return;
        }
        lastHeadPos = snake[0].transform.position;
        if (inputDirection == Direction.UP)
        {
            snakeHead.transform.Translate(new Vector3(0, 0, 1.0f));
        }
        else if(inputDirection == Direction.RIGHT)
        {
            snakeHead.transform.Translate(new Vector3(1.0f, 0, 0));
        }
        else if(inputDirection == Direction.DOWN)
        {
            snakeHead.transform.Translate(new Vector3(0, 0, -1.0f));
        }
        else if(inputDirection == Direction.LEFT)
        {
            snakeHead.transform.Translate(new Vector3(-1.0f, 0, 0));
        }

        for (int i = 1; i < snake.Count; i++)
        {
            Vector3 tempPos = snake[i].transform.position;
            snake[i].transform.position = lastHeadPos;
            lastHeadPos = tempPos;
        }

        if (isEat)
        {
            grow();
            isEat = false;
        }
        lastHeadPos = snake[0].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("yess");
        if (other.gameObject.tag == "food")
        {

        }
        else if(other.gameObject.tag == "wall")
        {
            isDead = true;
            Debug.Log(this.gameObject.name + " isDead " + isDead);
        }
    }

    public void Eat(GameObject other)
    {
        isEat = true;
        //   Destroy(other.gameObject);
        //   generateFood();
        SendFood();//报告新的食物
        Debug.Log(this.gameObject.name + " isEat " + isEat);
    }
    public void grow()
    {
        if (isDead)
        {
            return;
        }
        GameObject snakeBodyTemp = Instantiate(Resources.Load("Prefabs/BlueSnake", typeof(GameObject)), lastHeadPos, Quaternion.identity, null) as GameObject;
        snakeBodyTemp.transform.parent = this.transform;
        snake.Add(snakeBodyTemp);
    }

    public void generateFood(Vector3Int newPos)
    {
        Debug.Log("InMove " + isDead + isEat);
        foreach(var i in GameObject.FindGameObjectsWithTag("food"))
        {
            Destroy(i);//毁掉上一个食物
        }

        GameObject tmpFood = Instantiate(Resources.Load("Prefabs/Food", typeof(GameObject)), newPos, Quaternion.identity, null) as GameObject;

    }
    public void SendFood()
    {
        MsgFood food = new MsgFood();
        Vector3Int newPos = new Vector3Int((int)Random.Range(-4, 4), 1, (int)Random.Range(-4, 4));
        food.x = newPos.x;
        food.y = newPos.y;
        food.z = newPos.z;
        NetManager.Send(food);//向服务器发送这条消息
    }
}
