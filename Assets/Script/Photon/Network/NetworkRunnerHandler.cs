using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner m_NetworkRunnerPrafab;

    NetworkRunner m_NetworkRunner;

    private void Start()
    {
        m_NetworkRunner = Instantiate(m_NetworkRunnerPrafab);
        m_NetworkRunner.name = "NetworkRunner";

        var clientTask = InitializeNetworkRunner(m_NetworkRunner, GameMode.AutoHostOrClient, NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);
    }


    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode mode, NetAddress address, SceneRef scene, Action<NetworkRunner> initialized)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

        if(sceneManager == null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            Address = address,
            Scene = scene,
            SessionName = "TestRoom",
            Initialized = initialized,
            SceneManager = sceneManager
        });
    }

}
