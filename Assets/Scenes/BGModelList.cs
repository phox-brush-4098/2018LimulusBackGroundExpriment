using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObject
{
    public GameObject parent;
    public Transform trans;

    public bool isAlive;
}

public class BGModelList : MonoBehaviour
{
    private void Awake()
    {
        gi = this;
    }

    public List<GameObject> modelList;
    List<List<BGObject>> active;
    public static BGModelList gi { get; private set; }

    private void Start()
    {
        active = new List<List<BGObject>>();

        for (int i = 0; i < modelList.Count; ++i)
        {
            active.Add(new List<BGObject>());
        }
    }

    public BGObject GetModel(int arg)
    {
        var obj = Instantiate(modelList[arg]);
        var bg = new BGObject();
        bg.parent = obj;
        bg.trans = obj.transform;
        return bg;
    }
}
