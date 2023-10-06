using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{

    public static LightManager instance { get; private set; }
    public List<Lamp> lamps = new List<Lamp>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddLamp(Lamp lamp)
    {
        lamps.Add(lamp);
    }

    public void RemoveLamp(Lamp lamp)
    {
        lamps.Remove(lamp);
    }

}
