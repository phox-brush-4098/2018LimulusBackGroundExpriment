using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMot
{
    public int freez;
    int Cnt = 0;
    public int CntMax;

    // trueだったら破棄
    public bool Run(Camera arg)
    {
        if (freez > 0) { --freez; return false; }
        RunMain(arg);
        ++Cnt;
        return Cnt > CntMax;
    }

    protected virtual void RunMain(Camera arg) { }
}

public class CMMove : CamMot
{
    public Vector3 spd, spdAdd;
    public float spdExt = 1f, spdAddExt = 1f;

    protected override void RunMain(Camera arg)
    {
        arg.transform.position = arg.transform.position + spd;
        spd *= spdExt;
        spd += spdAdd;
        spdAdd *= spdAddExt;
    }
}

public class CMRot : CamMot
{
    public Vector3 spd, spdAdd;
    public float spdExt = 1f, spdAddExt = 1f;

    protected override void RunMain(Camera arg)
    {
        var tmp = arg.transform.localRotation.eulerAngles;
        tmp += spd;
        arg.transform.localRotation = Quaternion.Euler(tmp);
        spd *= spdExt;
        spd += spdAdd;
        spdAdd *= spdAddExt;
    }
}

public class V3MValueProgram
{
    protected float value = 0f;
    protected int cnt = 0;
    public virtual void Incr() { ++cnt; }
    public virtual float GetValue() { return value; }
}

public class V3MVPLinear : V3MValueProgram
{
    public int cntMax;

    public override void Incr()
    {
        value = (float)(cnt) / cntMax;
        base.Incr();
    }
}

public class Vec3Motion
{
    public Vector3 begin, end;
    public V3MValueProgram program;
    public float value;  // 0f(begin) - 1f(end)

    public void IncrProgram() {  }
    public virtual Vector3 GetValue() { return begin; }
}

public class V3MLinear : Vec3Motion
{
    public override Vector3 GetValue()
    {
        return begin * (1f - value) + end * value;
    }
}

public class BGCameraMotion : MonoBehaviour
{
    public Camera cam;
    public static BGCameraMotion gi { get; private set; }
    List<CamMot> alive, trash;

    public void Reset()
    {
        alive.Clear();
        trash.Clear();
    }

    public void AddAlive(CamMot arg)
    {
        alive.Add(arg);
    }

    void Awake()
    {
        alive = new List<CamMot>();
        trash = new List<CamMot>();
        gi = this;
    }

    private void OnDestroy()
    {
        gi = null;
    }

    // CamMot系をすべて追加し終えたら呼んで
    public void EndAddAlive()
    {
        trash.Capacity = alive.Capacity;
    }

    public void Run()
    {
        for (int i = 0, n = alive.Count; i < n; /**/)
        {
            if (alive[i].Run(cam)) { trash.Add(alive[i]); alive.RemoveAt(i); --n; }
            else { ++i; }
        }
    }
}
