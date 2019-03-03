using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public float centery;
    public bool isportrait;
    public List<GameObject> down;
    public List<GameObject> up;
    // Start is called before the first frame update
    void Start()
    {
        if (isportrait)
        {
            centery = transform.Find("center").transform.position.y;
        }
        else
        {
            centery = transform.Find("center").transform.position.x;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "maincharacter")
        {
            if (isportrait)
            {
                if (collision.transform.position.y < centery)
                {
                    for(int i = 0; i < up.Count; i++)
                    {
                        ChangetoTransparent(up[i]);
                        ChangetoReal(down[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < up.Count; i++)
                    {
                        ChangetoTransparent(down[i]);
                        ChangetoReal(up[i]);
                    }
                }
            }
        }
    }

    private void ChangetoTransparent(GameObject obj)
    {
        obj.transform.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.75f);
    }

    private void ChangetoReal(GameObject obj)
    {
        obj.transform.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.75f);
    }
}
