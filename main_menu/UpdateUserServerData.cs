using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using CharacterData;
using UnityEngine.UI;

public class UpdateUserServerData : MonoBehaviour
{
    [SerializeField]private playerData data;
    public GameObject importantRedMessage;
    [SerializeField] private Text username;
    [SerializeField] private Text currentLevel;
    [SerializeField] private Camera characterChangeCam;
    [SerializeField] private Camera mainMenuCam;
    [SerializeField] private Canvas characterChangePage;
    [SerializeField] private Canvas mainMenuPage;
    void Start()
    {
        data = GameObject.FindWithTag("Player").GetComponent<playerData>();
        getData();
    }

    public void updateData(){
        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string, string>{
                {"isHasCharacter", true.ToString()},
                {"currentGenderParts", CharacterData.CharacterData.currentGenderParts == CharacterData.CharacterData.maleParts? "maleParts" : "femaleParts"},
                {"currentHeadId", data.currentHeadId.ToString()},
                {"currentFacialHairId", data.currentFacialHairId.ToString()},
                {"currentHairId", data.currentHairId.ToString()},
                {"currentEyebrowsId", data.currentEyebrowsId.ToString()}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    private void getData(){
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    private void OnDataRecieved(GetUserDataResult result){
        Debug.Log("[got user data]");
        if(result.Data.ContainsKey("isHasCharacter") && bool.Parse(result.Data["isHasCharacter"].Value)){
        PlayFabClientAPI.GetPlayerProfile( new GetPlayerProfileRequest() {
        ProfileConstraints = new PlayerProfileViewConstraints() {
        ShowDisplayName = true
        }
        },
        result => username.text = result.PlayerProfile.DisplayName,
        error => Debug.LogError(error.ErrorMessage));

            CharacterCreating.characterReset(CharacterData.CharacterData.currentGenderParts, data);
            data.currentEyebrowsId = int.Parse(result.Data["currentEyebrowsId"].Value);
            data.currentFacialHairId = int.Parse(result.Data["currentFacialHairId"].Value);
            data.currentHairId = int.Parse(result.Data["currentHairId"].Value);
            data.currentHeadId = int.Parse(result.Data["currentHeadId"].Value);
            CharacterData.CharacterData.currentGenderParts = result.Data["currentGenderParts"].Value == "femaleParts" ? CharacterData.CharacterData.femaleParts : CharacterData.CharacterData.maleParts;
            if(result.Data["currentGenderParts"].Value == "femaleParts"){
                CharacterData.CharacterData.maleParts.SetActive(false);
                CharacterData.CharacterData.femaleParts.SetActive(true);
            CharacterCreating.FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHair[data.currentHairId].modelOfDecoration.name).SetActive(true);
            CharacterCreating.FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleFacialHair[data.currentFacialHairId].modelOfDecoration.name).SetActive(true);
            CharacterCreating.FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHead[data.currentHeadId].modelOfDecoration.name).SetActive(true);
            CharacterCreating.FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleEyebrows[data.currentEyebrowsId].modelOfDecoration.name).SetActive(true);
            }
            else if(result.Data["currentGenderParts"].Value == "maleParts"){
                CharacterData.CharacterData.femaleParts.SetActive(false);
                CharacterData.CharacterData.maleParts.SetActive(true);
            CharacterCreating.FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHair[data.currentHairId].modelOfDecoration.name).SetActive(true);
            CharacterCreating.FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleFacialHair[data.currentFacialHairId].modelOfDecoration.name).SetActive(true);
            CharacterCreating.FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHead[data.currentHeadId].modelOfDecoration.name).SetActive(true);
            CharacterCreating.FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleEyebrows[data.currentEyebrowsId].modelOfDecoration.name).SetActive(true);
            }

        }
    }


    private void OnError(PlayFabError error){
        if(!importantRedMessage.GetComponent<Animation>().isPlaying){
                importantRedMessage.transform.Find("Text").GetComponent<Text>().text = error.ErrorMessage;
                importantRedMessage.GetComponent<Animation>().Play("importantRedMessageAnimation");
            }
    }

    private void OnDataSend(UpdateUserDataResult result){
        Debug.Log("[DATA SENT]");
        PlayFabClientAPI.GetUserData(
        new GetUserDataRequest(),
        result => ChangeCanvas(result),
        error => Debug.Log(error.ErrorMessage+"------")

         
    );
    }

    private void ChangeCanvas(GetUserDataResult result){
            if(result.Data.ContainsKey("isHasCharacter") && bool.Parse(result.Data["isHasCharacter"].Value)){
            characterChangeCam.gameObject.SetActive(false);
            mainMenuCam.gameObject.SetActive(true);
            characterChangePage.gameObject.SetActive(false);
            mainMenuPage.gameObject.SetActive(true);
        }
    }

}
