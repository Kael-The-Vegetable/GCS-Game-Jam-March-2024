using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform followTransform;
    public CameraStates cameraState;
    private Vector3 offset;
    public enum CameraStates
    {
        Following,
        None
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (cameraState)
        {
            case CameraStates.None:
                break;
            case CameraStates.Following:
                if (followTransform != null)
                {
                    offset =  followTransform.position - transform.position;
                    transform.position = offset;
                }
                break;
        }
    }
}
