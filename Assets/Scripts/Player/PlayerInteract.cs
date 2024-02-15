using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private Camera Cam;
    [SerializeField] public float Distance=3f;
    [SerializeField] private LayerMask mask;

    private PlayerUI PlayerUI;
    private InputManager inputManager;


    // Start is called before the first frame update
    void Start()
    {
        Cam = GetComponent<PlayerLook>().Cam;
        PlayerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerUI.UpdateText(string.Empty);
        Ray ray = new Ray(Cam.transform.position,Cam.transform.forward);

        Debug.DrawRay (ray.origin,ray.direction * Distance);

        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo,Distance,mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                PlayerUI.UpdateText(interactable.PromptMessage);
                if (inputManager.Onfoot.Interact.triggered)
                {

                }
            }
        }
    }
}
