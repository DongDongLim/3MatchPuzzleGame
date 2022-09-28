using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum MapIndex
{
    Title,
    Lobby,
    Game,
};

public class SceneMng : NetworkSceneManagerBase
{
    IEnumerator iter;

    // 로딩 캔버스
    [SerializeField]
    private GameObject loadingCanvas;

    // 씬 Enter 이벤트
    public UnityAction<string> SceneEnter;

    // 씬 Exit 이벤트
    public UnityAction<string> SceneExit;

    // 씬 Move 이벤트
    public UnityAction<string> SceneMove;

    // 현재 씬
    public Scene curScene;


    [Header("Scenes")]
    [SerializeField] private SceneReference _lobby;
    [SerializeField] private SceneReference _game;


    private void Awake()
    {
        // 현재 씬 받아오기
        curScene = SceneManager.GetActiveScene();
        
        DeActiveLoadingScene(null);
        SceneExit = ActiveLoadingScene;
        SceneEnter = DeActiveLoadingScene;
        
    }

    private void Start()
    {
        
    }

    void ActiveLoadingScene(string sceneName_Null)
    {
        loadingCanvas.SetActive(true);
    }

    void DeActiveLoadingScene(string sceneName_Null)
    {
        loadingCanvas.SetActive(false);
    }

    public void SceneChange(string sceneName)
    {
        iter = LoadYourAsyncScene(sceneName);
        StartCoroutine(iter);
    }

    public void SceneChange(int sceneIndex)
    {
        iter = LoadYourAsyncScene(sceneIndex);
        StartCoroutine(iter);
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        SceneExit?.Invoke(curScene.name);
        while (!asyncLoad.isDone)
        {
            SceneMove?.Invoke(curScene.name);
            yield return null;
        }
        curScene = SceneManager.GetActiveScene();
        SceneEnter?.Invoke(sceneName);
    }

    IEnumerator LoadYourAsyncScene(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        SceneExit?.Invoke(curScene.name);
        while (!asyncLoad.isDone)
        {
            SceneMove?.Invoke(curScene.name);
            yield return null;
        }
        curScene = SceneManager.GetActiveScene();
        SceneEnter?.Invoke(curScene.name);
    }

    protected override IEnumerator SwitchScene(SceneRef prevScene, SceneRef newScene, FinishedLoadingDelegate finished)
    {
        Debug.Log($"Switching Scene from {prevScene} to {newScene}");

        SceneExit?.Invoke(curScene.name);

        List<NetworkObject> sceneObjects = new List<NetworkObject>();

        string path;
        switch ((MapIndex)(int)newScene)
        {
            case MapIndex.Lobby: path = _lobby; break;
            case MapIndex.Game: path = _game; break;
            default: Debug.Log("Null Scene"); path = null; break;
        }
        yield return SceneManager.LoadSceneAsync(path, LoadSceneMode.Single);
        curScene = SceneManager.GetSceneByPath(path);
        Debug.Log($"Loaded scene {path}: {curScene}");
        sceneObjects = FindNetworkObjects(curScene, disable: false);

        // Delay one frame
        yield return null;
        finished(sceneObjects);

        Debug.Log($"Switched Scene from {prevScene} to {newScene} - loaded {sceneObjects.Count} scene objects");

        SceneEnter?.Invoke(curScene.name);
    }
}
