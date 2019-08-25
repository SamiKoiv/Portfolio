using UnityEngine;

// For objects that are to be used with another object, like using car with gas canister to fill the tank.
public interface ICombinable
{
    void Combine(Rigidbody rb);
    Transform GetTransform();
    Vector3 GetPopupOffset();
    string GetCombinationMessage();
}
