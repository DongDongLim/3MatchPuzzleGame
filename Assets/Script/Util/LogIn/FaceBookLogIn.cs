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
            // �ʱ�ȭ
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // �� Ȱ��ȭ
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // �ʱ�ȭ ������
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
            // ���� �Ͻ�����
            Time.timeScale = 0;
        }
        else
        {
            // ���� �Ͻ���������
            Time.timeScale = 1;
        }
    }

    public void facebookLogin()
    {
        // �о�� ������ ����
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        // �α��� ����
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User IDDebug.Log(aToken);
            string facebookToken = aToken.TokenString;
            // ���̾�̽��� ���̽��� ��ū���� ���� ��û
            FirebaseMng.instance.SignIn(facebookToken);

        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }
}
