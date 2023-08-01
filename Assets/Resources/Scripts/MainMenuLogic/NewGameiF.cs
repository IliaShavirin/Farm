using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class NewGameiF : MonoBehaviour
{
private Text TextButton;
private bool conservation_exists;

    
    void Start()
    { TextButton=GetComponentInChildren<Text>();

 if(File.Exists(DataBase.playerInfoPath))
        {
           TextButton.text="Continue";
        }
        else
        {
            TextButton.text="New Game";
        }
        
    }
}
