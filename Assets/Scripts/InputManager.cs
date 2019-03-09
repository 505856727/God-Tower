using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : DDOLSingleton<InputManager>
{
    //根据标签筛选物体
    public GameObject InputRadial(bool state,string hitobject)
    {
        if (state)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.tag == hitobject)
                {
                    return hits[i].collider.gameObject;
                }
            }
        }
        return null;
    }

    //根据物体本身筛选物体
    public GameObject InputRadial(bool state,GameObject hitobject)
    {
        if (state)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider == hitobject)
                {
                    return hits[i].collider.gameObject;
                }
            }
        }
        return null;
    }
}
