using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using UnityEditor.Experimental.UIElements.GraphView;
using Random = UnityEngine.Random;


// AI Move

/*
public struct AiMove
{
    public int score;
    public int point;

    public AiMove(int _score, int _point)
    {
        score = _score;
        point = _point;
    }
}
*/


public class GameControllerAI : MonoBehaviour
{
    //public Text[] buttonList;
    public List<Text> buttonList = new List<Text>();
    /*public List<Text> buttonListCopy = new List<Text>();*/
    public bool[] buttonListCopy;
   
    private Text[] SelectedButtons;

    [SerializeField] private string PlayerSide, ComputerSide;


    public static GameControllerAI instance;

    //List
    public List<int> PositionList = new List<int>();
   // public List<GameObject> ButtonInteractable = new List<GameObject>();
    public List<int> PlayerWinList = new List<int>();
    public List<int> ComputerWinList = new List<int>();

    [SerializeField] private GameObject _GameOverPanel;
    [SerializeField] private GameObject _RestartButton;
    [SerializeField] private GameObject _PlayerPage;
    [SerializeField] private GameObject _TurnButton;
    [SerializeField] private GameObject _X;
    [SerializeField] private GameObject _O;

    [SerializeField] private Image _tieImage;

    [SerializeField] private Text _Turntext;
    [SerializeField] private Text _Wintext;
    [SerializeField] private Text _Xtext;
    [SerializeField] private Text _Otext;
    [SerializeField] private Text _PlayerDisplayText;
    [SerializeField] private Text _CompDisplaytext;
    [SerializeField] private Text _PlayerWinText;
    [SerializeField] private Text _CompWinText;

    [SerializeField] private int movecount;

    //public int compValue;
    private int tempvalue;
    public int index=0,idx; 
    public int ComputerPosition;
    public int tempCompValue;
    public bool Pflag=true,Cflag = true;
    //public int poslistvalue;

    [SerializeField] public float delay;
    [SerializeField] private int PlayerWin = 0, CompWin = 0;
   

    public bool PlayerTurn;
    
    
    private void Awake()
    {
        instance = this;
        _GameOverPanel.SetActive(false);
        _RestartButton.SetActive(false);

        SetGameControllerOnButtons();
        PositionList.Clear();
        AddtoPositionList();
        AddtoButtonListCopy();
        _PlayerDisplayText.text = "";
        _CompDisplaytext.text = "";

        PlayerWinList.Clear();
        ComputerWinList.Clear();
       

        Pflag = true;
       // Cflag = true;
        ComputerPosition = 0;
        PlayerTurn = true;
        PlayerSide = "";
        movecount = 0;
        delay = 5;
    }

    
    //Add to Position List
    public void AddtoPositionList()
    {
        
        // Debug.Log("* PosList length:=" + PositionList.Count);
        PositionList.Add(0);
        PositionList.Add(1);
        PositionList.Add(2);
        PositionList.Add(3);
        PositionList.Add(4);
        PositionList.Add(5);
        PositionList.Add(6);
        PositionList.Add(7);
        PositionList.Add(8);

    }

    public void AddtoButtonListCopy()
    {
        for (int i = 0; i < 9; i++)
        {
            buttonListCopy[i] = true;
        }

    }

    public void SetGameControllerOnButtons()
    {
        for (int b = 0; b < 9; b++)
        {
            buttonList[b].GetComponentInParent<GridSpaceAI>().SetGameControllerReference(this);
        }
    }

    private int winValue;

                                                                       //change
    public void ButtonNotInteractable()
    {
        if (PlayerTurn == true)               /*change here*/
        {
            for (int i = 0; i < buttonListCopy.Length; i++)
            {


                if ( /*buttonList[i].GetComponentInParent<Button>().interactable == false*/
                    buttonListCopy[i] == false)
                {
                    tempvalue = PositionList[i];
                    //   print("tempvalue=" + tempvalue);

                    print("pos value (notInteractable)==" + PositionList[i]);
                    PositionList.Remove(tempvalue);
                    buttonListCopy[tempvalue] = false;

                    //  Debug.Log("buttonNot interactable="+i);
                }

            }
        }
        else
        {
            for (int i = 0; i < PositionList.Count; i++)
            {


                if ( /*buttonList[i].GetComponentInParent<Button>().interactable == false*/
                    buttonListCopy[i] == false)
                {
                    tempvalue = PositionList[i];
                    //   print("tempvalue=" + tempvalue);

                    print("pos value (notInteractable)==" + PositionList[i]);
                    PositionList.Remove(tempvalue);
                    buttonListCopy[tempvalue] = false;

                    //  Debug.Log("buttonNot interactable="+i);
                }

            }
        }

    }

   

