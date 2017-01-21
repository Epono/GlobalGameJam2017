using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class ObstacleIsleScript : ObstacleTemplateScript {

    public int ID ;
    [SerializeField]
    List<Sprite> sprites;

    [SerializeField]
    List<GameObject> prefabs;

    

    public RuntimeAnimatorController forcedAnimator;

    // Use this for initialization
    void Start() {
      
        //Debug.Log("tamere");
        //int randomAnimation = Random.Range(0, 4);
        //gameObject.GetComponent<SpriteRenderer>().sprite = sprites[randomAnimation];
        //if (forcedAnimator == null)
        //{
        //    gameObject.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController) Resources.Load("Animations/Isle_" + (randomAnimation + 1) + "_animator");
        //}
        //else
        //{
        //    gameObject.GetComponent<Animator>().runtimeAnimatorController =  forcedAnimator;
        //}
    }

    // Update is called once per frame
    void Update() {

    }
}
