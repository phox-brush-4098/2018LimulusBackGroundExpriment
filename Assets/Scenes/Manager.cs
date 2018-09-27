using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScene
{
    public GameObject parentObj;
    public Transform parent;
    public virtual void SetColor(Material arg) { }

    public void Run()
    {
        RunMain();
    }

    protected virtual void RunMain()
    {

    }

    public void Init()
    {
        InitMain();
    }

    protected virtual void InitMain()
    {

    }

    // 基本的に継承はしない
    public virtual void Delete()
    {
        MonoBehaviour.Destroy(parentObj);
    }

    protected BGObject GetModel(int arg)
    {
        var tmp = BGModelList.gi.GetModel(arg);
        tmp.trans.parent = parent;
        return tmp;
    }
}

public class Manager : MonoBehaviour
{
    public BGScene obj = null;
    public Color cl;
    public Color back;
    public Material mat;

    public Slider sl;
    public Text btnText;

    private void Awake()
    {
        sl.maxValue = 2;
    }

    void BGCamReset()
    {
        BGCameraMotion.gi.Reset();
    }

    // Use this for initialization
    void Start()
    {
        mat.SetColor("_Color2", cl);
        mat.SetColor("_Color", back);

        Camera.main.backgroundColor = back;
    }

    // Update is called once per frame
    void Update()
    {
        if (obj != null)
        {
            obj.Run();
        }

        BGCameraMotion.gi.Run();
    }

    public void ViewNumUpdate()
    {
        btnText.text = "play" + sl.value;
    }

    public void StartScene()
    {
        if (obj != null)
        {
            obj.Delete();
            Destroy(obj.parentObj);
        }

        BGCamReset();

        switch (sl.value)
        {
        case 1:
            obj = new BG01();
            break;

        case 2:
            obj = new BG02();
            break;

        default:
            obj = new BGScene();
            break;
        }

        if (obj != null)
        {
            obj.SetColor(mat);
            obj.parentObj = new GameObject();
            obj.parent = obj.parentObj.transform;
            obj.Init();
            BGCameraMotion.gi.EndAddAlive();
        }
    }
}

public class BG02 : BGScene
{
    protected override void InitMain()
    {
        base.InitMain();

        BGCameraMotion.gi.transform.position = new Vector3(0f, 0f, 5f);
        BGCameraMotion.gi.transform.LookAt(new Vector3(0f, 50f, 3f), new Vector3(0f, 0f, 1f));

        var move = new CMMove();

        BGCameraMotion.gi.AddAlive(move);
        move.CntMax = 4000;
        move.spd = new Vector3(0f, 45f * 0.02f, 0f);

        for (int i = 0; i < 100; ++i)
        {
            var obj = GetModel(1);
            obj.trans.position = new Vector3(-7.5f, 40f * i, 8f);
            obj.trans.localScale = new Vector3(65f, 65f, 800f);
            obj.trans.localRotation = Quaternion.Euler(Vector3.zero);

            obj = GetModel(1);
            obj.trans.position = new Vector3(7.5f, 40f * i, 8f);
            obj.trans.localScale = new Vector3(65f, 65f, 800f);
            obj.trans.localRotation = Quaternion.Euler(Vector3.zero);

            obj = GetModel(0);
            obj.trans.position = new Vector3(0f, 40f * i, 16f);
            obj.trans.localScale = new Vector3(23f, 0.1f, 1.75f);

            obj = GetModel(0);
            obj.trans.position = new Vector3(0f, 40f * i, 12f);
            obj.trans.localScale = new Vector3(15f, 0.1f, 1.25f);
        }

            for (int i = 0; i < 400; ++i)
        {
            var obj = GetModel(0);
            obj.trans.position = new Vector3(0f, 10f * i, 0f);
            obj.trans.localScale = new Vector3(1f, 8f, 0.1f);

            obj = GetModel(0);
            obj.trans.position = new Vector3(-1.25f, 10f * i - 5f, 0f);
            obj.trans.localScale = new Vector3(1f, 8f, 0.1f);
            obj = GetModel(0);
            obj.trans.position = new Vector3(1.25f, 10f * i - 5f, 0f);
            obj.trans.localScale = new Vector3(1f, 8f, 0.1f);

            obj = GetModel(0);
            obj.trans.position = new Vector3(-2.5f, 10f * i, 0f);
            obj.trans.localScale = new Vector3(1f, 8f, 0.1f);
            obj = GetModel(0);
            obj.trans.position = new Vector3(2.5f, 10f * i, 0f);
            obj.trans.localScale = new Vector3(1f, 8f, 0.1f);

            obj = GetModel(0);
            obj.trans.position = new Vector3(-3.75f, 10f * i - 5f, 0f);
            obj.trans.localScale = new Vector3(1f, 8f, 0.1f);
            obj = GetModel(0);
            obj.trans.position = new Vector3(3.75f, 10f * i - 5f, 0f);
            obj.trans.localScale = new Vector3(1f, 8f, 0.1f);

        }
    }
}

