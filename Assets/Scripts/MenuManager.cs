using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
