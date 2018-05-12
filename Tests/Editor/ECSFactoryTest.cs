using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using ChocolateECS;

public class ECSFactoryTest
{
	[Test]
	public void Instantiate_WithNullGameObject_ThrowsArgumentException() 
	{
		Assert.Throws<ArgumentException>(() => { ECSFactory.Instantiate(null); });
	}

	[Test]
	public void Instantiate_WithValidGameObject_ReturnsNotNull() 
	{
		GameObject go = new GameObject();

		Assert.IsNotNull(ECSFactory.Instantiate(go));
	}

	[Test]
	public void Instantiate_WithValidGameObject_ReturnsANewGameObject() 
	{
		GameObject go = new GameObject();
		GameObject newGo = ECSFactory.Instantiate(go);

		Assert.AreNotEqual(go.GetInstanceID(), newGo.GetInstanceID());
	}

	[Test]
	public void Instantiate_WithValidGameObject_TriggersOnGameObjectInstantiated() 
	{
		GameObject go = new GameObject();
		bool eventTriggered = false;
		ECSFactory.OnGameObjectInstantiated += (eventGo) => {
			eventTriggered = true;
		};
		
		GameObject newGo = ECSFactory.Instantiate(go);

		Assert.IsTrue(eventTriggered);
	}

	[Test]
	public void Instantiate_WithValidGameObject_TriggersOnGameObjectInstantiatedWithNewGameObject() 
	{
		GameObject go = new GameObject();
		
		ECSFactory.OnGameObjectInstantiated += (eventGo) => {
			Assert.AreNotEqual(go.GetInstanceID(), eventGo.GetInstanceID());
		};
		
		GameObject newGo = ECSFactory.Instantiate(go);
	}

	[Test]
	public void DestroyImmediate_WithNullGameObject_ThrowsArgumentException() 
	{
		Assert.Throws<ArgumentException>(() => { ECSFactory.DestroyImmediate(null); });
	}

	// TODO: This test doesn't seem to evaluate correctly
	// [Test]
	// public void DestroyImmediate_WithValidGameObject_SetsTheGameObjectToNull() 
	// {
	// 	GameObject go = new GameObject();

	// 	ECSFactory.DestroyImmediate(go);

	// 	Assert.AreEqual(go, null);
	// }

	[Test]
	public void DestroyImmediate_WithValidGameObject_TriggersOnGameObjectPreDestroyed() 
	{
		GameObject go = new GameObject();
		bool eventTriggered = false;
		ECSFactory.OnGameObjectPreDestroyed += (eventGo) => {
			eventTriggered = true;
		};
		
		ECSFactory.DestroyImmediate(go);

		Assert.IsTrue(eventTriggered);
	}

	[Test]
	public void DestroyImmediate_WithValidGameObject_TriggersOnGameObjectPreDestroyedWithGameObject() 
	{
		GameObject go = new GameObject();
		
		ECSFactory.OnGameObjectPreDestroyed += (eventGo) => {
			Assert.AreEqual(go.GetInstanceID(), eventGo.GetInstanceID());
		};
		
		ECSFactory.DestroyImmediate(go);
	}

	[Test]
	public void DestroyImmediate_WithValidGameObject_TriggersOnGameObjectPostDestroyed() 
	{
		GameObject go = new GameObject();
		bool eventTriggered = false;
		ECSFactory.OnGameObjectPostDestroyed += (eventGo) => {
			eventTriggered = true;
		};
		
		ECSFactory.DestroyImmediate(go);

		Assert.IsTrue(eventTriggered);
	}

	// TODO: Enable when I know what to test against
	// [Test]
	// public void DestroyImmediate_WithValidGameObject_TriggersOnGameObjectPostDestroyedWithGameObject() 
	// {
	// 	GameObject go = new GameObject();
		
	// 	ECSFactory.OnGameObjectPostDestroyed += (eventGo) => {
	// 		Assert.AreEqual(go.GetInstanceID(), eventGo.GetInstanceID());
	// 	};
		
	// 	ECSFactory.DestroyImmediate(go);
	// }
}

