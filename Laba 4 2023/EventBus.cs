using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_4_2023
{
    public class EventBus
        {
            private Dictionary<string, List<Delegate>> _eventHandlers;
            private DateTime _lastEventTime;
            private int _eventThrottleMilliseconds;
            public EventBus(int eventThrottleMilliseconds)
            {
                _eventHandlers = new Dictionary<string, List<Delegate>>();
                _eventThrottleMilliseconds = eventThrottleMilliseconds;
                _lastEventTime = DateTime.MinValue;
            }
            public void RegisterEventHandler(string eventName, Delegate eventHandler)
            {
                if (!_eventHandlers.ContainsKey(eventName))
                {
                    _eventHandlers[eventName] = new List<Delegate>();
                }
                _eventHandlers[eventName].Add(eventHandler);
            }
            public void UnregisterEventHandler(string eventName, Delegate eventHandler)
            {
                if (_eventHandlers.ContainsKey(eventName))
                {
                    _eventHandlers[eventName].Remove(eventHandler);
                }
            }
            public void RaiseEvent(string eventName, object sender, EventArgs args)
            {
                if (_eventHandlers.ContainsKey(eventName))
                {
                    var eventHandlers = _eventHandlers[eventName];                    
                    var timeSinceLastEvent = DateTime.Now - _lastEventTime;
                    if
                        (timeSinceLastEvent.TotalMilliseconds < _eventThrottleMilliseconds)
                    {
                        var waitTime = _eventThrottleMilliseconds - timeSinceLastEvent.TotalMilliseconds;
                        Thread.Sleep((int)waitTime);
                    }
                    foreach (var eventHandler in eventHandlers)
                    {
                        eventHandler.DynamicInvoke(sender, args);
                    }
                    _lastEventTime = DateTime.Now;
                }
            }
        }
    }
