using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{

    private Collider2D stageBounds;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        stageBounds = GetComponent<Collider2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        print("Exit");
        if(other.gameObject.CompareTag("Entity") && other.gameObject.GetComponent<DCharacterController>() != null)
        {
            print("Player Fell");
            DCharacterController absentCharacter = other.gameObject.GetComponent<DCharacterController>();
            absentCharacter.Fall();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
