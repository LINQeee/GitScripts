using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingMechanic : MonoBehaviour
{
    [SerializeField] private GameObject swordAttachment;
    [SerializeField] private GameObject shieldAttachment;

    // Update is called once per frame
    void Update()
    {
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                if(!GetComponent<Animator>().GetBool("isWithWeapon")){
                    GetComponent<Animator>().SetBool("isWithWeapon", true);
                    swordAttachment.transform.GetChild(0).gameObject.SetActive(true);
                    shieldAttachment.transform.GetChild(0).gameObject.SetActive(true);
                }
                else{
                    GetComponent<Animator>().SetBool("isWithWeapon", false);
                    swordAttachment.transform.GetChild(0).gameObject.SetActive(false);
                    shieldAttachment.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            
    }
}
