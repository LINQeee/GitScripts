using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Player : NetworkBehaviour
{
    public static Player localPlayer;
    [SyncVar] public string matchID;
    private NetworkMatch networkMatch;

    private void Start() {
        networkMatch = GetComponent<NetworkMatch>();

        if(localPlayer){
            localPlayer = this;
        }
        else{
            MainMenu.instance.SpawnPlayerUIPrefab(this);
        }
    }
//* //////////////////////////////////////////////////////////////////////////  
//* ///////////////HOST SCRPTS////////////////////////////////////////////////  
//* //////////////////////////////////////////////////////////////////////////  
    public void HostGame(){
        string ID = MainMenu.GetRandomID();
        CmdHostGame(ID);
    }

    [Command]
    public void CmdHostGame(string ID){
        matchID = ID;
        if(MainMenu.instance.HostGame(ID, gameObject)){
            Debug.Log($"CREATED LOBBY WITH ID :{ID}");
            networkMatch.matchId = ID.ToGuid();
            TargetHostGame(true, ID);
        }
        else
        {
            Debug.Log("FAILED TO CREATE LOBBY");
            TargetHostGame(false, ID);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool succes, string ID){
        matchID = ID;
        Debug.Log($"ID {matchID} == {ID}");
        MainMenu.instance.HostSucces(succes, ID);
    }

//* //////////////////////////////////////////////////////////////////////////  
//* ///////////////CLIENT SCRPTS//////////////////////////////////////////////
//* ////////////////////////////////////////////////////////////////////////// 

public void JoinGame(string inputID){
        CmdJoinGame(inputID);
    }

    [Command]
    public void CmdJoinGame(string ID){
        matchID = ID;
        if(MainMenu.instance.JoinGame(ID, gameObject)){
            Debug.Log($"SUCCESFULLY JOINED LOBBY");
            networkMatch.matchId = ID.ToGuid();
            TargetJoinGame(true, ID);
        }
        else
        {
            Debug.Log("FAILED TO JOIN LOBBY");
            TargetJoinGame(false, ID);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool succes, string ID){
        matchID = ID;
        Debug.Log($"ID {matchID} == {ID}");
        MainMenu.instance.JoinSucces(succes, ID);
    }

//* //////////////////////////////////////////////////////////////////////////  
//* ///////////////BEGIN GAME SCRPTS//////////////////////////////////////////  
//* //////////////////////////////////////////////////////////////////////////

public void BeginGame(){
        CmdBeginGame();
    }

    [Command]
    public void CmdBeginGame(){
        MainMenu.instance.BeginGame(matchID);
        Debug.Log("Game Started");
    }

    public void StartGame(){
        TargetBeginGame();
    }

    [TargetRpc]
    void TargetBeginGame(){
        Debug.Log($"ID {matchID} STARTING SESSION");
        DontDestroyOnLoad(gameObject);
        MainMenu.instance.inGame = true;
        transform.localScale = Vector3.one;
        SceneManager.LoadScene("main_world", LoadSceneMode.Additive);
    }
}
