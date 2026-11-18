using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;
    //GUI
    private InputActionMap GUIInputActionMap;
    private InputAction select;
    //Boat
    private InputActionMap boatInputActionMap;
    private InputAction move;
    private InputAction fish;

    private void Start()
    {
        GUIInputActionMap = inputActionAsset.FindActionMap("GUI");
        select = GUIInputActionMap.FindAction("Select");

        boatInputActionMap = inputActionAsset.FindActionMap("Boat");
        move = boatInputActionMap.FindAction("Move");
        fish = boatInputActionMap.FindAction("Fish");
    }
}
