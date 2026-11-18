using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private float sideways = 0f;
    private float vertical = 0f;
    
    void Update()
    {
        if (sideways != 0)
        {
            Vector3 movement = new Vector3(sideways, 0f, 0f);
            transform.Translate(movement * Time.deltaTime * 5f);
        }
        if (vertical != 0)
        {
            Vector3 movement = new Vector3(0f, vertical, 0f);
            transform.Translate(movement * Time.deltaTime * 5f);
        }
    }

    public void OnVertical(InputAction.CallbackContext context)
    {
        vertical = context.ReadValue<float>();
    }

    public void OnSidways(InputAction.CallbackContext context)
    {
        sideways = context.ReadValue<float>();
    }
}
