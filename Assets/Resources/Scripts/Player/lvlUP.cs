using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlUP : MonoBehaviour
{
private SpriteRenderer LVLup;
private SpriteRenderer LVLup0;
private SpriteRenderer LVLup1;
private SpriteRenderer LVLup2;
 
 
public IEnumerator UpLvl()
    {
         LVLup.sprite = Resources.Load<Sprite>("Object/lvl");
          yield return new WaitForSeconds(2f);
           LVLup.sprite = Resources.Load<Sprite>("Canvas/empty");
       Player.playerInfo.lvlstep++;
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        LVLup = GetComponent<SpriteRenderer>();
        LVLup0 = GetComponentsInChildren<SpriteRenderer>()[0];
        LVLup1 = GetComponentsInChildren<SpriteRenderer>()[1];
        LVLup2 = GetComponentsInChildren<SpriteRenderer>()[2];
       
    }

    // Update is called once per frame
    void Update()
    {
         if (Player.playerInfo.lvlstep<Player.playerInfo.lvl) 
        {
            StartCoroutine(UpLvl());
        }
    }
}
