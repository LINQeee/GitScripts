using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using CharacterData;

public class CharacterCreating : MonoBehaviour
{
    private playerData data;
    private GameObject player;

    [SerializeField] private Text hairText;
    [SerializeField] private Text facialHairText;
    [SerializeField] private Text faceText;
    [SerializeField] private Text eyebrowsText;
    [SerializeField] private Toggle femaleToggle;
    [SerializeField] private Toggle maleToggle;
    [SerializeField] private GameObject importantRedMessage;
    [SerializeField] private GameObject importantGreenMessage;

    private void Start() {
        data = GameObject.FindWithTag("Player").GetComponent<playerData>();
        player = GameObject.FindWithTag("Player");

        importantGreenMessage.transform.Find("Text").GetComponent<Text>().text = "It looks like you don't have a character yet, let's create one!";
        importantGreenMessage.GetComponent<Animation>().Play("importantGreenMessageAnimation");
    }

public void genderToMaleButton(){
    if(maleToggle.isOn){
            characterReset(CharacterData.CharacterData.femaleParts);
            CharacterData.CharacterData.currentGenderParts = CharacterData.CharacterData.maleParts;
            CharacterData.CharacterData.maleParts.SetActive(true);
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHair[data.currentHairId].modelOfDecoration.name).SetActive(true);
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleFacialHair[data.currentFacialHairId].modelOfDecoration.name).SetActive(true);
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHead[data.currentHeadId].modelOfDecoration.name).SetActive(true);
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleEyebrows[data.currentEyebrowsId].modelOfDecoration.name).SetActive(true);
            hairText.text = "HAIR 1";
           facialHairText.text = "FACIAL HAIR 1";
           faceText.text = "FACE 1";
           eyebrowsText.text = "EYEBROWS 1";
    }
}

