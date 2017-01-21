using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IsleAttribute {
    
    private int _healthPoint;
        public int HealthPoint
    {
        get { return _healthPoint; }
        set { _healthPoint = value; }
    }

    private int _damageToGive;

    public int DamageToGive
    {
        get { return _damageToGive; }
        set { _damageToGive = value; }
    }

}
