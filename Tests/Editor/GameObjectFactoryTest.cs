using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using ChocolateECS;

public class GameObjectFactoryTest
{
	GameObjectFactory _gameObjectFactory;


	[SetUp] public void SetUp()
    {
    	_gameObjectFactory = new GameObjectFactory();
    }

	[Test]
	public void GameObjectFactory_CanBeInstantiated() 
	{
		var newFactory = new GameObjectFactory();
		Assert.NotNull(newFactory);
	}

	[Test]
	public void Instantiate_WithNullGameObject_ThrowsArgumentException() 
	{
		Assert.Throws<ArgumentException>(() => { _gameObjectFactory.Instantiate(null); });
	}

	[Test]
	public void Instantiate_WithValidGameObject_ReturnsNotNull() 
	{
		GameObject go = new GameObject();

		Assert.IsNotNull(_gameObjectFactory.Instantiate(go));
	}

	[Test]
	public void Instantiate_WithValidGameObject_ReturnsANewGameObject() 
	{
		GameObject go = new GameObject();
		GameObject newGo = _gameObjectFactory.Instantiate(go);

		Assert.AreNotEqual(go.GetInstanceID(), newGo.GetInstanceID());
	}

	[Test]
	public void Instantiate_WithValidGameObject_TriggersOnGameObjectInstantiated() 
	{
		GameObject go = new GameObject();
		bool eventTriggered = false;
		_gameObjectFactory.OnGameObjectInstantiated += (eventGo) => {
			eventTriggered = true;
		};
		
		GameObject newGo = _gameObjectFactory.Instantiate(go);

		Assert.IsTrue(eventTriggered);
	}

	[Test]
	public void Instantiate_WithValidGameObject_TriggersOnGameObjectInstantiatedWithNewGameObject() 
	{
		GameObject go = new GameObject();
		
		_gameObjectFactory.OnGameObjectInstantiated += (eventGo) => {
			Assert.AreNotEqual(go.GetInstanceID(), eventGo.GetInstanceID());
		};
		
		GameObject newGo = _gameObjectFactory.Instantiate(go);
	}

	[Test]
	public void DestroyImmediate_WithNullGameObject_ThrowsArgumentException() 
	{
		Assert.Throws<ArgumentException>(() => { _gameObjectFactory.DestroyImmediate(null); });
	}

	// TODO: This test doesn't seem to evaluate correctly
	// [Test]
	// public void DestroyImmediate_WithValidGameObject_SetsTheGameObjectToNull() 
	// {
	// 	GameObject go = new GameObject();

	// 	_gameObjectFactory.DestroyImmediate(go);

	// 	Assert.AreEqual(go, null);
	// }

	[Test]
	public void DestroyImmediate_WithValidGameObject_TriggersOnGameObjectDestroyed() 
	{
		GameObject go = new GameObject();
		bool eventTriggered = false;
		_gameObjectFactory.OnGameObjectDestroyed += (eventGo) => {
			eventTriggered = true;
		};
		
		_gameObjectFactory.DestroyImmediate(go);

		Assert.IsTrue(eventTriggered);
	}

	[Test]
	public void DestroyImmediate_WithValidGameObject_TriggersOnGameObjectDestroyedWithGameObject() 
	{
		GameObject go = new GameObject();
		
		_gameObjectFactory.OnGameObjectDestroyed += (eventGo) => {
			Assert.AreEqual(go.GetInstanceID(), eventGo.GetInstanceID());
		};
		
		_gameObjectFactory.DestroyImmediate(go);
	}
}

