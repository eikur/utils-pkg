using System;
using System.Collections.Generic;
using UnityEngine;

public class Services : MonoBehaviour, IServiceContainer
{
    [SerializeField] private GeneralServices _generalServices = null;

    private List<KeyValuePair<Type, object>> _boundItems = new List<KeyValuePair<Type, object>>();
    private bool _initialized = false;

    void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        _generalServices.AddToContainer(this);
        foreach(var initializable in ResolveList<IInitializable>())
        {
            initializable.Initialize();
        }

        _initialized = true;
    }


    public void Register<T>(T obj) where T : class
    {
        _boundItems.Add(new KeyValuePair<Type, object> (typeof(T), obj));
    }

    public void Register<T>() where T : class, new()
    {
        Register<T>(new T());
    }

    public T Resolve<T>()
    {
        object retVal = null;
        var key = typeof(T);

        retVal = _boundItems.Find(kvp => kvp.Key == typeof(T)).Value;

        return (T)retVal;
    }

    public List<T> ResolveList<T>()
    {
        var retVal = new List<T>();
        foreach(var item in _boundItems)
        {
            if (typeof(T) == item.Key)
            {
                retVal.Add((T)item.Value);
            }
        }

        return retVal;
    }

    #region disposal
    private void OnDestroy()
    {
        Dispose();
    }

    private void Dispose()
    {
        var disposableList = ResolveList<IDisposable>();
        foreach (var disposable in disposableList)
        {
            disposable.Dispose();
        }

        _boundItems.Clear();
    }
    #endregion

    #region singleton instance
    public static Services Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        Initialize();
    }

    #endregion  
}

public interface IServiceContainer
{
    void Register<T>(T obj) where T : class;
    void Register<T>() where T : class, new();
    T Resolve<T>();
    List<T> ResolveList<T>();
}
