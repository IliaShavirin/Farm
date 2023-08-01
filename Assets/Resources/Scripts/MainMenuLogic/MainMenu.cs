using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Клас що відповідає за головне меню
/// </summary>
public class MainMenu : MonoBehaviour
{ 
    /// <summary>
    /// Метод який викликається коли гра знайшла збереження
    /// </summary>
    public void  Play()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);// Завантаження ігрової локації
   }
    /// <summary>
    /// Метод який викликається коли гра не знайшла збереження
    /// </summary>
     public void  NewPlay()
   {
       DataBase.ClearDataBase();
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);// Завантаження ігрової локації
   }
   public void Exit()
   {
       Application.Quit();// Вихід з гри
   }
}
