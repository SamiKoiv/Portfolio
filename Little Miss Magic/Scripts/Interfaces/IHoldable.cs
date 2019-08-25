using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldable
{
    void TakeHold();
    void Throw(Vector3 throwForce);
    void Drop();
}
