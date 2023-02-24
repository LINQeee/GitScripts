using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;
using System.Text;
using System.Security.Cryptography;

public class Match : NetworkBehaviour
{
    public string ID;
    public readonly List<GameObject> players = new List<GameObject>();

    public Match(string ID, GameObject player){
        this.ID = ID;
        players.Add(player);
    }
}
public class MainMenu : NetworkBehaviour
{
    public static MainMenu instance;
    [SerializeField] private readonly SyncList<Match> matches = new SyncList<Match>();
    [SerializeField] private readonly SyncList<string> matchIDs = new SyncList<string>();
    [SerializeField] private InputField JoinInput;
    [SerializeField] private Button HostButton;
    [SerializeField] private Button JoinButton;
    [SerializeField] private Canvas LobbyCanvas;

    [SerializeField] private Transform UIPlayerParent;
    [SerializeField] private GameObject UIPlayerPrefab;
    [SerializeField] private Text IDText;
    [SerializeField] private Button BeginGameButton;
    [SerializeField] private GameObject TurnManager;
    public bool inGame;

    private void Start() {
        instance = this;
    }

    private void Update() {
        if(!inGame){
            Player[] players = FindObjectsOfType<Player>();

            for (int i = 0; i < players.Length; i++)
            {
                players[i].gameObject.transform.localScale = Vector3.zero;
            }
        }
    }

//* HOST SCRIPTS
    public void Host(){
        JoinInput.interactable = false;
        HostButton.interactable = false;
        JoinButton.interactable = false;

        Player.localPlayer.HostGame();
    }

    public void HostSucces(bool succes, string matchID){
        if(succes){
            LobbyCanvas.enabled = true;

            SpawnPlayerUIPrefab(Player.localPlayer);
            IDText.text = matchID;
            BeginGameButton.interactable = true;
        }
        else{
            JoinInput.interactable = true;
        HostButton.interactable = true;
        JoinButton.interactable = true;
        }
    }
    public bool HostGame(string matchID, GameObject player){
        if(!matchIDs.Contains(matchID)){
            matchIDs.Add(matchID);
            matches.Add(new Match(matchID, player));
            return true;
        }
        return false;
    }

//* CLIENT SCRIPTS
 public void Join(){
        JoinInput.interactable = false;
        HostButton.interactable = false;
        JoinButton.interactable = false;

        Player.localPlayer.JoinGame(JoinInput.text.ToUpper());
    }

    public void JoinSucces(bool succes, string matchID){
        if(succes){
            LobbyCanvas.enabled = true;

            SpawnPlayerUIPrefab(Player.localPlayer);
            IDText.text = matchID;
            BeginGameButton.interactable = false;
        }
        else{
            JoinInput.interactable = true;
        HostButton.interactable = true;
        JoinButton.interactable = true;
        }
    }

    public bool JoinGame(string matchID, GameObject player){
        if(matchIDs.Contains(matchID)){
            for (int i = 0; i < matches.Count; i++)
            {
                if(matches[i].ID == matchID){
                    matches[i].players.Add(player);
                    break;
                }
            }
            return true;
        }
        return false;
    }
//* SYSTEM(SERVER) SCRIPTS
    public static string GetRandomID(){
        string ID = string.Empty;
        for (int i = 0; i < 5; i++)
        {
            int rand = UnityEngine.Random.Range(0, 36);
            if(rand < 26) ID += (char)(rand + 65);
            else ID += (rand - 26).ToString();

        }
        return ID;
    }

    public void SpawnPlayerUIPrefab(Player player){
        GameObject newUIPlayer = Instantiate(UIPlayerPrefab, UIPlayerParent);
        newUIPlayer.GetComponent<PlayerUI>().SetPlayer(player);
    }

    public void StartGame(){
        Player.localPlayer.BeginGame();
    }

    public void BeginGame(string matchID){
        GameObject newTurnManager = Instantiate(TurnManager);
        NetworkServer.Spawn(newTurnManager);
        newTurnManager.GetComponent<NetworkMatch>().matchId = matchID.ToGuid();
        TurnManager turnManager = newTurnManager.GetComponent<TurnManager>();

        for (int i = 0; i < matches.Count; i++)
        {
            if(matches[i].ID == matchID){
                foreach (var player in matches[i].players)
                {   
                    Player player1 = player.GetComponent<Player>();
                    turnManager.AddPlayer(player1);
                    player1.StartGame();
                }
                break;
            }
        }
    }

}

public static class MatchExtension{
    public static Guid ToGuid(this string id){
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        byte[] inputBytes = Encoding.Default.GetBytes(id);
        byte[] hashBytes = provider.ComputeHash(inputBytes);

        return new Guid(hashBytes);
    }
}
