using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : DDOLSingleton<TimeManager>
{
    private struct Runmethod
    {
        public System.Action method;//委托方法
        public int type;            //托管种类（根据时间&根据布尔值）
        public float begintime;     //开始时间
        public float time;          //持续时间
    }
    private List<Runmethod> runmethods;//托管方法组

    const int booltype = 0;
    const int timetype = 1;

    private void Awake()
    {
        runmethods = new List<Runmethod>();
    }

    private void Update()
    {
        RunSimultaneous();
    }

    //托管运行方法
    public void RunSimultaneous()
    {
        for(int i = 0; i < runmethods.Count; i++)
        {
            if (runmethods[i].type == timetype && Time.time - runmethods[i].begintime >= runmethods[i].time)
            {
                runmethods.RemoveAt(i);
            }
        }
        for (int i = 0; i < runmethods.Count; i++)
        {
            runmethods[i].method();
        }
    }

    //开启托管（时间控制）
    public void RunMethod(System.Action action,float time)
    {
        Runmethod runmethod = new Runmethod();
        runmethod.method = action;
        runmethod.begintime = Time.time;
        runmethod.time = time;
        runmethod.type = timetype;
        runmethods.Add(runmethod);

    }

    //开启托管（布尔值控制）
    public void RunMethod(System.Action action)
    {
        Runmethod runmethod = new Runmethod();
        runmethod.method = action;
        runmethod.type = booltype;
        runmethods.Add(runmethod);
    }

    //关闭托管（布尔值控制）
    public void CloseMethod(System.Action action)
    {
        for(int i = 0; i < runmethods.Count; i++)
        {
            if (runmethods[i].method == action)
            {
                runmethods.RemoveAt(i);
                return;
            }
        }
    }
}
