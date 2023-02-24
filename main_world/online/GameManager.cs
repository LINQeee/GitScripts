using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : MonoBehaviour
{
    public void stopGame(){
        if(NetworkServer.active && NetworkClient.isConnected){
            NetworkManager.singleton.StopHost();
        }
        else if(NetworkClient.isConnected){
            NetworkManager.singleton.StopClient();
        }
        else if(NetworkServer.active){
            NetworkManager.singleton.StopServer();
        }
    }
}
