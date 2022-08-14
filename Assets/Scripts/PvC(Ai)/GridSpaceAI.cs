using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using  UnityEngine.UI;

public class GridSpaceAI : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    public Text buttonText;
    private GameControllerAI _gameController;
    private bool play;
    public void SetGameControllerReference(GameControllerAI controller1)
    {
        _gameController = controller1;
    }
    

    public void SetSpace()
    {
        if(_gameController.PlayerTurn == true)
        {
            buttonText.text = _gameController.GetPlayerSide();
            button.interactable = false;
            _gameController.EndTurn();
        }
    }
}
