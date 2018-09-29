using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSCFW
{
	public class MonoUnitTest : MonoBehaviour
	{
		void Start()
		{
			Test_EventCenter_Register();
			Test_EventCenter_Unregister();
			Test_EventCenter_FireEvent();
		}

		private bool _TestMethod_Called = false;
		private void TestMethod(object x)
		{
			_TestMethod_Called = true;
		}

		private bool _TestMethod_B_Called = false;
		private void TestMethod_B(object x)
		{
			_TestMethod_B_Called = true;
		}

		private void Test_EventCenter_Register()
		{
			Singleton<EventCenter>.Create();
			var center = Singleton<EventCenter>.Instance;
			center.Register(EventID.UNIT_TEST_ID, TestMethod, EventCenter.Order.ONE);

			var handler = center.FindHandler(EventID.UNIT_TEST_ID, EventCenter.Order.ONE);
			Debug.Assert(handler == TestMethod);

			handler = center.FindHandler(EventID.BEGIN, EventCenter.Order.ONE);
			Debug.Assert(handler == null);

			handler = center.FindHandler(EventID.BEGIN, EventCenter.Order.TWO);
			Debug.Assert(handler == null);

			handler = center.FindHandler(EventID.UNIT_TEST_ID, EventCenter.Order.TWO);
			Debug.Assert(handler == null);

			Singleton<EventCenter>.Destroy();
			Debug.Log("Test_EventCenter_Register FINISH");
		}

		private void Test_EventCenter_Unregister()
		{
			Singleton<EventCenter>.Create();
			var center = Singleton<EventCenter>.Instance;
			center.Register(EventID.UNIT_TEST_ID, TestMethod, EventCenter.Order.ONE);
			center.Unregister(EventID.UNIT_TEST_ID, TestMethod, EventCenter.Order.TWO);

			var handler = center.FindHandler(EventID.UNIT_TEST_ID, EventCenter.Order.ONE);
			Debug.Assert(handler == TestMethod);

			center.Unregister(EventID.BEGIN, TestMethod, EventCenter.Order.ONE);
			handler = center.FindHandler(EventID.UNIT_TEST_ID, EventCenter.Order.ONE);
			Debug.Assert(handler == TestMethod);

			center.Unregister(EventID.UNIT_TEST_ID, TestMethod_B, EventCenter.Order.ONE);
			handler = center.FindHandler(EventID.UNIT_TEST_ID, EventCenter.Order.ONE);
			Debug.Assert(handler == TestMethod);

			center.Unregister(EventID.UNIT_TEST_ID, TestMethod, EventCenter.Order.ONE);
			handler = center.FindHandler(EventID.UNIT_TEST_ID, EventCenter.Order.ONE);
			Debug.Assert(handler == null);

			Singleton<EventCenter>.Destroy();
			Debug.Log("Test_EventCenter_Register FINISH");
		}

		private void Test_EventCenter_FireEvent()
		{
			Singleton<EventCenter>.Create();
			var center = Singleton<EventCenter>.Instance;
			center.Register(EventID.UNIT_TEST_ID, TestMethod, EventCenter.Order.ONE);

			center.Fire(EventID.UNIT_TEST_ID);
			Debug.Assert(_TestMethod_Called);
			Debug.Assert(!_TestMethod_B_Called);
			_TestMethod_Called = false;

			center.Unregister(EventID.UNIT_TEST_ID, TestMethod, EventCenter.Order.ONE);
			center.Fire(EventID.UNIT_TEST_ID);
			Debug.Assert(!_TestMethod_Called);
			Debug.Assert(!_TestMethod_B_Called);

			Singleton<EventCenter>.Destroy();
			Debug.Log("Test_EventCenter_FireEvent FINISH");
		}
	}
}

