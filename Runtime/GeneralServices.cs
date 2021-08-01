using System;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "GeneralServices", menuName = "Services/GeneralServices")]
public class GeneralServices : Service
{
    public List<Service> Services = new List<Service>();

    public override void AddToContainer(IServiceContainer container) 
    {
        foreach (Service service in Services)
        {
            service.AddToContainer(container);
        }

        var instantiatedServices = container.ResolveList<Service>();

        foreach(Service service in instantiatedServices)
        {
            service.Initialize();
        }

        foreach(Service service in instantiatedServices)
        {
            service.OnAllServicesInitialized();
        }
    }

    public override void Dispose()
    {
    }
}

public interface IInitializable
{
    void Initialize();
}

[Serializable]
public abstract class Service : ScriptableObject, IInitializable, IDisposable
{
    public abstract void AddToContainer(IServiceContainer container);
    public virtual void Initialize() { }
    public virtual void OnAllServicesInitialized() { }
    public virtual void Dispose() { }
}



