using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameOrCount : MonoBehaviour
{
   private Text TextButton;

    
    void Start()
    { TextButton=GetComponentInChildren<Text>();
      var playerInfo = DataBase.LoadPlayerInfo();

        if(playerInfo.conservation_exists)
        {
           this.transform.localScale=new Vector2(1,1);

           
        }
        else
        {
            this.transform.localScale=new Vector2(0,0);
        }
        
    }
}
