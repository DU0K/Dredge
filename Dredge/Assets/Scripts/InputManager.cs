using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;
    //GUI
    protected InputActionMap GUIInputActionMap;
    protected InputAction select;
    //Boat
    protected InputActionMap boatInputActionMap;
    protected InputAction move;
    protected InputAction fish;

    private void OnDisable()
    {
        GUIInputActionMap.Disable();
    }

    private void Start()
    {
        GUIInputActionMap = inputActionAsset.FindActionMap("GUI");
        select = GUIInputActionMap.FindAction("Select");

        boatInputActionMap = inputActionAsset.FindActionMap("Boat");
        move = boatInputActionMap.FindAction("Move");
        fish = boatInputActionMap.FindAction("Fish");

        GUIInputActionMap.Enable();
    }
}
