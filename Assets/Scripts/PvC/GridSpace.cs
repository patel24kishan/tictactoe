using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GridSpace : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    public Text buttonText;
    public Text turntext;
    private GameController _gameController;
    private bool play;
    public void SetGameControllerReference(GameController controller)
    {
        _gameController = controller;
    }
    

    public void SetSpace()
    {
        if (_gameController.PlayerTurn == true)
        {
            buttonText.text = _gameController.GetPlayerSide();
            button.interactable = false;
            _gameController.EndTurn();
        }
    }
}
