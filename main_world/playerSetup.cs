using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using StarterAssets;
using UnityEngine.InputSystem;
public class playerSetup : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer){
            foreach(Transform trans in transform){
                trans.gameObject.SetActive(true);
                if(trans.name == "PlayerArmature"){
                    trans.GetComponent<ThirdPersonController>().enabled = true;
                    trans.GetComponent<FightingMechanic>().enabled = true;
                    trans.GetComponent<PlayerInput>().enabled = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
