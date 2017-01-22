using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsleAttribute : MonoBehaviour{
    [SerializeField]
    public List<Sprite> spriteList;
    void Start()
    {
        
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[2];
       
       
    }

    void Update()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[2];
    }
    

    private int _healthPoint = 3;
    public int HealthPoint
    {
        get { return _healthPoint; }
        set { _healthPoint = value; }
    }

    private int _damageToGive = 25;

    public int DamageToGive
    {
        get { return _damageToGive; }
        set { _damageToGive = value; }
    }

}
