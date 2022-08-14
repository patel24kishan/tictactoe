using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Game_Controller1 : MonoBehaviour
{
    [FormerlySerializedAs("buttonList")] [SerializeField] private Text[] buttonList1;
    [SerializeField] private Text[] SelectedButtons;
    
    [SerializeField] private string PlayerSide,ComputerSide;
    
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
    [SerializeField] private Text _Player2Displaytext;
    [SerializeField] private Text _PlayerWinText;
    [SerializeField] private Text _Player2WinText;
    
    [SerializeField] private int movecount;
    [SerializeField] private int PlayerWin=0, Player2Win=0;

     public  bool PlayerTurn;
     public string temp;

    private void Awake()
    {
        _GameOverPanel.SetActive(false);
        _RestartButton.SetActive(false);
        SetGameControllerOnButtons();
       
        PlayerTurn = true;
        PlayerSide = "";
        movecount = 0;

        _PlayerDisplayText.text = "";
        _Player2Displaytext.text = "";
    }

    void SetGameControllerOnButtons()
    {
        for (int i = 0; i < 9; i++)
        {
            buttonList1[i].GetComponentInParent<GridspacePvP>().SetGameControllerReference(this);
        }
    }

    public void setPlayer(string value)
    {
       Temp = value;
        PlayerSide = value;
        _PlayerDisplayText.text = value;
        _Turntext.text = "<<";
        _Player2Displaytext.text=(value == "X") ? "O" :"X";
        StartCoroutine(disableObject(_PlayerPage));
    }

    public string Temp { get; set; }

    public string GetPlayerSide()
    {
        return PlayerSide;
    }
    
    private void Update()
    {
       
      
      
        if (movecount == 9)
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
    }

    
   
    public void EndTurn()
    {
        //Player
        
       
           
            if (PlayerWinCondition())
            {
                gameOverPlayer("win");
            }
            else
            {
                ChangeSide();
               
            }
        
    }

    public void ChangeSide()
    {
        movecount++;
        PlayerSide = (PlayerSide == "X") ? "O" :"X";
        
        if (PlayerSide == "X")
            _Turntext.text = "<<";
        else
            _Turntext.text = ">>";
        
            StartCoroutine(delayTurnValue(_Turntext));
        
    }
    
    IEnumerator delayTurnValue(Text txt)
    {
        
        yield return new WaitForSeconds(1.5f);
        
    }


    public void gameOverPlayer(string winningValue)
    {
        _GameOverPanel.SetActive(true);
        _RestartButton.SetActive(true);
        if (winningValue == "win")
        {
            if (PlayerSide==Temp )
            {    PlayerWin++;
                _PlayerWinText.text = PlayerWin.ToString();
            }
            else if (PlayerSide==((Temp == "X") ? "O" :"X"))
            {
                Player2Win++;
                _Player2WinText.text = Player2Win.ToString();
            }
            _Wintext.text = "W  I  N  S ";
        }
        else if (winningValue == "draw")
        {
            
            _tieImage.enabled = true;
            _Turntext.enabled = false;
            _Wintext.text = "  M A T C H ";

        }

        for (int i = 0; i < buttonList1.Length; i++)
        {
            buttonList1[i].GetComponentInParent<Button>().interactable = false;
        }

       
       
    }
    
    public void SetRestart()
    {
       Awake();
       _GameOverPanel.SetActive(false);
        _PlayerPage.SetActive(true);
        _tieImage.enabled = false;
        _Turntext.enabled = true;
        for (int i = 0; i < buttonList1.Length; i++)
        {
            buttonList1[i].GetComponentInParent<Button>().interactable = true;
            buttonList1[i].text = "";
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
            (buttonList1[0].text == PlayerSide && buttonList1[1].text == PlayerSide &&
             buttonList1[2].text == PlayerSide) ||
            (buttonList1[3].text == PlayerSide && buttonList1[4].text == PlayerSide &&
             buttonList1[5].text == PlayerSide) ||
            (buttonList1[6].text == PlayerSide && buttonList1[7].text == PlayerSide &&
             buttonList1[8].text == PlayerSide) ||
            //COLUMN
            (buttonList1[0].text == PlayerSide && buttonList1[3].text == PlayerSide &&
             buttonList1[6].text == PlayerSide) ||
            (buttonList1[1].text == PlayerSide && buttonList1[4].text == PlayerSide &&
             buttonList1[7].text == PlayerSide) ||
            (buttonList1[2].text == PlayerSide && buttonList1[5].text == PlayerSide &&
             buttonList1[8].text == PlayerSide) ||
            //DIAGONAL
            (buttonList1[0].text == PlayerSide && buttonList1[4].text == PlayerSide &&
             buttonList1[8].text == PlayerSide) ||
            (buttonList1[2].text == PlayerSide && buttonList1[4].text == PlayerSide &&
             buttonList1[6].text == PlayerSide))
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

