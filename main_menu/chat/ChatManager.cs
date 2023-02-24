using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using Newtonsoft.Json;
using PlayFab.ServerModels;

public class ChatManager : MonoBehaviour
{

    [SerializeField] private TMP_InputField messageInput;
    [SerializeField] private GameObject inputField;
    private string username;
    private List<string> currentData = new List<string>();
    private int currentCount;
    private string lastMessage;
    void Start()
    {
        PlayFabClientAPI.GetPlayerProfile( new PlayFab.ClientModels.GetPlayerProfileRequest() {
        ProfileConstraints = new PlayFab.ClientModels.PlayerProfileViewConstraints() {
        ShowDisplayName = true
        }
        },
        result => username = result.PlayerProfile.DisplayName,
        error => Debug.LogError(error.ErrorMessage));

        StartCoroutine(checkForNewMessages());
    }

    public ChatMessage ReturnClass(){
            return new ChatMessage(username, messageInput.text);
        }
    
    public string getMessage(ChatMessage chatMessage){
        return $"{chatMessage.Author}: {chatMessage.Message}";
    }

    public void sendMessage(){
       var listOfMessages = new List<string>();
        foreach(var word in currentData){
            listOfMessages.Add(word);
        }
        
        listOfMessages.Add(getMessage(new ChatMessage(username, messageInput.text)));
      //  if(listOfMessages.Count > 10) listOfMessages.RemoveAt(listOfMessages.Count-1);
        Debug.Log(listOfMessages.Count+"------"+currentData.Count);
        PlayFabServerAPI.SetTitleData(new SetTitleDataRequest
        {
            Key = "Messages",
            Value = JsonConvert.SerializeObject(listOfMessages),
        },
            result => Debug.Log("[MESSAGE SENT]"),
            error => Debug.Log(error.ErrorMessage)
        );
    }

    private IEnumerator checkForNewMessages()
    {
startPos:
        PlayFabClientAPI.GetTitleData(new PlayFab.ClientModels.GetTitleDataRequest(), OnDataRecieved, OnError);
        yield return new WaitForSecondsRealtime(0.3f);
        goto startPos;
    }

    private void OnDataRecieved(PlayFab.ClientModels.GetTitleDataResult result){
        Debug.Log(currentData.Count+" "+JsonConvert.DeserializeObject<List<string>>(result.Data["Messages"]).Count);
        if(result.Data["Messages"] != null && JsonConvert.DeserializeObject<List<string>>(result.Data["Messages"]).Count > currentData.Count){
            Debug.Log("[DATA GOT]");
            currentData = JsonConvert.DeserializeObject<List<string>>(result.Data["Messages"]);
            for(int i = 0; i < transform.childCount; i++){
                Destroy(transform.GetChild(i).gameObject);
            }
            for(int i = currentData.Count-1; i>currentData.Count-11;i--){
            GameObject newMessage = Instantiate(inputField, transform);
            newMessage.GetComponent<TMP_InputField>().text = currentData[i];
            newMessage.GetComponent<TMP_InputField>().interactable = false;
            }
            }
        }
        
        
    private void OnError(PlayFabError error){
        Debug.Log(error.ErrorMessage);
    }
}

public class ChatMessage{
    public string Author;
    public string Message;
    public ChatMessage(string Author, string Message){
        this.Author = Author;
        this.Message = Message;
    }
}