    //Player Win Position
    public void PlayerWinPosition(int pos)
    {
        //Corner_position
        if (pos == 0)
        {
            PlayerWinList.Add(1);
            PlayerWinList.Add(2);
            PlayerWinList.Add(3);
            PlayerWinList.Add(4);
            PlayerWinList.Add(6);
            PlayerWinList.Add(8);
        }
        else if (pos == 2)
        {
            PlayerWinList.Add(0);
            PlayerWinList.Add(1);
            PlayerWinList.Add(4);
            PlayerWinList.Add(5);
            PlayerWinList.Add(6);
            PlayerWinList.Add(8);
        }
        else if (pos == 2)
        {
            PlayerWinList.Add(0);
            PlayerWinList.Add(1);
            PlayerWinList.Add(4);
            PlayerWinList.Add(5);
            PlayerWinList.Add(6);
            PlayerWinList.Add(8);
        }
        else if (pos == 6)
        {
            PlayerWinList.Add(0);
            PlayerWinList.Add(2);
            PlayerWinList.Add(3);
            PlayerWinList.Add(4);
            PlayerWinList.Add(7);
            PlayerWinList.Add(8);
        }
        else if (pos == 8)
        {
            PlayerWinList.Add(0);
            PlayerWinList.Add(2);
            PlayerWinList.Add(4);
            PlayerWinList.Add(5);
            PlayerWinList.Add(6);
            PlayerWinList.Add(7);
        }

        //Center Postion
        else if (pos == 4)
        {
            PlayerWinList.Add(0);
            PlayerWinList.Add(1);
            PlayerWinList.Add(2);
            PlayerWinList.Add(3);
            PlayerWinList.Add(5);
            PlayerWinList.Add(6);
            PlayerWinList.Add(7);
            PlayerWinList.Add(8);
        }

        //Middle Position
        else if (pos == 1)
        {
            PlayerWinList.Add(0);
            PlayerWinList.Add(2);
            PlayerWinList.Add(4);
            PlayerWinList.Add(7);
        }
        else if (pos == 3)
        {
            PlayerWinList.Add(0);
            PlayerWinList.Add(4);
            PlayerWinList.Add(5);
            PlayerWinList.Add(6);
        }
        else if (pos == 5)
        {
            PlayerWinList.Add(2);
            PlayerWinList.Add(3);
            PlayerWinList.Add(4);
            PlayerWinList.Add(8);
        }
        else if (pos == 7)
        {
            PlayerWinList.Add(1);
            PlayerWinList.Add(4);
            PlayerWinList.Add(6);
            PlayerWinList.Add(8);
        }
    }

    //Computer Win Position
    public void ComputerWinPosition(int pos)
    {
        //Corner Position
        if (pos == 0)
        {
            ComputerWinList.Add(1);
            ComputerWinList.Add(2);
            ComputerWinList.Add(3);
            ComputerWinList.Add(4);
            ComputerWinList.Add(6);
            ComputerWinList.Add(8);
        }

        if (pos == 2)
        {
            ComputerWinList.Add(0);
            ComputerWinList.Add(1);
            ComputerWinList.Add(4);
            ComputerWinList.Add(5);
            ComputerWinList.Add(6);
            ComputerWinList.Add(8);
        }

        else if (pos == 6)
        {
            ComputerWinList.Add(0);
            ComputerWinList.Add(2);
            ComputerWinList.Add(3);
            ComputerWinList.Add(4);
            ComputerWinList.Add(7);
            ComputerWinList.Add(8);
        }

        else if (pos == 8)
        {
            ComputerWinList.Add(0);
            ComputerWinList.Add(2);
            ComputerWinList.Add(4);
            ComputerWinList.Add(5);
            ComputerWinList.Add(6);
            ComputerWinList.Add(7);
        }

        //Center Postion
        else if (pos == 4)
        {
            ComputerWinList.Add(0);
            ComputerWinList.Add(1);
            ComputerWinList.Add(2);
            ComputerWinList.Add(3);
            ComputerWinList.Add(5);
            ComputerWinList.Add(6);
            ComputerWinList.Add(7);
            ComputerWinList.Add(8);
        }

        //Middle Position
        else if (pos == 1)
        {
            ComputerWinList.Add(0);
            ComputerWinList.Add(2);
            ComputerWinList.Add(4);
            ComputerWinList.Add(7);
        }

        else if (pos == 3)
        {
            ComputerWinList.Add(0);
            ComputerWinList.Add(4);
            ComputerWinList.Add(5);
            ComputerWinList.Add(6);
        }

        else if (pos == 5)
        {
            ComputerWinList.Add(2);
            ComputerWinList.Add(3);
            ComputerWinList.Add(4);
            ComputerWinList.Add(8);
        }

        else if (pos == 7)
        {
            ComputerWinList.Add(1);
            ComputerWinList.Add(4);
            ComputerWinList.Add(6);
            ComputerWinList.Add(8);
        }

        /*for (int i = 0; i < ComputerWinList.Count; i++)
        {
            Debug.Log("ComputerWinPosition="+ComputerWinList[i].ToString());    
        }*/
    }

