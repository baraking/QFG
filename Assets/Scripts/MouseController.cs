using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMovement))]
public class MouseController : MonoBehaviour
{
    public static MouseController instance;

    public enum HeroAction { Walk,LookAt,Grab,TalkTo,UseItem}
    public HeroAction curHeroAction;
    public int curHeroActionIndex;
    public Texture2D[] heroActionIcons;

    public InventoryItem curItem;

    Camera camera;

    public LayerMask movementMask;

    public PlayerMovement movement;

    public void Awake()
    {
        if (instance != null)
        {
            return;
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        camera = Camera.main;
        movement = GetComponent<PlayerMovement>();

        SetCurHeroAction((int)HeroAction.Walk);
    }

    public void SetCurHeroAction(int heroActionIndex)
    {
        if (heroActionIndex == (int)HeroAction.UseItem && ReferenceEquals(curItem, null))
        {
            heroActionIndex++;
        }  

        if (UI_Manager.instance.isInventoryOpen)
        {
            while (heroActionIndex == (int)HeroAction.TalkTo || heroActionIndex == (int)HeroAction.Walk || (heroActionIndex == (int)HeroAction.UseItem && ReferenceEquals(curItem, null)))
            {
                heroActionIndex++;
                heroActionIndex = heroActionIndex % System.Enum.GetValues(typeof(HeroAction)).Length;
            }
        }

        curHeroActionIndex = heroActionIndex % System.Enum.GetValues(typeof(HeroAction)).Length;
        curHeroAction = (HeroAction)curHeroActionIndex;

        if (curHeroAction != HeroAction.UseItem)
        {
            Cursor.SetCursor(heroActionIcons[curHeroActionIndex], Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(curItem.myIcon.sprite.texture, Vector2.zero, CursorMode.Auto);
            print("Put icon for " + curItem.item.name);
        }

        Debug.Log("Action is " + curHeroAction.ToString());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HeroActionsManager.instance.isCasting = !HeroActionsManager.instance.isCasting;
        }

        if (!EventSystem.current.IsPointerOverGameObject() && !UI_Manager.instance.IsUIManagerActive())
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
                            if(Vector3.Distance(transform.position, hit.collider.ClosestPointOnBounds(transform.position)) <= hit.collider.gameObject.GetComponentInParent<Interactable>().lookAtDistance)
                            {
                                StartCoroutine(UI_Manager.instance.SetMessageOnMessageBoard(hit.collider.gameObject.GetComponentInParent<Interactable>().LookAtThis()));
                            }
                            else
                            {
                                Debug.Log("I'm too far for that");
                                movement.WalkToPoint(hit.point);
                            }
                        }
                    }
                }
                else if (curHeroAction == HeroAction.Grab)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.GetComponentInParent<Interactable>())
                        {
                            if (Vector3.Distance(transform.position, hit.collider.ClosestPointOnBounds(transform.position)) <= hit.collider.gameObject.GetComponentInParent<Interactable>().grabDistance)
                            {
                                StartCoroutine(UI_Manager.instance.SetMessageOnMessageBoard(hit.collider.gameObject.GetComponentInParent<Interactable>().GrabThis()));
                            }
                            else
                            {
                                Debug.Log("I'm too far for that");
                                movement.WalkToPoint(hit.point);
                            }
                        }
                    }
                }
                else if (curHeroAction == HeroAction.TalkTo)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.GetComponentInParent<Interactable>())
                        {
                            if (Vector3.Distance(transform.position, hit.collider.ClosestPointOnBounds(transform.position)) <= hit.collider.gameObject.GetComponentInParent<Interactable>().talkToDistance)
                            {
                                StartCoroutine(UI_Manager.instance.SetMessageOnMessageBoard(hit.collider.gameObject.GetComponentInParent<Interactable>().TalkToThis()));
                            }
                            else
                            {
                                Debug.Log("I'm too far for that");
                                movement.WalkToPoint(hit.point);
                            }
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                SetCurHeroAction(curHeroActionIndex + 1);
            }

        }
        else if (UI_Manager.instance.isInventoryOpen)
        {
            if (Input.GetMouseButtonDown(0) && UI_Manager.instance.isMessageBoardOpen)
            {
                StartCoroutine(UI_Manager.instance.ContinueMessageOnMessageBoard());
            }
            else if (Input.GetMouseButtonDown(1))
            {
                SetCurHeroAction(curHeroActionIndex + 1);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && UI_Manager.instance.isMessageBoardOpen)
            {
                StartCoroutine(UI_Manager.instance.ContinueMessageOnMessageBoard());
            }   
        }

    }
}
