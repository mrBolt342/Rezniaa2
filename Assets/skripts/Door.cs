using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2f;
    [SerializeField] private float interactionDistance = 2f;

    [Header("Input")]
    [SerializeField] private InputActionReference interactAction;

    [Header("Audio")]
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isOpen = false;
    private AudioSource audioSource;
    private Transform player;
    private bool isPlayerInRange = false;

    private void Awake()
    {
        initialRotation = transform.rotation;
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        if (interactAction != null)
        {
            interactAction.action.Enable();
            interactAction.action.performed += OnInteractPerformed;
        }
    }

    private void OnDisable()
    {
        if (interactAction != null)
        {
            interactAction.action.performed -= OnInteractPerformed;
        }
    }

    private void Update()
    {
        // ѕровер€ем рассто€ние до игрока
        float distance = Vector3.Distance(transform.position, player.position);
        bool nowInRange = distance <= interactionDistance;

        // ≈сли состо€ние изменилось, можно добавить визуальную подсказку
        if (nowInRange != isPlayerInRange)
        {
            isPlayerInRange = nowInRange;
            // «десь можно добавить UI подсказку (например, показать/скрыть "Press E to interact")
        }

        // ѕлавное вращение двери
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, openSpeed * Time.deltaTime);
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (isPlayerInRange)
        {
            ToggleDoor();
        }
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            targetRotation = initialRotation * Quaternion.Euler(0, 0, openAngle);
            PlaySound(openSound);
        }
        else
        {
            targetRotation = initialRotation;
            PlaySound(closeSound);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}