    //Player's Page method
    public void setPlayer(string value)
    {
        PlayerSide = _PlayerDisplayText.text = value;
        ComputerSide = _CompDisplaytext.text = (PlayerSide == "X") ? ComputerSide = "O" : ComputerSide = "X";

        StartCoroutine(disableObject(_PlayerPage));
    }

    public string GetPlayerSide()
    {
        return PlayerSide;
    }

    public string GetComputerSide()
    {
        return ComputerSide;
    }



    /*public void computerTurn()
    {
        tempCompValue = ComputeValue();
        //  Debug.Log("index after compValue="+index.ToString());

        if (buttonList[tempCompValue].GetComponentInParent<Button>().interactable == true)
        {
            buttonList[tempCompValue].text = ComputerSide;
            buttonList[tempCompValue].GetComponentInParent<Button>().interactable = false;
            EndTurn();
        }
    }*/                                                                                                         //UPDATE

        private void Update()
        {


            if (PlayerTurn == false)
            {
                
                
                delay += delay * Time.deltaTime * 2;
                if (delay >= 100)
                {
                    print("jjj");
                    //Debug.Log("index before compValue="+index.ToString());
                    tempCompValue = ComputeValue();
                    
                   // print(tempCompValue);
                   
                  //  Debug.Log("index after compValue="+index.ToString());
                    
                    if (buttonList[tempCompValue].GetComponentInParent<Button>().interactable == true)
                    {
                        buttonList[tempCompValue].text = ComputerSide;
                        buttonList[tempCompValue].GetComponentInParent<Button>().interactable = false;
                       // buttonListCopy[tempCompValue].GetComponentInParent<Button>().interactable = false;
                        EndTurn();
                        delay = 5;
                        
                    }
                    /*else
                    {
                        ComputeValue();
                    }*/
    
                }
            }

            if (movecount == 9)
            {

                if (PlayerTurn == true)
                {
                    if (PlayerWinCondition())
                    {
                        gameOverPlayer("win");
                    }
                    else
                    {
                        gameOverPlayer("draw");
                    }

                }
                //Computer
                else

                {

                    if (ComputerWinCondition())
                    {
                        gameOverComputer("win");
                    }
                    else
                    {
                        gameOverComputer("draw");
                    }
                }
            }

        }
  
    public void EndTurn()
    {
        ButtonNotInteractable();
     
        if (PlayerTurn == true)
        {

            if (Pflag == true)
            {
               
                PlayerWinPosition(tempvalue);
                
                Pflag = false;
            }
          //  print("2");
            if (PlayerWinCondition())
            {
                gameOverPlayer("win");
            }
            else
            {
               // print("3");
               PlayerTurn = false; 
                ChangeSide();
               
                
            }

           
        }
        
        //Computer
        else

        {

            if (Cflag == true)
            {
                ComputerWinPosition(tempvalue);
                Cflag = false;
            }
            
           loop: if (ComputerWinCondition())
            {
                gameOverComputer("win");
            }
            else
            {

                ChangeSide();
               
                PlayerTurn = true;
            }
           
        }
        
    }
    
    
    //Position selection for Computer AI
    public int ComputeValue()
    {

        
      print("POSITION LIST COUNT=="+PositionList.Count);
      if (Cflag == true)
      {
          index = Random.Range(0, PositionList.Count);
          idx = PositionList[index];
          print("idx Pl=="+idx);
      }
      else
      {
          index = Random.Range(0,ComputerWinList.Count);
          idx = ComputerWinList[index];
          print("idx Cwl=="+idx);
          
      }

     
          if (buttonList[idx].GetComponentInParent<Button>().interactable ==false)
          {
              ComputeValue();
          }
      

      /*if (ComputerWinCondition()==true)
      {

          if (ComputerWinList.Contains(idx))
          {
            //  print("Cwc");
              ComputerPosition = idx;
          }
          else
          {
            //  print("without Cwc");
              ComputerPosition = idx;
          }
      }
      
      else 
      {
          if (PlayerWinList.Contains(idx))
          {
              if (PlayerWinCondition()==true)
              {
                //  print("PwL");
                  ComputerPosition = idx;
              }
              else
              {
                 // print("without PwL");
                  ComputerPosition = idx;
              }
          }
      }*/
         
             return idx;
             // return ComputerPosition;
    }


    
    
public void ChangeSide()
    {
        movecount++;
        StartCoroutine(delayTurnValue(_Turntext));
    }
    
