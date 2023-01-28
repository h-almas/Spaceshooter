using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public interface Enemy
{
    public int GetPower();
    public void GetDamage(int damage);
}


