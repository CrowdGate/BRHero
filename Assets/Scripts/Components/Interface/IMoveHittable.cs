using UnityEngine;

public interface IMoveHittable
{
    void OnMoveStart();
    void OnMoving();
    void OnMoveEnd();
}
