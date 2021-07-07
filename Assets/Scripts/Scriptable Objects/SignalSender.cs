using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SignalSender : ScriptableObject
{
    public List<SignalListener> listeners = new List<SignalListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaise();
        }
    }

    //-- REGISTER LISTENER ------------------

    public void RegisterListener(SignalListener listener)
    {
        listeners.Add(listener);
    }

    //-- DEREGISTER LISTENER ----------------

    public void DeRegisterListener(SignalListener listener)
    {
        listeners.Remove(listener);
    }
}
