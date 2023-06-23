using System;
using UnityEngine;

public class ButtonHolder : MonoBehaviour
{
    [SerializeField] private int ButtonKeyCode;
    [SerializeField] public bool Enabled = true;

    private float _holdDownStartTime;
    public ButtonHolder(int buttonKeyCode)
    {
        ButtonKeyCode = buttonKeyCode;
    }

    private bool _isHeld = false;
    public event Action<float> OnHoldComplete;
    public event Action<float> OnHolding;

    void Update()
    {
        Debug.Log("hold update");

        if (!Enabled) return;

        if (Input.GetMouseButtonDown(ButtonKeyCode))
        {
            _holdDownStartTime = Time.time;
            _isHeld = true;
        }

        if(_isHeld)
        {
            var heldTime = Time.time - _holdDownStartTime;
            OnHolding?.Invoke(heldTime);
        }

        if (Input.GetMouseButtonUp(ButtonKeyCode))
        {
            _isHeld = false;
            var heldTime = Time.time - _holdDownStartTime;
            OnHoldComplete?.Invoke(heldTime);
        }
    }

    public static ButtonHolder LeftMouseButton => new(0);
}