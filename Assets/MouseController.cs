using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class MouseController : MonoBehaviour
{
    public enum HeroAction { Walk,LookAt,Grab,Speak,UseItem}

    Camera camera;

    public LayerMask movementMask;

    public PlayerMovement movement;

    private void Start()
    {
        camera = Camera.main;
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            //SwitchCase for current action.

            //Move
            if(Physics.Raycast(ray,out hit,100,movementMask))
            {
                Debug.Log("Clicked on " + hit.collider.gameObject.name);

                movement.WalkToPoint(hit.point);
            }
        }
    }
}
