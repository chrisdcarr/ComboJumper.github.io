using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input : MonoBehaviour
{
    int input_number = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input_number = InputMovement();
    }

    public int GetInputValue()
    {
        return input_number;
    }

    public int InputMovement()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Debug.Log("pressed down");
            return -1;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.Log("pressed up");
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Debug.Log("pressed left");
            return -2;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Debug.Log("pressed right");
            return 2;
        }
        else
            return 0;
    }
}
