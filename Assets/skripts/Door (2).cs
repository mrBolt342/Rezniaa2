using JUTPS.JUInputSystem;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Door : MonoBehaviour
{
    [SerializeField] private JUTPSInputControlls input;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (input.Player.Interact.phase == UnityEngine.InputSystem.InputActionPhase.Started)
            {
                transform.localRotation = Quaternion.Euler(0, 90, 0);
            }
        }
    }
}
