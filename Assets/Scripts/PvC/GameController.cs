using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameController : MonoBehaviour
{
    [SerializeField] private Text[] buttonList;
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
    [SerializeField] private Text _CompDisplaytext;
    [SerializeField] private Text _PlayerWinText;
    [SerializeField] private Text _CompWinText;
    
    [SerializeField] private int movecount;
                     public int compValue;
    [SerializeField] private float delay;
    [SerializeField] private int PlayerWin=0, CompWin=0;

     public  bool PlayerTurn;

    private void Awake()
    {
        _GameOverPanel.SetActive(false);
        _RestartButton.SetActive(false);
       
        SetGameControllerOnButtons();
        
        _PlayerDisplayText.text = "";
        _CompDisplaytext.text = "";
        
        PlayerTurn = true;
        PlayerSide = "";
        movecount = 0;
        delay = 5;
    }

    void SetGameControllerOnButtons()
    {
        for (int i = 0; i < 9; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public void setPlayer(string value)
    {
        PlayerSide = value;
        ComputerSide = (PlayerSide == "X") ? ComputerSide="O" :ComputerSide="X";
        _PlayerDisplayText.text = PlayerSide;
        _CompDisplaytext.text = ComputerSide;
        
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

    private void Update()
    {
       
        if (PlayerTurn == false)
        {
            delay += delay * Time.deltaTime*2f;
            if (delay >= 100)
            {
                    
                compValue = Random.Range(0, 9);
                if (buttonList[compValue].GetComponentInParent<Button>().interactable == true)
                {
                    buttonList[compValue].text = ComputerSide;
                   // StartCoroutine((PlayerturnText(_Turntext)));
                    buttonList[compValue].GetComponentInParent<Button>().interactable = false;
                    EndTurn();
                    delay = 5;
                }
               
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
        //Player
        
        if (PlayerTurn == true)
        {
           
            if (PlayerWinCondition())
            {
                gameOverPlayer("win");
               

            }
            else
            {
                ChangeSide();
                PlayerTurn =false;
            }
        }
        //Computer
        
        else
           
        {
            
                if ( ComputerWinCondition())
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

        for (int i = 0; i < buttonList.Length; i++)
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

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
        _CompWinText.text = CompWin.ToString();
       
    }
    
    public void SetRestart()
    {
       Awake();
        /*PlayerSide = "";
        ComputerSide = "";
        movecount = 0;
        delay =5;*/
        _GameOverPanel.SetActive(false);
        _PlayerPage.SetActive(true);
        _tieImage.enabled = false;
        _Turntext.enabled = true;
        for (int i = 0; i < buttonList.Length; i++)
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

