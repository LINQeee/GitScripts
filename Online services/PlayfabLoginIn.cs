using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
//using StarterAssets;

public class PlayfabLoginIn : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text messageText;
    [SerializeField] private InputField username;
    [SerializeField] private InputField emailInput;
    [SerializeField] private InputField passwordInput;

    [SerializeField] private Canvas loginPage;
    [SerializeField] private Canvas registerPage;
    [SerializeField] private Canvas forgotPasswordPage;
    [SerializeField] private Canvas characterChangePage;
    [SerializeField] private Camera characterChangeCam;
    [SerializeField] private Canvas mainMenuPage;
    [SerializeField] private Camera mainMenuCam;
    [SerializeField] private List<Button> listOfButtons;

    private static Canvas currentPage;
    private bool isMessageShowing;

    // Update is called once per frame
    private void Start() {
      //      ThirdPersonController.changeIsCanMove(false);
    }

    public void RegisterButton() {
        // if(passwordInput.text.Length < 6){
        //     messageShowCoroutine(new Color(1,0,0,0), "Password is too short");
        //     return;
        // }

        var request = new RegisterPlayFabUserRequest{
            Email = emailInput.text,
            Password = passwordInput.text,
            DisplayName = username.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucces, OnError);
    }

    void OnRegisterSucces(RegisterPlayFabUserResult result){
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGameEnter, OnError);
    }

    public void LoginButton() {
        var request = new LoginWithEmailAddressRequest{
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSucces, OnError);
    }

    void OnLoginSucces(LoginResult result){
        if(currentPage == null)currentPage = loginPage;
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGameEnter, OnError);
    }
    public void OnGameEnter(GetUserDataResult result = null){
        if(result.Data.ContainsKey("isHasCharacter") && bool.Parse(result.Data["isHasCharacter"].Value)){
            characterChangeCam.gameObject.SetActive(false);
            mainMenuCam.gameObject.SetActive(true);
            currentPage.gameObject.SetActive(false);
            mainMenuPage.gameObject.SetActive(true);
        }
        else{
            mainMenuCam.gameObject.SetActive(false);
            characterChangeCam.gameObject.SetActive(true);
            currentPage.gameObject.SetActive(false);
            characterChangePage.gameObject.SetActive(true);
        }
    }
    public void ResetPasswordButton() {
            var request = new SendAccountRecoveryEmailRequest{
                Email = emailInput.text,
                TitleId = "2B7BB"
            };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result) {
                foreach(var button in listOfButtons) button.interactable = false;
                StartCoroutine(forgotPasswordWaiting());
    }

    IEnumerator forgotPasswordWaiting(){
        
        StartCoroutine(messageShowCoroutine(new Color(0,1,0,0), "Check Your Email"));
        yield return new WaitUntil(() => !isMessageShowing);
        foreach(var button in listOfButtons) button.interactable = true;
        TransitionToLoginButton();
    }

     void OnError(PlayFabError error){
        StartCoroutine(messageShowCoroutine(new Color(1,0,0,0), error.ErrorMessage));
        Debug.Log(error.ErrorMessage);
    }

    public void TransitionToRegisterButton(){
        if(currentPage == null)currentPage = loginPage;
        messageText.color = new Color(0, 0, 0, 0);
        registerPage.gameObject.SetActive(true);
        currentPage.gameObject.SetActive(false);
        currentPage = registerPage;
    }
    public void TransitionToLoginButton(){
        if(currentPage == null)currentPage = loginPage;
        messageText.color = new Color(0, 0, 0, 0);
        loginPage.gameObject.SetActive(true);
        currentPage.gameObject.SetActive(false);
        currentPage = loginPage;
    }
    public void TransitionToForgotPasswordButton(){
        if(currentPage == null)currentPage = loginPage;
        messageText.color = new Color(0, 0, 0, 0);
        forgotPasswordPage.gameObject.SetActive(true);
        currentPage.gameObject.SetActive(false);
        currentPage = forgotPasswordPage;
    }

    IEnumerator messageShowCoroutine(Color messageColor, string messageContent){
        isMessageShowing = true;
        messageText.color = messageColor;
        messageText.text = messageContent;
moreOpacity:
        messageText.color += new Color(0, 0, 0, 0.005f);
        yield return new WaitForEndOfFrame();
        if(messageText.color.a <= 0.99f) goto moreOpacity;
        yield return new WaitForSecondsRealtime(1.5f);
lessOpacity:
    messageText.color -= new Color(0,0,0,0.005f);
    yield return new WaitForEndOfFrame();
    if(messageText.color.a >= 0.005f) goto lessOpacity;
    isMessageShowing = false;
    }
}