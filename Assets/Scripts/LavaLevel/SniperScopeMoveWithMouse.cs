using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// Source Link for this 3rd Party Camera Script: https://www.gamedeveloper.com/programming/rotate-zoom-and-move-your-camera-with-your-mouse-in-unity3d
public class SniperScopeMoveWithMouse : MonoBehaviour
{

    public PlayerInput playerInput;
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 10.0f;
    public float sensitivityY = 10.0f;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -90F;
    public float maximumY = 90F;
    float rotationY = -60F;

    private bool usingMouse = false;

    // For camera movement
    float CameraPanningSpeed = 10.0f;

    private Vector2 rotationDelta;

    public void MouseRightClick()
    {
        // Mouse support
        if (Input.GetAxis("Mouse X") != 0)
        {
            usingMouse = true;

            // Higher sensitivity for mouse for better rotation experience/visuals
            sensitivityX = 10.0f;
            sensitivityY = 10.0f;

            // Disable for mouse as well since there is no need to let the character move or attack after getting on the elevator
            // Will go directly into next scene after golem death
            playerInput.actions.FindAction("Move").Disable();
            playerInput.actions.FindAction("Attack1").Disable();

            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else
        {
            // If the player decides to use the controller midway into the game, this boolean makes it possible
            usingMouse = false;
        }

        // Controller support        
        if (((rotationDelta.x != 0) || (rotationDelta.y != 0)) && !usingMouse)
        {
            // Lower sensitivty for controller for better rotation experience/visuals
            sensitivityX = 3.0f;
            sensitivityY = 3.0f;
            // Need to disable when using thumb sticks for sniper scope camera movement
            // Will go directly into next scene after golem death
            playerInput.actions.FindAction("Move").Disable();
            playerInput.actions.FindAction("Attack1").Disable();

            float rotationX = transform.localEulerAngles.y + rotationDelta.x * sensitivityX;

            rotationY += rotationDelta.y * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        rotationDelta = context.ReadValue<Vector2>();
    }
}