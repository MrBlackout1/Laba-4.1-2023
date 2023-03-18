using Laba_4_2023;
using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        var eventBus = new EventBus(2000);
        
        eventBus.RegisterEventHandler("event1", new EventHandler(EventHandler1));
        eventBus.RegisterEventHandler("event1", new EventHandler(EventHandler2));
        eventBus.RegisterEventHandler("event2", new EventHandler(EventHandler3));
        
        eventBus.RaiseEvent("event1", null, EventArgs.Empty);
        eventBus.RaiseEvent("event2", null, EventArgs.Empty);
        eventBus.RaiseEvent("event1", null, EventArgs.Empty);

        eventBus.UnregisterEventHandler("event1", new EventHandler(EventHandler1));
                
        eventBus.RaiseEvent("event1", null, EventArgs.Empty);
        eventBus.RaiseEvent("event2", null, EventArgs.Empty);
        eventBus.RaiseEvent("event1", null, EventArgs.Empty);
    }

    static void EventHandler1(object sender, EventArgs args)
    {
        Console.WriteLine("EventHandler1 called");
    }

    static void EventHandler2(object sender, EventArgs args)
    {
        Console.WriteLine("EventHandler2 called");
    }

    static void EventHandler3(object sender, EventArgs args)
    {
        Console.WriteLine("EventHandler3 called");
    }
}