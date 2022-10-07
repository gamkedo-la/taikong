using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBehaviour
{
    void LockOnPlayer(Transform t);
    void IgnorePlayer();
    void FireWeapon();
    void DestroySelf();
}