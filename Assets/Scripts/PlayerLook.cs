using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform cameraTransform;

    [Header("Sensibilidad")]
    [SerializeField] private float sensivilityX = 2f;
    [SerializeField] private float sensivilityY = 2f;

    [Header("Límites")]
    [SerializeField] private float maxY = 85f;
    [SerializeField] private float minY = -85f;

    private float currentRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // centrar el cursor

        if(cameraTransform == null) // la camara no ha sido asignada
        {
            Debug.LogError("Aún no se asigno la cámara");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current; // creando un objeto para acceder a la información del movimiento del mouse

        // estructura condicional, sirve para evaluar si una condicion es verdadera o falsa y realizar una tarea dependiendo el estado 
        if (mouse == null || cameraTransform == null)
        {
            return;
        }

        //almacenando el movimiento del mouse en un arreglo de 2 dimensiones [pos_y, pos_y]
        Vector2 mouseDelta = mouse.delta.ReadValue();

        float mouseX = mouseDelta.x * sensivilityX;
        float mouseY = mouseDelta.y * sensivilityY;

        // Yaw
        transform.Rotate(Vector3.up, mouseX);

        //Pitch
        currentRotation -= mouseY;
        currentRotation = Mathf.Clamp(currentRotation, minY, maxY); // reestringiendo el movimiento de rotacion
        cameraTransform.localRotation = Quaternion.Euler(currentRotation, 0f, 0f);

     }
}
