using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Transform BeginPos;
    public Transform MBPos;
    private Transform MovePos;
    public float MoveSpeed;
    void Start()
    {
        MovePos = MBPos;
    }

    
    void Update()
    {
        if (Vector2.Distance(transform.position,BeginPos.position)<0.1f)
        {
            MovePos = MBPos;
        }
        
        if(Vector2.Distance(transform.position,MBPos.position)<0.1f)
        {
            MovePos = BeginPos;
        }
        transform.position = Vector2.MoveTowards(transform.position,MovePos.position,MoveSpeed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        collision.transform.parent = this.transform; //玩家进入触发器以后,设置为触发器子项
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.parent = null; //玩家离开触发器以后,设置触发器子项为Null
    }
}
