using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GridspacePvP : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button1;
    public Text buttonText1;
    public Text turntext;
    private Game_Controller1 _gameController1;
    private bool play;
    public void SetGameControllerReference(Game_Controller1 controller)
    {
        _gameController1 = controller;
    }
    

    public void SetSpace()
    {
       
        buttonText1.text = _gameController1.GetPlayerSide();
        button1.interactable = false;
        _gameController1.EndTurn();
        
    }
}