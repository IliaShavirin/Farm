using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuForWindow : MonoBehaviour
{
     public void  Save()
   {
       DataBase.Save();
   }
   public void Exit()
   {
       Application.Quit();
   }
}
