using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject enemyDwarf;
    public GameObject player;
    public int enemiesAtStart;


    private List<GameObject> enemyDwarves = new List<GameObject>();
    private GameObject stage;
    public TextMeshPro gameEndText;
    // Start is called before the first frame update
    void Start()
    {
        stage = GameObject.Find("Stage");
        SpawnEnemies();
       
    }

    private void SpawnEnemies()
    {
        float angle = 0f;
        float newX = Mathf.Cos(angle) * 3;
        float newY = Mathf.Sin(angle) * 3;
        player.transform.position = new Vector3(newX, newY, -.19f);
        float angleAdjust = Mathf.Rad2Deg * (Mathf.PI / (enemiesAtStart + 1));
        for (int x = 1; x <= enemiesAtStart; x++)
        {
            angle += 72f;
            print(angle);
            newX = Mathf.Cos(angle) * 3;
            newY = Mathf.Sin(angle) * 3;
            print(newX + ", " + newY);
            GameObject newDwarf = Instantiate(enemyDwarf);
            newDwarf.transform.position = new Vector3(newX, newY, -.19f);
            enemyDwarves.Add(newDwarf);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyDwarves.Count == 0)
        {

        }
    }
}
