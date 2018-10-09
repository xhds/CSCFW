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
			Test_JsonAPI_LoadFromFile();
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

		private void Test_JsonAPI_LoadFromFile()
		{
			var classA = JsonAPI.LoadFromFile<ClassA>("JsonAPITest");
			Debug.Assert(classA != null);
			Debug.Assert(classA.a == -3);
			Debug.Assert(classA.b == -0.4f);
			Debug.Assert(classA.c);
			Debug.Assert(classA.d == "fuck");
			Debug.Assert(classA.e != null);
			for (int i = 0; i < classA.e.Count; ++i)
			{
				Debug.Assert(classA.e[i] == i + 1);
			}
			Debug.Assert(classA.f != null);
			foreach (var kv in classA.f)
			{
				Debug.Assert(kv.Key  == kv.Value);
			}
			Debug.Log("Test_JsonAPI_LoadFromFile FINISH");
		}
	}
}

