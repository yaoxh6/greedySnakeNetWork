using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlSnake : BaseSnake
{
    public bool underControl = false;
    public List<int> keycodes;
    void Awake()
    {
     //   underControl = false;
        keycodes = new List<int>();
    }
    void Update()
    {
        MoveUpdate();
    }

    public void MoveUpdate()
    {
        if (isDead)
        {
            return;
        }

        if (underControl)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                keycodes.Add(1);//move(Direction.UP);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                keycodes.Add(2);// move(Direction.RIGHT);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                keycodes.Add(3);// move(Direction.DOWN);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                keycodes.Add(4);// move(Direction.LEFT);
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                isEat = true;
            }
        }

    }
}
