using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class FirebaseMng : Singleton<FirebaseMng>
{

    private FirebaseAuth m_Auth;

    public void SignIn(string code)
    {
        string authCode;
        authCode = code;
        m_Auth = FirebaseAuth.DefaultInstance;
        Credential credential =
            PlayGamesAuthProvider.GetCredential(authCode);
        m_Auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                return;
            }

            FirebaseUser newUser = task.Result;
            FirebaseUser user = m_Auth.CurrentUser;
            if (user != null)
            {
                string playerName = user.DisplayName;

                // The user's Id, unique to the Firebase project.
                // Do NOT use this value to authenticate with your backend server, if you
                // have one; use User.TokenAsync() instead.
                string uid = user.UserId;
            }

        });

    }
}
