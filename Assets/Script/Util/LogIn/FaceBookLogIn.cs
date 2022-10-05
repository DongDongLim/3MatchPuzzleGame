using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FaceBookLogIn : MonoBehaviour
{
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            // 초기화
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // 앱 활성화
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // 초기화 성공시
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // 게임 일시중지
            Time.timeScale = 0;
        }
        else
        {
            // 게임 일시중지해제
            Time.timeScale = 1;
        }
    }

    public void facebookLogin()
    {
        // 읽어올 권한을 설정
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        // 로그인 성공
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User IDDebug.Log(aToken);
            string facebookToken = aToken.TokenString;
            // 파이어베이스에 페이스북 토큰으로 가입 요청
            FirebaseMng.instance.SignIn(facebookToken);

        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }
}
