using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform followTransform;
    public Transform menuTransform;
    public CameraStates cameraState;
    public Vector3 offset = Vector3.zero;
    private Vector3 _lastPosition = Vector3.zero;
    private Transform _originalTransform;
    private CameraStates _lastCameraState;
    
    public enum CameraStates
    {
        Following,
        Menu,
        None
    }
    void Start()
    {
        _originalTransform = transform;
       
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void LateUpdate()
    {
        if (cameraState != CameraStates.Menu && _lastCameraState == CameraStates.Menu)
        {
            transform.transform.position = _originalTransform.position;
            transform.rotation = _originalTransform.rotation;
        }

        switch (cameraState)
        {
            // Do nothing
            case CameraStates.None:
                break;
            // Follow the Transform object, if it's not null.
            case CameraStates.Following:
                if (followTransform != null) // if it is null, reset and function like CameraState.None
                {
                    Vector3 movePosition = Vector3.Slerp(_lastPosition, followTransform.position, 0.5f);

                    transform.position = movePosition - offset;
                    _lastPosition = followTransform.position;
                }
                else // no follow transform, set everything to original state for the next time we get a transform.
                {
                    offset = Vector3.zero;

                }
                break;
            case CameraStates.Menu:
                transform.transform.position = menuTransform.transform.position;
                transform.LookAt(menuTransform.forward);
                break;
        }
        _lastCameraState = cameraState;
    }

}
