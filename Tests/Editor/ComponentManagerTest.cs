using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using ChocolateECS;

public class ComponentManagerTest
{
    public class TestMainComponent : MonoBehaviour, IComponent 
    {
        string data;
    }

    public class TestSecondaryComponent : MonoBehaviour, IComponent 
    {
        string data;
    }

    public class TestTertiaryComponent : MonoBehaviour, IComponent 
    {
        string data;
    }

    public void CreateTestSingleComponent(Type type)
    {
        GameObject newGameObject = new GameObject();
        newGameObject.AddComponent(type);
    }

    public void CreateTestDualComponent(Type firstType, Type secondType)
    {
        GameObject newGameObject = new GameObject();
        newGameObject.AddComponent(firstType);
        newGameObject.AddComponent(secondType);
    }

    ComponentManager _componentManager;

    [SetUp]
    public void Init()
    {
        _componentManager = new ComponentManager();
    }

    [TearDown]
    public void Dispose()
    {
        // Destroy all gameobjects
        var foundObjects = GameObject.FindObjectsOfType<GameObject>();
        for (int i = 0; i < foundObjects.Length; ++i)
            GameObject.DestroyImmediate(foundObjects[i]);
    }

    [Test]
    public void GetComponents_WithNullType_ThrowsArgumentException() 
    {
        Assert.Throws<ArgumentException>(() => { _componentManager.GetComponents(null); });
    }

    [Test]
    public void GetDualComponents_WithNullTypes_ThrowsArgumentException() 
    {
        Assert.Throws<ArgumentException>(() => { _componentManager.GetDualComponents(null, typeof(ComponentManager)); });
        Assert.Throws<ArgumentException>(() => { _componentManager.GetDualComponents(typeof(ComponentManager), null); });
        Assert.Throws<ArgumentException>(() => { _componentManager.GetDualComponents(null, null); });
    }

    [Test]
    public void GetComponents_AfterCallingOnAwakeWithZeroTestMainComponent_ReturnsEmptyCollection() 
    {
        _componentManager.OnAwake();

        Assert.AreEqual(0, _componentManager.GetComponents(typeof(TestMainComponent)).Count);
    }

    [Test]
    public void GetComponents_WithoutCallingOnAwakeWithOneTestMainComponent_ReturnsEmptyCollection() 
    {
        CreateTestSingleComponent(typeof(TestMainComponent));

        Assert.AreEqual(0, _componentManager.GetComponents(typeof(TestMainComponent)).Count);
    }

    [Test]
    public void GetComponents_AfterCallingOnAwakeWithOneTestMainComponent_ReturnsCollectionWithOneElement() 
    {
        CreateTestSingleComponent(typeof(TestMainComponent));
        _componentManager.OnAwake();
        
        Assert.AreEqual(1, _componentManager.GetComponents(typeof(TestMainComponent)).Count);
    }

    [Test]
    public void GetComponents_WithTwoComponents_ReturnsCollectionWithOneElementForEachIndividualComponent() 
    {
        CreateTestDualComponent(typeof(TestMainComponent), typeof(TestSecondaryComponent));
        _componentManager.OnAwake();

        Assert.AreEqual(1, _componentManager.GetComponents(typeof(TestMainComponent)).Count);
        Assert.AreEqual(1, _componentManager.GetComponents(typeof(TestSecondaryComponent)).Count);
    }

    [Test]
    public void GetDualComponents_WithTwiceTheSameType_ReturnsEmptyCollection() 
    {
        CreateTestDualComponent(typeof(TestMainComponent), typeof(TestSecondaryComponent));
        _componentManager.OnAwake();
        
        Assert.AreEqual(0, _componentManager.GetDualComponents(typeof(TestMainComponent), typeof(TestMainComponent)).Count);
    }

    [Test]
    public void GetDualComponents_WithOneElementMatchingBothTypes_ReturnsOneElement() 
    {
        CreateTestDualComponent(typeof(TestMainComponent), typeof(TestSecondaryComponent));
        _componentManager.OnAwake();
        
        Assert.AreEqual(1, _componentManager.GetDualComponents(typeof(TestMainComponent), typeof(TestSecondaryComponent)).Count);
        Assert.AreEqual(1, _componentManager.GetDualComponents(typeof(TestSecondaryComponent), typeof(TestMainComponent)).Count);
    }

    [Test]
    public void GetDualComponents_WithZeroElementMatchingBothTypes_ReturnsZeroElements() 
    {
        CreateTestDualComponent(typeof(TestMainComponent), typeof(TestSecondaryComponent));
        _componentManager.OnAwake();
        
        Assert.AreEqual(0, _componentManager.GetDualComponents(typeof(TestMainComponent), typeof(TestTertiaryComponent)).Count);
        Assert.AreEqual(0, _componentManager.GetDualComponents(typeof(TestSecondaryComponent), typeof(TestTertiaryComponent)).Count);
    }
}

