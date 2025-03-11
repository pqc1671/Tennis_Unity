using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRInputRaycast : Singleton<OVRInputRaycast>
{
    public Raycast raycast;
    public void ToggeActive(bool isShow)
    {
        raycast.IsActive = isShow;
        raycast.line.enabled = isShow;
    }

    // Update is called once per frame
    private void Start()
    {
        ControllerInputManager.Instance.OnRightTriggerDown += OnPressRightTrigger;
    }

    void OnPressRightTrigger()
    {
        if (raycast.raycastingObj != null)
        {
            if (raycast.raycastingObj.GetComponent<ButtonOVR>())
            {
                    raycast.raycastingObj.GetComponent<ButtonOVR>().OnRaycastClick();
            }
        }
    }
}
