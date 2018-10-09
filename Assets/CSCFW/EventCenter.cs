using System.Collections.Generic;

namespace CSCFW
{
	public class EventCenter
	{
		public enum Order
		{
			ONE = 1,
			TWO = 2,
		}
		private readonly Order[] EXECUTE_ORDER = new Order[] { Order.ONE, Order.TWO };

		public delegate void EventHandler(object arg = null);
		private Dictionary<Order, Dictionary<EventID, EventHandler>> _eventDict = new Dictionary<Order, Dictionary<EventID, EventHandler>>();

		private EventCenter()
		{
			for (int i = 0; i < EXECUTE_ORDER.Length; ++i)
			{
				_eventDict.Add(EXECUTE_ORDER[i], new Dictionary<EventID, EventHandler>());
			}
		}

		public void Register(EventID eventID, EventHandler handler, Order order)
		{
			var orderDict = _eventDict[order];
			if (!orderDict.ContainsKey(eventID))
			{
				orderDict[eventID] = handler;
			}
			else
			{
				orderDict[eventID] += handler;
			}
		}

		public void Unregister(EventID eventID, EventHandler handler, Order order)
		{
			var orderDict = _eventDict[order];
			if (!orderDict.ContainsKey(eventID))
			{
				return;
			}
			orderDict[eventID] -= handler;
			if (orderDict[eventID] == null)
			{
				orderDict.Remove(eventID);
			}
		}

		public void Fire(EventID eventID, object arg = null)
		{
			for (int i = 0; i < EXECUTE_ORDER.Length; ++i)
			{
				var orderDict = _eventDict[EXECUTE_ORDER[i]];
				if (orderDict.ContainsKey(eventID))
				{
					if (orderDict[eventID] != null)
					{
						orderDict[eventID].Invoke(arg);
					}
					else
					{
						orderDict.Remove(eventID);
					}
				}
			}
		}

		public EventHandler FindHandler(EventID eventID, Order order)
		{
			var orderDict = _eventDict[order];
			if (orderDict.ContainsKey(eventID))
			{
				return orderDict[eventID];
			}
			return null;
		}
	}
}


