using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerCustom : NetworkManager {
    public void StartupHost() {
        if (NetworkManager.singleton.isNetworkActive)
            NetworkManager.singleton.StopHost();

        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame() {
        SetIPAdress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAdress() {
        string ipAddress = GameObject.Find("InputFieldIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort() {
        NetworkManager.singleton.networkPort = 7777;
    }

    void OnLevelWasLoaded(int level) {
        if (level == 0) {
            SetupMenuSceneButtons();
        } else if (level == 1) {
            SetupOtherSceneButtons();
        }
    }

    void SetupMenuSceneButtons() {
        GameObject.Find("Button Host").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Button Host").GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("Button Join").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Button Join").GetComponent<Button>().onClick.AddListener(JoinGame);
    }

    void SetupOtherSceneButtons() {
        GameObject.Find("Button Exit Party").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Button Exit Party").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }
}
