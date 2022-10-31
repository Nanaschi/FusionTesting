using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VanillaTutorial
{
    public class NetworkRunnerHandler : MonoBehaviour
    {
        private NetworkRunner _runner;

        async void StartGame(GameMode mode)
        {
            // Create the Fusion runner and let it know that we will be providing user input
            _runner = gameObject.AddComponent<NetworkRunner>();
            _runner.ProvideInput = true;

            // Start or join (depends on gamemode) a session with a specific name
            await _runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = "TestRoom",
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });}
    }
}
