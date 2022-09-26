using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMng : Singleton<SceneMng>
{
    IEnumerator iter;

    // 씬 Enter 이벤트
    public UnityAction<string> SceneEnter;

    // 씬 Exit 이벤트
    public UnityAction<string> SceneExit;

    // 씬 Move 이벤트
    public UnityAction<string> SceneMove;

    // 현재 씬
    public Scene curScene;

    // 로딩 캔버스
    [SerializeField]
    GameObject loadingCanvas;

    GameObject loading;

    protected override void Awake()
    {
        base.Awake();
        // 현재 씬 받아오기
        curScene = SceneManager.GetActiveScene();
        loading = Instantiate(loadingCanvas, transform, false);
        DeActiveLoadingScene(null);
        SceneExit = ActiveLoadingScene;
        SceneEnter = DeActiveLoadingScene;


    }

    void ActiveLoadingScene(string sceneName_Null)
    {
        loading.SetActive(true);
    }

    void DeActiveLoadingScene(string sceneName_Null)
    {
        loading.SetActive(false);
    }

    public void SceneChange(string sceneName)
    {
        iter = LoadYourAsyncScene(sceneName);
        StartCoroutine(iter);
    }

    public void SceneChange(int sceneIndex)
    {
        SceneChange(SceneManager.GetSceneByBuildIndex(sceneIndex).name);
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        SceneExit?.Invoke(curScene.name);
        while (!asyncLoad.isDone)
        {
            SceneMove?.Invoke(sceneName);
            yield return null;
        }
        curScene = SceneManager.GetActiveScene();
        SceneEnter?.Invoke(sceneName);
    }
}
