using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerInputManager : Singleton<ControllerInputManager>
{
    /*public static ControllerInputManager Instance { get; private set; }*/

    // Các sự kiện của tay trái
    public event Action OnLeftTriggerDown;
    public event Action OnLeftPrimaryButtonDown;
    public event Action OnLeftSecondaryButtonDown;

    // Các sự kiện của tay phải
    public event Action OnRightTriggerDown;
    public event Action OnRightPrimaryButtonDown;
    public event Action OnRightSecondaryButtonDown;

    // Lưu trạng thái của các phím ở frame trước
    private bool _leftTriggerPrev = false;
    private bool _rightTriggerPrev = false;
    private bool _leftPrimaryPrev = false;
    private bool _rightPrimaryPrev = false;
    private bool _leftSecondaryPrev = false;
    private bool _rightSecondaryPrev = false;

    // Ngưỡng kích hoạt cho các phím analog (trigger, grip, …)
    private const float triggerThreshold = 0.5f;

    List<InputDevice> leftDevices = new List<InputDevice>();
    List<InputDevice> rightDevices = new List<InputDevice>();

    private void OnDestroy()
    {
        OnLeftPrimaryButtonDown = null;
    }

    private void Update()
    {
        UpdateLeftController();
        UpdateRightController();
    }

    private void UpdateLeftController()
    {
        /*List<InputDevice> leftDevices = new List<InputDevice>();*/
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, leftDevices);

        // Duyệt qua tất cả thiết bị tay trái hiện có
        bool leftTrigger = false;
        bool leftPrimary = false;
        bool leftSecondary = false;
        foreach (var device in leftDevices)
        {
            // Trigger (sử dụng analog value)
            if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                if (triggerValue > triggerThreshold)
                    leftTrigger = true;
            }
            // Primary Button (ví dụ: A/X)
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            {
                leftPrimary = true;
            }
            // Secondary Button (ví dụ: B/Y)
            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
            {
                leftSecondary = true;
            }
        }

        // Kiểm tra sự kiện nhấn xuống cho trigger
        if (leftTrigger && !_leftTriggerPrev)
        {
            OnLeftTriggerDown?.Invoke();
        }
        // Kiểm tra sự kiện nhấn xuống cho primary button
        if (leftPrimary && !_leftPrimaryPrev)
        {
            OnLeftPrimaryButtonDown?.Invoke();
        }
        // Kiểm tra sự kiện nhấn xuống cho secondary button
        if (leftSecondary && !_leftSecondaryPrev)
        {
            OnLeftSecondaryButtonDown?.Invoke();
        }

        // Cập nhật trạng thái frame trước
        _leftTriggerPrev = leftTrigger;
        _leftPrimaryPrev = leftPrimary;
        _leftSecondaryPrev = leftSecondary;
    }

    private void UpdateRightController()
    {
        /*List<InputDevice> rightDevices = new List<InputDevice>();*/
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, rightDevices);

        bool rightTrigger = false;
        bool rightPrimary = false;
        bool rightSecondary = false;
        foreach (var device in rightDevices)
        {
            if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                if (triggerValue > triggerThreshold)
                    rightTrigger = true;
            }
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            {
                rightPrimary = true;
            }
            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
            {
                rightSecondary = true;
            }
        }

        if (rightTrigger && !_rightTriggerPrev)
        {
            OnRightTriggerDown?.Invoke();
        }
        if (rightPrimary && !_rightPrimaryPrev)
        {
            OnRightPrimaryButtonDown?.Invoke();
        }
        if (rightSecondary && !_rightSecondaryPrev)
        {
            OnRightSecondaryButtonDown?.Invoke();
        }

        _rightTriggerPrev = rightTrigger;
        _rightPrimaryPrev = rightPrimary;
        _rightSecondaryPrev = rightSecondary;
    }
}
