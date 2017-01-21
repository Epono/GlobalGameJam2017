using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class ObstacleIsleScript : ObstacleTemplateScript {

    [SerializeField]
    List<Sprite> sprites;

    // Use this for initialization
    void Start() {
        int randomAnimation = Random.Range(0, 4);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[randomAnimation];
        gameObject.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animations/Isle_" + (randomAnimation + 1) + "_animator");
    }

    // Update is called once per frame
    void Update() {

    }
}
