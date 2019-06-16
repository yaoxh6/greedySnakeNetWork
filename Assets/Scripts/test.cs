using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        initWall();
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
