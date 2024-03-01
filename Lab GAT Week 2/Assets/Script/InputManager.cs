using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class InputManager : MonoBehaviour
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event StartTouch OnEndTouch;
    #endregion
    
    private PlayerControl playerControl;
    private Camera mainCam;

    void Awake()
    {
        playerControl = new PlayerControl();
        mainCam = Camera.main;
    }
    private void OnEnable()
    {
        playerControl.Enable();
    }
    private void OnDisable()
    {
        playerControl.Disable();
    }
    void Start()
    {
        playerControl.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerControl.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if(OnStartTouch != null)
        {
            OnStartTouch(Utils.ScreenToWorld(mainCam, playerControl.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        }
    }
    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(Utils.ScreenToWorld(mainCam, playerControl.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
        }
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCam, playerControl.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
