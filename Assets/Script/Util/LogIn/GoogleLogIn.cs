using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GoogleLogIn : MonoBehaviour
{
    [SerializeField]
    GameObject sussessBtn;

    [SerializeField]
    GameObject failsBtn;


    public void LogInBtn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }



    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            PlayGamesPlatform.Instance.RequestServerSideAccess(
            /* forceRefreshToken= */
            false,
            FirebaseMng.instance.SignIn
            );
            sussessBtn.SetActive(true);
        }
        else
        {
            failsBtn.SetActive(true);
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }
}
