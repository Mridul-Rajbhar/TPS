using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer
{
    public static FunctionTimer Create(Action action, float timer)
    {

        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHood));
        FunctionTimer functionTimer = new FunctionTimer(action, timer, gameObject);
        gameObject.GetComponent<MonoBehaviourHood>().onUpdate = functionTimer.Update;

        return functionTimer;
    }

    public void RestartTimer(float new_time)
    {
        this.timer = new_time;
    }
    public class MonoBehaviourHood : MonoBehaviour
    {

        public Action onUpdate;
        private void Update()
        {
            if (onUpdate != null)
            {
                onUpdate();
            }
        }
    }
    Action action;
    public float timer;
    GameObject functionTimerGameObject;
    private bool isDestroyed;

    private FunctionTimer(Action action, float timer, GameObject functionTimerGameObject)
    {
        this.action = action;
        this.timer = timer;
        this.functionTimerGameObject = functionTimerGameObject;
        isDestroyed = false;
    }

    // non-monobehaviour update.
    public void Update()
    {
        if (!isDestroyed)
        {
            this.timer -= Time.deltaTime;
            if (this.timer < 0)
            {
                action();
                DestroySelf();
            }
        }

    }

    void DestroySelf()
    {
        isDestroyed = true;
        UnityEngine.Object.Destroy(functionTimerGameObject);
    }
}
