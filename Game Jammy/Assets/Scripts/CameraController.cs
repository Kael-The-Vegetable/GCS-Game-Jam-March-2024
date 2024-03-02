using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform followTransform;
    public CameraStates cameraState;
    private Vector3 offset = Vector3.zero;
    private Vector3 _lastPosition = Vector3.zero;
    public enum CameraStates
    {
        Following,
        None
    }
    void Start()
    {
        if (offset == null)
        {
            offset = (followTransform.position - transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void LateUpdate()
    {
        switch (cameraState)
        {
            // Do nothing
            case CameraStates.None:
                break;
            // Follow the Transform object, if it's not null.
            case CameraStates.Following:
                if (followTransform != null) // if it is null, reset and function like CameraState.None
                {
                    // we are just tracking the position, and weould like to follow it from an offset
                    if (offset == Vector3.zero)
                    {
                        offset = (followTransform.position - transform.position);
                    }

                    Vector3 movePosition = Vector3.Slerp(_lastPosition, followTransform.position, 0.5f);

                    transform.position = movePosition - offset;
                    _lastPosition = followTransform.position;
                }
                else // no follow transform, set everything to original state for the next time we get a transform.
                {
                    offset = Vector3.zero;

                }
                break;
        }
    }

}
