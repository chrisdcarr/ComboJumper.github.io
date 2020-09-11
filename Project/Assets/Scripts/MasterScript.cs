using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterScript : MonoBehaviour
{
    protected Text score;
    int combo_size = 3;

    private void Awake()
    {
        score = GameObject.Find("Score").GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Level(score.text);
    }

    public int GetScore()
    {
        
        //Debug.Log(score.text.ToString());
        return int.Parse( score.text);
        //return 2;
    }
    public void IncreaseScore()
    {
        score.text = (int.Parse(score.text)+1).ToString();
    }

    
    public int GetComboSize()
    {
        return combo_size;
    }

    void Level(string score)
    {
        switch (score)
        {
            case "15":
                combo_size = 4;
                break;
            case "30":
                combo_size = 5;
                break;
            case "50":
                combo_size = 6;
                break;
            case "70":
                combo_size = 7;
                break;
            case "100":
                combo_size = 8;
                break;
        }
    }

    public void GameOver()
    {
        gameObject.GetComponent<SceneManagement>().MainMenu();
    }
}
