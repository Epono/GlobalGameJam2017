using UnityEngine;
using UnityEngine.UI;

public class prefIP : MonoBehaviour {
    [SerializeField]
    private InputField _ip;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("IP")) {
            _ip.text = PlayerPrefs.GetString("IP");
        } else {
            PlayerPrefs.SetString("IP", _ip.text);
        }
    }
	
	// Update is called once per frame
	void Update () {
        PlayerPrefs.SetString("IP", _ip.text);
    }
}
