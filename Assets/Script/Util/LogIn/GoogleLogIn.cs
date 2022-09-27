using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GoogleLogIn : MonoBehaviour, ISignIn
{
    [SerializeField]
    GameObject m_SignInBtn;

    public void SignIn()
    {
        // 로그인 결과 받아오기 Todo Facebook SDK 문제 해결후 재활성화
        //PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        SceneMng.instance.SceneChange(1);
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
        }
        else
        {
            ReSignIn();
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

    // 다시 로그인 시도
    public void ReSignIn()
    {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }
}
