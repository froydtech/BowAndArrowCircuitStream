using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;
using UnityEngine.Events;

public class SetCorrectCameraHeight : MonoBehaviour
{
    enum TrackingSpace
    {
        Stationary,
        RoomScale
    }

    List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();


    [Header("Camera Settings")]

    [SerializeField]
    [Tooltip("Decide if experience is Room Scale or Stationary. Note this option does nothing for mobile VR experiences, these experience will default to Stationary")]
    TrackingSpace m_TrackingSpace = TrackingSpace.Stationary;

    [SerializeField]
    [Tooltip("Camera Height - overwritten by device settings when using Room Scale ")]
    float m_StationaryCameraYOffset = 1.36144f;

    [SerializeField]
    [Tooltip("GameObject to move to desired height off the floor (defaults to this object if none provided)")]
    public GameObject m_CameraFloorOffsetObject;

    void Awake()
    {
       

    }

    void Start()
    {
        SetCameraHeight();
    }

    void SetCameraHeight()
    {
        float cameraYOffset = m_StationaryCameraYOffset;
        SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);

        if (m_TrackingSpace == TrackingSpace.Stationary)
        {
            //XRDevice.SetTrackingSpaceType(TrackingSpaceType.Stationary);
            //XRDevice.SetTrackingSpaceType(TrackingSpace.Stationary);
            //XRInputSubsystem.TrySetTrackingOriginMode();
            for (int i = 0; i < subsystems.Count; i++)
            {
                subsystems[i].TrySetTrackingOriginMode(TrackingOriginModeFlags.Device);
                subsystems[i].TryRecenter();
            }
            // make the user face to the forward direction
//            InputTracking.Recenter();
        }

        // if on a room-scale experience, we disregard the height the user entered
        else if (m_TrackingSpace == TrackingSpace.RoomScale)
        {
            for (int i = 0; i < subsystems.Count; i++)
            {
                subsystems[i].TrySetTrackingOriginMode(TrackingOriginModeFlags.Floor);
            }
        }

        //Move floor offset to correct height
        if (m_CameraFloorOffsetObject)
            m_CameraFloorOffsetObject.transform.localPosition = new Vector3(m_CameraFloorOffsetObject.transform.localPosition.x, cameraYOffset, m_CameraFloorOffsetObject.transform.localPosition.z);
    }
}

