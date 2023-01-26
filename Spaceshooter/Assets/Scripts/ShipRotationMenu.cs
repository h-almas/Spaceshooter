using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotationMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,45 * Time.deltaTime);
    }
}
