using UnityEngine;

public interface ICameraState
{
    public void Enter(CameraController playerController);
    public void Update();
    public void Exit();
}