public void genderToFemaleButton(){
    if(femaleToggle.isOn){
            characterReset(CharacterData.CharacterData.maleParts);
            CharacterData.CharacterData.currentGenderParts = CharacterData.CharacterData.femaleParts;
            CharacterData.CharacterData.femaleParts.SetActive(true);
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHair[data.currentHairId].modelOfDecoration.name).SetActive(true);
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleFacialHair[data.currentFacialHairId].modelOfDecoration.name).SetActive(true);
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHead[data.currentHeadId].modelOfDecoration.name).SetActive(true);
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleEyebrows[data.currentEyebrowsId].modelOfDecoration.name).SetActive(true);
             hairText.text = "HAIR 1";
           facialHairText.text = "FACIAL HAIR 1";
           faceText.text = "FACE 1";
           eyebrowsText.text = "EYEBROWS 1";
    }
}

 public void EyebrowPreviousButton(){
        if(CharacterData.CharacterData.currentGenderParts == CharacterData.CharacterData.femaleParts){
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleEyebrows[data.currentEyebrowsId].modelOfDecoration.name).SetActive(false);
            if(data.currentEyebrowsId == 0){
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleEyebrows[CharacterData.CharacterData.allFemaleEyebrows.Count-1].modelOfDecoration.name).SetActive(true);
                data.currentEyebrowsId = CharacterData.CharacterData.allFemaleEyebrows.Count-1;
            }
            else{
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleEyebrows[data.currentEyebrowsId-1].modelOfDecoration.name).SetActive(true);
                data.currentEyebrowsId -= 1;
            }
        }
        else{
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleEyebrows[data.currentEyebrowsId].modelOfDecoration.name).SetActive(false);
            if(data.currentEyebrowsId == 0){
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleEyebrows[CharacterData.CharacterData.allMaleEyebrows.Count-1].modelOfDecoration.name).SetActive(true);
                data.currentEyebrowsId = CharacterData.CharacterData.allMaleEyebrows.Count-1;
            }
            else{
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleEyebrows[data.currentEyebrowsId-1].modelOfDecoration.name).SetActive(true);
                data.currentEyebrowsId -= 1;
            }
        }
        eyebrowsText.text = $"EYEBROWS {data.currentEyebrowsId+1}";
    }

    public void EyebrowNextButton(){
        if(CharacterData.CharacterData.currentGenderParts == CharacterData.CharacterData.femaleParts){
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleEyebrows[data.currentEyebrowsId].modelOfDecoration.name).SetActive(false);
            if(data.currentEyebrowsId == CharacterData.CharacterData.allFemaleEyebrows.Count-1){
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleEyebrows[0].modelOfDecoration.name).SetActive(true);
                data.currentEyebrowsId = 0;
            }
            else{
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleEyebrows[data.currentEyebrowsId+1].modelOfDecoration.name).SetActive(true);
                data.currentEyebrowsId += 1;
            }
        }
        else{
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleEyebrows[data.currentEyebrowsId].modelOfDecoration.name).SetActive(false);
            if(data.currentEyebrowsId == CharacterData.CharacterData.allMaleEyebrows.Count-1){
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleEyebrows[0].modelOfDecoration.name).SetActive(true);
                data.currentEyebrowsId = 0;
            }
            else{
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleEyebrows[data.currentEyebrowsId+1].modelOfDecoration.name).SetActive(true);
                data.currentEyebrowsId += 1;
            }
        }
        eyebrowsText.text = $"EYEBROWS {data.currentEyebrowsId+1}";
    }

 public void FacePreviousButton(){
        if(CharacterData.CharacterData.currentGenderParts == CharacterData.CharacterData.femaleParts){
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHead[data.currentHeadId].modelOfDecoration.name).SetActive(false);
            if(data.currentHeadId == 0){
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHead[CharacterData.CharacterData.allFemaleHead.Count-1].modelOfDecoration.name).SetActive(true);
                data.currentHeadId = CharacterData.CharacterData.allFemaleHead.Count-1;
            }
            else{
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHead[data.currentHeadId-1].modelOfDecoration.name).SetActive(true);
                data.currentHeadId -= 1;
            }
        }
        else{
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHead[data.currentHeadId].modelOfDecoration.name).SetActive(false);
            if(data.currentHeadId == 0){
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHead[CharacterData.CharacterData.allMaleHead.Count-1].modelOfDecoration.name).SetActive(true);
                data.currentHeadId = CharacterData.CharacterData.allMaleHead.Count-1;
            }
            else{
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHead[data.currentHeadId-1].modelOfDecoration.name).SetActive(true);
                data.currentHeadId -= 1;
            }
        }
        faceText.text = $"FACE {data.currentHeadId+1}";
    }

    public void FaceNextButton(){
        if(CharacterData.CharacterData.currentGenderParts == CharacterData.CharacterData.femaleParts){
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHead[data.currentHeadId].modelOfDecoration.name).SetActive(false);
            if(data.currentHeadId == CharacterData.CharacterData.allFemaleHead.Count-1){
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHead[0].modelOfDecoration.name).SetActive(true);
                data.currentHeadId = 0;
            }
            else{
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHead[data.currentHeadId+1].modelOfDecoration.name).SetActive(true);
                data.currentHeadId += 1;
            }
        }
        else{
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHead[data.currentHeadId].modelOfDecoration.name).SetActive(false);
            if(data.currentHeadId == CharacterData.CharacterData.allMaleHead.Count-1){
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHead[0].modelOfDecoration.name).SetActive(true);
                data.currentHeadId = 0;
            }
            else{
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHead[data.currentHeadId+1].modelOfDecoration.name).SetActive(true);
                data.currentHeadId += 1;
            }
        }
        faceText.text = $"FACE {data.currentHeadId+1}";
    }

    public void facialHairPreviousButton(){
        if(CharacterData.CharacterData.currentGenderParts == CharacterData.CharacterData.femaleParts){
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleFacialHair[data.currentFacialHairId].modelOfDecoration.name).SetActive(false);
            if(data.currentFacialHairId == 0){
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleFacialHair[CharacterData.CharacterData.allFemaleFacialHair.Count-1].modelOfDecoration.name).SetActive(true);
                data.currentFacialHairId = CharacterData.CharacterData.allFemaleFacialHair.Count-1;
            }
            else{
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleFacialHair[data.currentFacialHairId-1].modelOfDecoration.name).SetActive(true);
                data.currentFacialHairId -= 1;
            }
           if(!importantRedMessage.GetComponent<Animation>().isPlaying){
                importantRedMessage.transform.Find("Text").GetComponent<Text>().text = "You can't change facial hair on a female";
                importantRedMessage.GetComponent<Animation>().Play("importantRedMessageAnimation");
            }
        }
        else{
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleFacialHair[data.currentFacialHairId].modelOfDecoration.name).SetActive(false);
            if(data.currentFacialHairId == 0){
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleFacialHair[CharacterData.CharacterData.allMaleFacialHair.Count-1].modelOfDecoration.name).SetActive(true);
                data.currentFacialHairId = CharacterData.CharacterData.allMaleFacialHair.Count-1;
            }
            else{
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleFacialHair[data.currentFacialHairId-1].modelOfDecoration.name).SetActive(true);
                data.currentFacialHairId -= 1;
            }
        }
        facialHairText.text = $"FACIAL HAIR {data.currentFacialHairId+1}";
    }

    public void facialHairNextButton(){
        if(CharacterData.CharacterData.currentGenderParts == CharacterData.CharacterData.femaleParts){
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleFacialHair[data.currentFacialHairId].modelOfDecoration.name).SetActive(false);
            if(data.currentFacialHairId == CharacterData.CharacterData.allFemaleFacialHair.Count-1){
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleFacialHair[0].modelOfDecoration.name).SetActive(true);
                data.currentFacialHairId = 0;
            }
            else{
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleFacialHair[data.currentFacialHairId+1].modelOfDecoration.name).SetActive(true);
                data.currentFacialHairId += 1;
            }
            
            if(!importantRedMessage.GetComponent<Animation>().isPlaying){
                importantRedMessage.transform.Find("Text").GetComponent<Text>().text = "You can't change facial hair on a female";
                importantRedMessage.GetComponent<Animation>().Play("importantRedMessageAnimation");
            }
        }
        if(CharacterData.CharacterData.currentGenderParts == CharacterData.CharacterData.maleParts){
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleFacialHair[data.currentFacialHairId].modelOfDecoration.name).SetActive(false);
            if(data.currentFacialHairId == CharacterData.CharacterData.allMaleFacialHair.Count-1){
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleFacialHair[0].modelOfDecoration.name).SetActive(true);
                data.currentFacialHairId = 0;
            }
            else{
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleFacialHair[data.currentFacialHairId+1].modelOfDecoration.name).SetActive(true);
                data.currentFacialHairId += 1;
            }
        }
        facialHairText.text = $"FACIAL HAIR {data.currentFacialHairId+1}";
    }

    public void hairPreviousButton(){
        if(CharacterData.CharacterData.currentGenderParts == CharacterData.CharacterData.femaleParts){
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHair[data.currentHairId].modelOfDecoration.name).SetActive(false);
           if(data.currentHairId == 0) {
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHair[CharacterData.CharacterData.allFemaleHair.Count-1].modelOfDecoration.name).SetActive(true);
                data.currentHairId = CharacterData.CharacterData.allFemaleHair.Count-1;
           }
           else{
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHair[data.currentHairId-1].modelOfDecoration.name).SetActive(true);
                data.currentHairId -= 1;
           }
        }
        else{
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHair[data.currentHairId].modelOfDecoration.name).SetActive(false);
           if(data.currentHairId == 0) {
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHair[CharacterData.CharacterData.allMaleHair.Count-1].modelOfDecoration.name).SetActive(true);
                data.currentHairId = CharacterData.CharacterData.allMaleHair.Count-1;
           }
           else{
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHair[data.currentHairId-1].modelOfDecoration.name).SetActive(true);
                data.currentHairId -= 1;
           }
        }

        hairText.text = $"HAIR {data.currentHairId+1}";
    }

    public void hairNextButton(){
        if(CharacterData.CharacterData.currentGenderParts == CharacterData.CharacterData.femaleParts){
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHair[data.currentHairId].modelOfDecoration.name).SetActive(false);
           if(data.currentHairId == CharacterData.CharacterData.allFemaleHair.Count-1) {
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHair[0].modelOfDecoration.name).SetActive(true);
                data.currentHairId = 0;
           }
           else{
                FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHair[data.currentHairId+1].modelOfDecoration.name).SetActive(true);
                data.currentHairId += 1;
           }
        }
        else{
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHair[data.currentHairId].modelOfDecoration.name).SetActive(false);
           if(data.currentHairId == CharacterData.CharacterData.allMaleHair.Count-1) {
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHair[0].modelOfDecoration.name).SetActive(true);
                data.currentHairId = 0;
           }
           else{
                FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHair[data.currentHairId+1].modelOfDecoration.name).SetActive(true);
                data.currentHairId += 1;
           }
        }
        hairText.text = $"HAIR {data.currentHairId+1}";
    }

    private void characterReset(GameObject genderParts){
        if(genderParts == CharacterData.CharacterData.femaleParts){
            CharacterData.CharacterData.femaleParts.SetActive(false);
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHair[data.currentHairId].modelOfDecoration.name).SetActive(false);
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleFacialHair[data.currentFacialHairId].modelOfDecoration.name).SetActive(false);
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleHead[data.currentHeadId].modelOfDecoration.name).SetActive(false);
            FromAllChilds(CharacterData.CharacterData.femaleParts, CharacterData.CharacterData.allFemaleEyebrows[data.currentEyebrowsId].modelOfDecoration.name).SetActive(false);
        }
        else{
            CharacterData.CharacterData.maleParts.SetActive(false);
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHair[data.currentHairId].modelOfDecoration.name).SetActive(false);
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleFacialHair[data.currentFacialHairId].modelOfDecoration.name).SetActive(false);
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleHead[data.currentHeadId].modelOfDecoration.name).SetActive(false);
            FromAllChilds(CharacterData.CharacterData.maleParts, CharacterData.CharacterData.allMaleEyebrows[data.currentEyebrowsId].modelOfDecoration.name).SetActive(false);
        }
            data.currentEyebrowsId = 0;
            data.currentFacialHairId = 0;
            data.currentHairId = 0;
            data.currentHeadId = 0;
    }

    public GameObject FromAllChilds(GameObject obj, string name) {
     foreach(Transform tran in obj.transform)
     {
        if(tran.name == name) return tran.gameObject;
        else
        {
            foreach(Transform trans in tran)
            {
                if(trans.name == name) return trans.gameObject;
                else
                {
                    foreach(Transform transf in trans)
                    {
                        if(transf.name == name) return transf.gameObject;
                        else
                        {
                            foreach(Transform transfor in transf)
                            {
                                if(transfor.name == name) return transfor.gameObject;
                                else
                                {
                                    foreach(Transform transform in transfor)
                                    {
                                        if(transform.name == name) return transform.gameObject;
                                    }
                                }
                            }
                        }
                    }
                } 
            }
        }
     }
     return null;
 }
}
