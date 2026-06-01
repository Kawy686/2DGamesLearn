using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool IsinWall = false;
    [SerializeField]private Vector3 RightLocation;
    [SerializeField]private Vector3 LeftLocation;
    private BoxCollider2D BoxCollider;
    void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) //玩家进入触发范围
    {
        if(collision.CompareTag("Wall"))
        {
            IsinWall = true;//设置Bool为True
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //玩家离开触发范围
    {
        if(collision.CompareTag("Wall"))
        {
            IsinWall = false;//设置Bool为false
        }
    }

    public void UpdateTheOrientation(bool IsRight) //根据玩家朝向更新Box位置
    {
        if(IsRight)
            BoxCollider.transform.localPosition = RightLocation;
        else
            BoxCollider.transform.localPosition = LeftLocation;
    }
}