    IEnumerator delayTurnValue(Text txt)
    {
        
        yield return new WaitForSeconds(2f);
        
    }
    
    
    public void gameOverPlayer(string winningValue)
    {
        _GameOverPanel.SetActive(true);
        _RestartButton.SetActive(true);
        if (winningValue == "win")
        {
            PlayerWin++;
           _Wintext.text = "P L A Y E R\nW  I  N  S ";
       }
       else if (winningValue == "draw")
        {
           // _TurnButton.GetComponent<Image>().enabled = true;
            _tieImage.enabled = true;
            _Turntext.enabled = false;
            _Wintext.text = "  M A T C H ";
        }

        for (int i = 0; i < /*buttonList.Length*/buttonList.Count; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;

        }

        _PlayerWinText.text = PlayerWin.ToString();
    }
    
    public void gameOverComputer(string winningValue)
    {
        _GameOverPanel.SetActive(true);
        _RestartButton.SetActive(true);
        if (winningValue == "win")
        {
            CompWin++;
            _Wintext.text = " C O M P U T E R\nW  I  N  S ";
        }
        else if (winningValue == "draw")
        {
            _tieImage.enabled = true;
            _Turntext.enabled = false;
            _Wintext.text = "  M A T C H ";
        }

        for (int i = 0; i < /* buttonList.Length*/buttonList.Count; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
        _CompWinText.text = CompWin.ToString();
       
    }
    
    public void SetRestart()
    {
       Awake();
       
        _GameOverPanel.SetActive(false);
        _PlayerPage.SetActive(true);
        _tieImage.enabled = false;
        _Turntext.enabled = true;
        for (int i = 0; i < /*buttonList.Length*/buttonList.Count; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = true;
            buttonList[i].text = "";
        }

        StartCoroutine(disableObject(_RestartButton));
    }

    IEnumerator disableObject(GameObject objectMain)
    {
        yield return new WaitForSeconds(0.5f);
        objectMain.SetActive(false);
    }

    private bool val;
    public bool PlayerWinCondition()
    {
        if (//ROW
            (buttonList[0].text == PlayerSide && buttonList[1].text == PlayerSide &&
             buttonList[2].text == PlayerSide) ||
            (buttonList[3].text == PlayerSide && buttonList[4].text == PlayerSide &&
             buttonList[5].text == PlayerSide) ||
            (buttonList[6].text == PlayerSide && buttonList[7].text == PlayerSide &&
             buttonList[8].text == PlayerSide) ||
            //COLUMN
            (buttonList[0].text == PlayerSide && buttonList[3].text == PlayerSide &&
             buttonList[6].text == PlayerSide) ||
            (buttonList[1].text == PlayerSide && buttonList[4].text == PlayerSide &&
             buttonList[7].text == PlayerSide) ||
            (buttonList[2].text == PlayerSide && buttonList[5].text == PlayerSide &&
             buttonList[8].text == PlayerSide) ||
            //DIAGONAL
            (buttonList[0].text == PlayerSide && buttonList[4].text == PlayerSide &&
             buttonList[8].text == PlayerSide) ||
            (buttonList[2].text == PlayerSide && buttonList[4].text == PlayerSide &&
             buttonList[6].text == PlayerSide))
        {
            val= true;
        }
        else
        {
            val = false;
        }

        return val;
    }
    
    public bool ComputerWinCondition()
    {
        if (//ROW
            (buttonList[0].text == ComputerSide && buttonList[1].text == ComputerSide &&
              buttonList[2].text == ComputerSide) ||
             (buttonList[3].text == ComputerSide && buttonList[4].text == ComputerSide &&
              buttonList[5].text == ComputerSide) ||
             (buttonList[6].text == ComputerSide && buttonList[7].text == ComputerSide &&
              buttonList[8].text == ComputerSide) ||
            //COLUMN
             (buttonList[0].text == ComputerSide && buttonList[3].text == ComputerSide &&
              buttonList[6].text == ComputerSide) ||
             (buttonList[1].text == ComputerSide && buttonList[4].text == ComputerSide &&
              buttonList[7].text == ComputerSide) ||
             (buttonList[2].text == ComputerSide && buttonList[5].text == ComputerSide &&
              buttonList[8].text == ComputerSide) ||
            //DIAGONAL
             (buttonList[0].text == ComputerSide && buttonList[4].text == ComputerSide &&
              buttonList[8].text == ComputerSide) ||
             (buttonList[2].text == ComputerSide && buttonList[4].text == ComputerSide &&
              buttonList[6].text == ComputerSide))
        {
            val= true;
        }
        else
        {
            val = false;
        }

        return val;
    }
    

} 
