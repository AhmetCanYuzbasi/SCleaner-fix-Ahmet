using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    Vector3 _mousePos;
    Camera _cam;
    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        Aim();
    }

    private void Aim(){
        _mousePos = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 aimDirection = (_mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        if (Time.timeScale == 0f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

    }
}
