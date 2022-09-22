using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChase : MonoBehaviour
{
    public float speed;
    public GameObject PlayerObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, PlayerObject.transform.position, speed * Time.deltaTime);
    }
}
