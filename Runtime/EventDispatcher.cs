using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "EventDispatcher", menuName = "Services/EventDispatcher")]
    public class EventDispatcher : Service
    {
        readonly List<IEventListener> _listeners = new List<IEventListener>();

        public override void AddToContainer(IServiceContainer container)
        {
            var eventDispatcher = ScriptableObject.CreateInstance<EventDispatcher>();
            container.Register<EventDispatcher>(eventDispatcher);
            container.Register<Service>(eventDispatcher);
            container.Register<IDisposable>(eventDispatcher);
        }

        public override void Dispose()
        {
            _listeners.Clear();
        }
        
        public void AddListener(IEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(IEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }

        public void Raise(GameEvent e)
        {
            foreach(var listener in _listeners)
            {
                listener.OnEventDispatched(e);
            }
        }

    }

    public interface IEventListener
    {
        void OnEventDispatched(GameEvent e);
    }

    public abstract class GameEvent
    {
    }
}