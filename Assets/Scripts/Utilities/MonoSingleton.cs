using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {

    /* Monosingleton is a class that creates a monostate 
     * to support n instanceswhere n is a fixed number
     * 
     * Change ": MonoBehavior" to ": MonoSingleton<'Class name'>" eg. ": MonoSingleton<LevelManager>"
    */

    private static T instance;
    public static T Instance {
        get
        {
            if (instance == null)
            {
                instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                instance.Init();
            }

            return instance;
        }
    }

    public virtual void Init() { }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else {
            instance = this as T;

        }
        Init();
    }

    private void OnDestory(){
        if (this == instance) {
           instance = null;
        }
    }

}
