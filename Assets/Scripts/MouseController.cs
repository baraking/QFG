using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class MouseController : MonoBehaviour
{
    public enum HeroAction { Walk,LookAt,Grab,Speak/*,UseItem*/}
    public HeroAction curHeroAction;
    public int curHeroActionIndex;
    public Texture2D[] heroActionIcons;

    Camera camera;

    public LayerMask movementMask;

    public PlayerMovement movement;

    private void Start()
    {
        camera = Camera.main;
        movement = GetComponent<PlayerMovement>();

        SetCurHeroAction((int)HeroAction.Walk);
    }

    private void SetCurHeroAction(int heroActionIndex)
    {
        //if index = useItem && curItem == null
        //index++

        curHeroActionIndex = heroActionIndex%System.Enum.GetValues(typeof(HeroAction)).Length;
        curHeroAction = (HeroAction)curHeroActionIndex;

        Cursor.SetCursor(heroActionIcons[curHeroActionIndex], Vector2.zero, CursorMode.Auto);

        Debug.Log("Action is " + curHeroAction.ToString());
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

                if (curHeroAction == HeroAction.Walk)
                {
                    movement.WalkToPoint(hit.point);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            SetCurHeroAction(curHeroActionIndex + 1);
        }
    }
}
