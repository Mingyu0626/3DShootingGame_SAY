using UnityEngine;

public class CameraStateContext
{
    private ICameraState _currentState;
    public ICameraState CurrentState { get => _currentState; set => _currentState = value; }
    private CameraController _cameraController;
    public CameraStateContext(CameraController controller)
    {
        _cameraController = controller;
    }
    public void ChangeState()
    {
        _currentState = new CameraFPSState();
        _currentState.Enter(_cameraController);
    }
    public void ChangeState(ICameraState newState)
    {
        if (!ReferenceEquals(_currentState, null))
        {
            _currentState.Exit();
        }
        _currentState = newState;
        _currentState.Enter(_cameraController);
    }

}
