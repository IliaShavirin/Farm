using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerInfo 
{
   public int Money;
   public int lvl;
   public float LvlProgress;
   public bool conservation_exists;
  public  float _positionx;
    public  float _positiony;
    public string lastSaveTime;
    public long timeOffline;
    public int lvlstep;


   public PlayerInfo (int Money,int lvl,float LvlProgress,bool  conservation_exists,float positionx,float positiony,string lastSaveTime,int lvlstep)
   {
       this.Money=Money;
       this.lvl=lvl;
       this.LvlProgress=LvlProgress;
       this.conservation_exists = conservation_exists;
       this._positionx=positionx;
       this._positiony=positiony;
       this.lastSaveTime=lastSaveTime;
       this.lvlstep = lvlstep;
   }
}
