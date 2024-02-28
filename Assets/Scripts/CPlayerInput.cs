using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerInput : MonoBehaviour
{
    Vector2 movementDir;
    Vector2 mousePos;

    private DCharacterController cc;

    private Camera cam;

    private void Start()
    {
        cc = this.GetComponent<DCharacterController>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        //Input
        movementDir.x = Input.GetAxis("Horizontal");
        movementDir.y = Input.GetAxis("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        cc.ExecuteCommand(new MoveCommand(movementDir, mousePos)); //Maybe redo this so it isn't sending a command every frame? idk. Stuff's fast anyhow.

        if(Input.GetButton("Fire1"))
        {
            cc.ExecuteCommand(new ShootCommand(true));
        }

        if(Input.GetButtonUp("Fire1"))
        {
            cc.ExecuteCommand(new ShootCommand(false));
        }

    }
}
