using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlSnake : BaseSnake
{
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            move(Direction.UP);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            move(Direction.RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            move(Direction.DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            move(Direction.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            isEat = true;
        }
    }
}
