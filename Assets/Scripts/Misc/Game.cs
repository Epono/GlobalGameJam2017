using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Misc {
    public class Game : MonoBehaviour {

        [SerializeField]
        private List<Button> _buttonsExitGame;

        //[SerializeField]
        //NetworkManager manager;

        void Awake() {
            if (Application.isWebPlayer || Application.isEditor)
                _buttonsExitGame.ForEach(buttonExitGame => buttonExitGame.interactable = false);
        }

        public void OnHostGame() {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            //Debug.Log(SceneManager.GetActiveScene().name);
            //SceneManager.sceneUnloaded(SceneManager.GetActiveScene());
        }

        public void OnJoinGame() {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            //manager.StartClient();
            //Debug.Log(SceneManager.GetActiveScene().name);
            //SceneManager.sceneUnloaded(SceneManager.GetActiveScene());
        }

        public void OnExitGame() {
            Application.Quit();
        }
    }
}
