using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterData{
public class CharacterData : MonoBehaviour
{    [SerializeField] private   GameObject SerializedMaleParts;
     [SerializeField] private   GameObject SerializedFemaleParts;
     [SerializeField] private   List<Decoration> SerializedMaleHair;
     [SerializeField] private   List<Decoration> SerializedMaleHead;
     [SerializeField] private   List<Decoration> SerializedMaleEyebrow;
     [SerializeField] private   List<Decoration> SerializedMaleFacialHair;
     [SerializeField] private   List<Torso> SerializedMaleTorso;
     [SerializeField] private   List<Arms> SerializedMaleArms;
     [SerializeField] private   List<Legs> SerializedMaleLegs;
    

     [SerializeField] private   List<Decoration> SerializedFemaleHair;
     [SerializeField] private   List<Decoration> SerializedFemaleHead;
     [SerializeField] private   List<Decoration> SerializedFemaleEyebrow;
     [SerializeField] private   List<Decoration> SerializedFemaleFacialHair;
     [SerializeField] private   List<Torso> SerializedFemaleTorso;
     [SerializeField] private   List<Arms> SerializedFemaleArms;
     [SerializeField] private   List<Legs> SerializedFemaleLegs;
     
     [SerializeField] private GameObject SerializedCurrentGenderParts;
     [SerializeField] private   List<Head> SerializedHeadEquipment;
     [SerializeField] private   List<BackAttachment> SerializedBackAttachment;
     [SerializeField] private   List<ShoulderAttachment> SerializedShoulderAttachment;
     [SerializeField] private   List<ElbowAttachment> SerializedElbowAttachment;
     [SerializeField] private  List<KneeAttachment> SerializedKneeAttachment;

    public static GameObject maleParts;
    public static List<Decoration> allMaleHair;
    public static List<Decoration> allMaleHead;
    public static List<Decoration> allMaleEyebrows;
    public static List<Decoration> allMaleFacialHair;
    public static List<Torso> allMaleTorso;
    public static List<Arms> allMaleArms;
    public static List<Legs> allMaleLegs;
    
    public static GameObject femaleParts;
    public static List<Decoration> allFemaleHair;
    public static List<Decoration> allFemaleHead;
    public static List<Decoration> allFemaleEyebrows;
    public static List<Decoration> allFemaleFacialHair;
    public static List<Torso> allFemaleTorso;
    public static List<Arms> allFemaleArms;
    public static List<Legs> allFemaleLegs;

    public static GameObject currentGenderParts;
    public static List<Head> allHeadEquipment;
    public static List<BackAttachment> allBackAttachment;
    public static List<ShoulderAttachment> allShoulderAttachment;
    public static List<ElbowAttachment> allElbowAttachment;
    static List<KneeAttachment> allKneeAttachment;

    private void Start() {
     maleParts = SerializedMaleParts;
     allMaleHair = SerializedMaleHair;
     allMaleHead = SerializedMaleHead;
     allMaleEyebrows = SerializedMaleEyebrow;
     allMaleFacialHair = SerializedMaleFacialHair;
     allMaleTorso = SerializedMaleTorso;
     allMaleArms = SerializedMaleArms;
     allMaleLegs = SerializedMaleLegs;
    
     femaleParts = SerializedFemaleParts;
     allFemaleHair = SerializedFemaleHair;
     allFemaleHead = SerializedFemaleHead;
     allFemaleEyebrows = SerializedFemaleEyebrow;
     allFemaleFacialHair = SerializedFemaleFacialHair;
     allFemaleTorso = SerializedFemaleTorso;
     allFemaleArms = SerializedFemaleArms;
     allFemaleLegs = SerializedFemaleLegs;

    currentGenderParts = SerializedCurrentGenderParts;
    allHeadEquipment = SerializedHeadEquipment;
    allBackAttachment = SerializedBackAttachment;
    allShoulderAttachment = SerializedShoulderAttachment;
    allElbowAttachment = SerializedElbowAttachment;
    allKneeAttachment = SerializedKneeAttachment;

    
    }
}
[SerializableAttribute]
public class Decoration{
    public  GameObject modelOfDecoration;
   // public  int idOfDecoration;
}

public enum gender{
    male,
    female
}
[SerializableAttribute]
public class Equipment{
  //  public int elementId;
    public GameObject elementModel;
}
[SerializableAttribute]
public class Armor:Equipment{public int armorBonus;}
[SerializableAttribute]
public class Torso:Armor{public int weight;}
[SerializableAttribute]
public class Arms:Armor{public int impactSpeedMultiplier;}
[SerializableAttribute]
public class Legs:Armor{public int speedMultiplier;}
[SerializableAttribute]
public class Head:Armor{public int speedMultiplier;}
[SerializableAttribute]
public class BackAttachment:Equipment{public int staminaBonus; public int inventorySizeBonus;}
[SerializableAttribute]
public class ShoulderAttachment:Armor{public int damageBonus;}
[SerializableAttribute]
public class ElbowAttachment:Armor{}
[SerializableAttribute]
public class KneeAttachment:Armor{public int staminaBonus;}
}