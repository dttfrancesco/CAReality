using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class SpeedBuff : PowerUpEffect
{
    [SerializeField] private float Maxamount;
    [SerializeField] private float Accamount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Waypoints>().maxSpeed += Maxamount;
        target.GetComponent<Waypoints>().acceleration += Accamount;
    }
}
