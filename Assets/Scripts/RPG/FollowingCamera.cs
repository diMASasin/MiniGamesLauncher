using System;
using RPG;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] public Transform _generalViewPosition;
    [field: SerializeField] public int MinPosition { get; private set; } = -65;
    [field: SerializeField] public int MaxPosition { get; set; } = -10;
    [field: SerializeField] public FollowingObject FollowingObject { get; private set; }

    private Transform _target;
    private ICameraInput _cameraInput;

    public bool HasTarget => _target != null;

    private Vector3 CameraPosition
    {
        get => _camera.transform.position;
        set => _camera.transform.position = value;
    }

    public void Init(ICameraInput cameraInput)
    {
        _cameraInput = cameraInput;
    }

    private void Update()
    {
        if (_cameraInput == null) return;

        TryZoom(_cameraInput.GetScrollDeltaY());
        TryStopZoom();
    }

    public void SetTarget(Transform target) => FollowingObject.Follow(target);

    public void RemoveTarget(Transform target)
    {
    }

    public void RemoveAllTargets() => FollowingObject.StopFollow();

    public void MoveToGeneralView() => SetTarget(_generalViewPosition);

    public void ResetZoom()
    {
    }

    public void TryZoom(float scrollDeltaY)
    {
        if (scrollDeltaY < 0 && CameraPosition.z > MinPosition ||
            scrollDeltaY > 0 && CameraPosition.z < MaxPosition)
            CameraPosition += new Vector3(0, 0, scrollDeltaY);

        float newPositionZ = Mathf.Clamp(CameraPosition.z, MinPosition, MaxPosition);

        CameraPosition = new Vector3(CameraPosition.x, CameraPosition.y, newPositionZ);
    }

    private void TryStopZoom()
    {
        if (FollowingObject.FreezeZPosition == true)
            return;

        float targetPositionZ;
        if (FollowingObject.FollowingFor == null)
            targetPositionZ = FollowingObject.MoveTo.z;
        else
            targetPositionZ = FollowingObject.FollowingFor.position.z;

        float tolerance = 0.2f;
        float positionZ = transform.position.z - FollowingObject.Offset.z;

        if (Math.Abs(positionZ - targetPositionZ) < 2 ||
            Math.Abs(positionZ - MaxPosition) < tolerance ||
            Math.Abs(positionZ - MinPosition) < tolerance)
        {
            FollowingObject.StopFollowZPosition();
        }
    }
}

public interface ICameraInput
{
    float GetScrollDeltaY();
}