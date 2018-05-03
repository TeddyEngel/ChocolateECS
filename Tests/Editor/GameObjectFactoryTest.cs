using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using ChocolateECS;

public class GameObjectFactoryTest
{
	[Test]
	public void Instantiate_WithNullGameObject_ThrowsArgumentException() 
	{
		Assert.Throws<ArgumentException>(() => { GameObjectFactory.Instantiate(null); });
	}

	[Test]
	public void Instantiate_WithValidGameObject_ReturnsNotNull() 
	{
		GameObject go = new GameObject();

		Assert.IsNotNull(GameObjectFactory.Instantiate(go));
	}

	[Test]
	public void Instantiate_WithValidGameObject_ReturnsANewGameObject() 
	{
		GameObject go = new GameObject();
		GameObject newGo = GameObjectFactory.Instantiate(go);

		Assert.AreNotEqual(go.GetInstanceID(), newGo.GetInstanceID());
	}

	[Test]
	public void Instantiate_WithValidGameObject_TriggersOnGameObjectInstantiated() 
	{
		GameObject go = new GameObject();
		bool eventTriggered = false;
		GameObjectFactory.OnGameObjectInstantiated += (eventGo) => {
			eventTriggered = true;
		};
		
		GameObject newGo = GameObjectFactory.Instantiate(go);

		Assert.IsTrue(eventTriggered);
	}

	[Test]
	public void Instantiate_WithValidGameObject_TriggersOnGameObjectInstantiatedWithNewGameObject() 
	{
		GameObject go = new GameObject();
		
		GameObjectFactory.OnGameObjectInstantiated += (eventGo) => {
			Assert.AreNotEqual(go.GetInstanceID(), eventGo.GetInstanceID());
		};
		
		GameObject newGo = GameObjectFactory.Instantiate(go);
	}

	[Test]
	public void DestroyImmediate_WithNullGameObject_ThrowsArgumentException() 
	{
		Assert.Throws<ArgumentException>(() => { GameObjectFactory.DestroyImmediate(null); });
	}

	// TODO: This test doesn't seem to evaluate correctly
	// [Test]
	// public void DestroyImmediate_WithValidGameObject_SetsTheGameObjectToNull() 
	// {
	// 	GameObject go = new GameObject();

	// 	GameObjectFactory.DestroyImmediate(go);

	// 	Assert.AreEqual(go, null);
	// }

	[Test]
	public void DestroyImmediate_WithValidGameObject_TriggersOnGameObjectPreDestroyed() 
	{
		GameObject go = new GameObject();
		bool eventTriggered = false;
		GameObjectFactory.OnGameObjectPreDestroyed += (eventGo) => {
			eventTriggered = true;
		};
		
		GameObjectFactory.DestroyImmediate(go);

		Assert.IsTrue(eventTriggered);
	}

	[Test]
	public void DestroyImmediate_WithValidGameObject_TriggersOnGameObjectPreDestroyedWithGameObject() 
	{
		GameObject go = new GameObject();
		
		GameObjectFactory.OnGameObjectPreDestroyed += (eventGo) => {
			Assert.AreEqual(go.GetInstanceID(), eventGo.GetInstanceID());
		};
		
		GameObjectFactory.DestroyImmediate(go);
	}

	[Test]
	public void DestroyImmediate_WithValidGameObject_TriggersOnGameObjectPostDestroyed() 
	{
		GameObject go = new GameObject();
		bool eventTriggered = false;
		GameObjectFactory.OnGameObjectPostDestroyed += (eventGo) => {
			eventTriggered = true;
		};
		
		GameObjectFactory.DestroyImmediate(go);

		Assert.IsTrue(eventTriggered);
	}

	// TODO: Enable when I know what to test against
	// [Test]
	// public void DestroyImmediate_WithValidGameObject_TriggersOnGameObjectPostDestroyedWithGameObject() 
	// {
	// 	GameObject go = new GameObject();
		
	// 	GameObjectFactory.OnGameObjectPostDestroyed += (eventGo) => {
	// 		Assert.AreEqual(go.GetInstanceID(), eventGo.GetInstanceID());
	// 	};
		
	// 	GameObjectFactory.DestroyImmediate(go);
	// }
}