public class BG01: BGScene
{
    protected override void RunMain()
    {
        base.RunMain();
    }
    
    protected override void InitMain()
    {
        base.InitMain();

        BGCameraMotion.gi.transform.position = new Vector3(0f, 0f, 5f);
        BGCameraMotion.gi.transform.LookAt(new Vector3(0f, 10f, 5f), new Vector3(0f, 0f, 1f));

        var move = new CMMove();

        BGCameraMotion.gi.AddAlive(move);
        move.CntMax = 1800;
        move.spd = new Vector3(0f, 50f * 0.02f, 0f);

        move = new CMMove();
        BGCameraMotion.gi.AddAlive(move);
        move.CntMax = 900;
        move.spd = new Vector3(0f, -30f * 0.02f, 0f);
        move.spdExt = 0.975f;

        var rot = new CMRot();
        BGCameraMotion.gi.AddAlive(rot);
        rot.spd = new Vector3(0f, 20f, 0f);
        rot.spdExt = 0.965f;
        rot.CntMax = 900;

        //move = new CMMove();
        //BGCameraMotion.gi.AddAlive(move);
        //move.freez = 300;
        //move.CntMax = 900;
        //move.spd = new Vector3(0f, 0f, 15f * 0.02f);
        //move.spdExt = 0.95f;

        //rot = new CMRot();
        //BGCameraMotion.gi.AddAlive(rot);
        //rot.spd = new Vector3(26f * 0.02f, 5f * 0.02f, 18f * 0.02f);
        //rot.spdExt = 0.95f;
        //rot.CntMax = 900;
        //rot.freez = 300;

        for (int i = 0; i < 20; ++ i)
        {
            var obj = GetModel(0);
            obj.trans.position = new Vector3(-30f + (3f * i), 155f - 4f, 0f);
            obj.trans.localScale = new Vector3(0.5f, 291f, 0.1f);

            obj = GetModel(0);
            obj.trans.position = new Vector3(-30f + (3f * i), 155f - 4f, 10f);
            obj.trans.localScale = new Vector3(0.5f, 291f, 0.1f);
        }

        for (int i = 0; i < 100; ++i)
        {
            var obj = GetModel(0);
            obj.trans.position = new Vector3(0f, (3f * i), 0f);
            obj.trans.localScale = new Vector3(201f, 0.5f, 0.1f);

            obj = GetModel(0);
            obj.trans.position = new Vector3(0f, (3f * i), 10f);
            obj.trans.localScale = new Vector3(201f, 0.5f, 0.1f);
        }
        
        for (int i = 0; i < 50; ++ i)
        {
            var obj = GetModel(1);
            obj.trans.position = new Vector3(12f, 10f * i + 318f, 5f);
            obj.trans.localScale = new Vector3(75f, 75f, 150f + i * 15f);
            obj.trans.localRotation = Quaternion.Euler(Vector3.zero);

            obj = GetModel(1);
            obj.trans.position = new Vector3(-12f, 10f * i + 318f, 5f);
            obj.trans.localScale = new Vector3(75f, 75f, 150f + i * 15f);
            obj.trans.localRotation = Quaternion.Euler(Vector3.zero);

            obj = GetModel(0);
            obj.trans.position = new Vector3(0f, (10f * i) + 318f, -5f);
            obj.trans.localScale = new Vector3(201f, 1f, 0.1f);

            obj = GetModel(0);
            obj.trans.position = new Vector3(0f, (10f * i) + 318f, 15f);
            obj.trans.localScale = new Vector3(201f, 0.5f, 0.1f);
        }

        for (int i = 50; i < 200; ++i)
        {
            var obj = GetModel(1);
            obj.trans.position = new Vector3(12f, 10f * i + 318f, 5f);
            obj.trans.localScale = new Vector3(75f, 75f, 150f + i * 15f);
            obj.trans.localRotation = Quaternion.Euler(Vector3.zero);

            obj = GetModel(1);
            obj.trans.position = new Vector3(-12f, 10f * i + 318f, 5f);
            obj.trans.localScale = new Vector3(75f, 75f, 150f + i * 15f);
            obj.trans.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
