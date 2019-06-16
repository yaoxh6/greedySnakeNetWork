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

    public void init()
    {
        snakeHead = Instantiate(Resources.Load("Prefabs/BlueSnake", typeof(GameObject)), new Vector3(0, 1, 0), Quaternion.identity, null) as GameObject;
        snake = new List<GameObject>();
        snake.Add(snakeHead);
        isDead = false;
        isEat = false;
        newBodyPos = new Vector3(0, 1, 0);
        lastHeadPos = new Vector3(0, 1, 0);
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
        if (other.gameObject.tag == "food")
        {
            isEat = true;
            Destroy(other.gameObject);
            generateFood();
            Debug.Log(this.gameObject.name + " isEat " + isEat);
        }
        else if(other.gameObject.tag == "wall")
        {
            isDead = true;
            Debug.Log(this.gameObject.name + " isDead " + isDead);
        }
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

    private void generateFood()
    {
        Debug.Log("InMove " + isDead + isEat);
        Vector3 newPos = new Vector3((int)Random.Range(-4, 4), 1, (int)Random.Range(-4, 4));
        GameObject tmpFood = Instantiate(Resources.Load("Prefabs/Food", typeof(GameObject)), newPos, Quaternion.identity, null) as GameObject;
    }
}
