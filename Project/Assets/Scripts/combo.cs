using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combo : MonoBehaviour
{
    GameObject game_master;
    Transform player;
    //original combo list
    public Sprite[] arrows = new Sprite[4];
    //combo list that shows when you are entering an input
    public Sprite[] arrows_selected = new Sprite[4];
    //array to hold the list for the platforms
    int[] combo_list;
    //the next input for the combo to be entered
    int next_num;
    //the current index for checking
    int combo_index = 0;
    //the index to change for the gui
    int next_index = 0;
    //checking that input currect so far
    bool correct_so_far;
    // Start is called before the first frame update
    bool first_run = true;
    
    void Start()
    {
        //get the player
        player = GameObject.Find("Player").GetComponent<Transform>();
        game_master = GameObject.Find("game master");
        
        if(first_run)
        {
            //randomly generate the list of inputs to be entered
            GetComboList();
            //draw the inputs for user to see 
            DrawComboList();
        }
        
        //sets the next number to check for as the first index 
        next_num = combo_list[combo_index];
    }

    private void OnEnable()
    {
        if(!first_run)
        {
            //randomly generate the list of inputs to be entered
            GetComboList();
            //draw the inputs for user to see 
            DrawComboList();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        //only if over player
        //only check if within the screen bounds
        
        if(player.position.y < transform.position.y)
        {
            if(transform.position.y<Camera.main.transform.position.y+Camera.main.orthographicSize)
            {
                if (combo_index == 0)
                    CheckPlayerInput();
                else if (correct_so_far)
                    CheckPlayerInput();
                else
                {

                    //reset the values and set correc to false

                    next_index = 0;
                    combo_index = 0;
                    next_num = combo_list[combo_index];
                    DrawComboList();
                }
                    
            }
            
        }
        
        
    }

    void GetComboList()
    {
        //for level one just do about 3 combos
        combo_list = new int[game_master.GetComponent<MasterScript>().GetComboSize()];
        
        for(int i=0; i<combo_list.Length;i++)
        {
            combo_list[i] = Random.Range(0, 4);
        }
    }

    void DrawComboList()
    {
        for (int i = 0; i < combo_list.Length; i++)
        {
            this.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = arrows[combo_list[i]];
        }

        first_run = false;
    }

    void CheckPlayerInput()
    {
        //number being input
        int cur = 0;
        //the number that relates to how the inputs are in combo
        int actual_dir = 0;
        //gets input number from the game master
        cur = GameObject.Find("game master").GetComponent<input>().GetInputValue();

        if(cur!=0)
        {
            /*
            Debug.Log("cur = "+cur);
            Debug.Log("next = " + next_num);
            Debug.Log("array next =" + combo_list[0]);
            */
            switch(cur)
            {
                case -1:
                    actual_dir = 1;
                    break;
                case 1:
                    actual_dir = 0;
                    break;
                case -2:
                    actual_dir = 3;
                    break;
                case 2:
                    actual_dir = 2;
                    break;
                
            }

            if (actual_dir == next_num)
            {
                correct_so_far = true;
                Change(next_num);
                
            }
            
            else /*if(actual_dir != 0 && actual_dir != next_num)*/
            {
                correct_so_far = false;
            }
                
        }
        

        
    }

    void Change(int num)
    {
        this.gameObject.transform.GetChild(next_index).GetComponent<SpriteRenderer>().sprite = arrows_selected[num];
        if(combo_index<combo_list.Length-1)
        {
            next_index++;
            combo_index++;
            next_num = combo_list[combo_index];
        }
        else if(combo_index==combo_list.Length-1)
        {
            //Debug.Log("execute jump!");

            //Debug.Log("jumping to " + GetComponentInParent<Collider2D>().transform.position);
            player.GetComponent<Player>().Jump(GetComponentInParent<Collider2D>().transform.position);
            game_master.GetComponent<MasterScript>().IncreaseScore();
            
        }
       
    }

}
