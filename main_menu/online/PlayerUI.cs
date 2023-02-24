using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
   public Text PlayerText;
   private Player player;

   public void SetPlayer(Player player){
    this.player = player;
    PlayerText.text = "name";
   }
}
