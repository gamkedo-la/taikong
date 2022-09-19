using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void ChangeHealth(float points, GameObject unit);

    Transform GetTransform();
}
