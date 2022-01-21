using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class MouseController : MonoBehaviour
{
    public enum HeroAction { Walk,LookAt,Grab,TalkTo/*,UseItem*/}
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
        if (!UI_Manager.instance.isMessageBoardOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Clicked on " + hit.collider.gameObject.name);
                }

                //SwitchCase for current action.

                //Move
                if (curHeroAction == HeroAction.Walk)
                {
                    if (Physics.Raycast(ray, out hit, 100, movementMask))
                    {
                        Debug.Log("Clicked on " + hit.collider.gameObject.name);

                        movement.WalkToPoint(hit.point);
                    }
                }
                else if (curHeroAction == HeroAction.LookAt)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.GetComponentInParent<Interactable>())
                        {
                            StartCoroutine(UI_Manager.instance.SetMessageOnMessageBoard(hit.collider.gameObject.GetComponentInParent<Interactable>().LookAtThis()));
                        }
                    }
                }
                else if (curHeroAction == HeroAction.Grab)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.GetComponentInParent<Interactable>())
                        {
                            StartCoroutine(UI_Manager.instance.SetMessageOnMessageBoard(hit.collider.gameObject.GetComponentInParent<Interactable>().GrabThis()));
                        }
                    }
                }
                else if (curHeroAction == HeroAction.TalkTo)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.GetComponentInParent<Interactable>())
                        {
                            StartCoroutine(UI_Manager.instance.SetMessageOnMessageBoard(hit.collider.gameObject.GetComponentInParent<Interactable>().TalkToThis()));
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                SetCurHeroAction(curHeroActionIndex + 1);
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(UI_Manager.instance.ContinueMessageOnMessageBoard());
            }   
        }

    }
}
