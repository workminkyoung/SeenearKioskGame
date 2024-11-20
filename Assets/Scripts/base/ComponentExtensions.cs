using System;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentExtensions
{
    public static T GetOrAddComponent<T>(this Component component) where T : Component => component.GetComponent<T>() ?? component.gameObject.AddComponent<T>();
}

public static class UtilityExtensions
{
    public static T[] GetComponentsOnlyInChildren_NonRecursive<T>(Transform parent) where T : class
    {
        if (parent.childCount <= 0) return null;

        List<T> output = new List<T>();
        T component;

        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).TryGetComponent<T>(out component) == true)
            {
                output.Add(component);
            }
        }
        if (output.Count > 0)
            return output.ToArray();

        return null;
    }

    public static T GetComponentOnlyInChildren_NonRecursive<T>(Transform parent) where T : class
    {
        if (parent.childCount <= 0) return null;

        T output = null;

        for (int i = 0; i < parent.childCount; i++)
        {
            var component = parent.GetChild(i).GetComponent<T>();
            if (component != null)
                output = component;
        }
        if (output != null)
            return output;

        return null;
    }


    /// <summary>
    /// Gets the components only in immediate children of parent.
    /// </summary>
    /// <returns>The components only in children.</returns>
    /// <param name="script">MonoBehaviour Script, e.g. "this".</param>
    /// <param name="isRecursive">If set to <c>true</c> recursive search of children is performed.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T[] GetComponentsOnlyInChildren<T>(this MonoBehaviour script, bool isRecursive = false) where T : class
    {
        if (isRecursive)
            return script.GetComponentsOnlyInChildren_Recursive<T>();
        return script.GetComponentsOnlyInChildren_NonRecursive<T>();
    }

    /*
    /// <summary>
    /// Gets the components only in children, with interface check. Slowest recursive check.
    /// </summary>
    /// <returns>The components only in children that are either interface, 
    /// component or subclass of a component passed.</returns>
    /// <param name="script">Script.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    static T[] GetComponentsOnlyInChildren_Interface<T>(this MonoBehaviour script) where T : class
    {
        List<T> group = new List<T>();
        //collect only if its an interface or a Component
        if (typeof(T).IsInterface || typeof(T).IsSubclassOf(typeof(Component))
        || typeof(T) == typeof(Component))
        {
            foreach (Transform child in script.transform)
            {
                group.AddRange(child.GetComponentsInChildren<T>());
            }
        }
        return group.ToArray();
    }
    */

    /// <summary>
    /// Gets the components only in children transform search. Not recursive, ie not grandchildren! 
    /// </summary>
    /// <returns>The components only in children transform search.</returns>
    /// <param name="parent">Parent, ie "this".</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    static T[] GetComponentsOnlyInChildren_NonRecursive<T>(this MonoBehaviour parent) where T : class
    {
        if (parent.transform.childCount <= 0) return null;

        var output = new List<T>();

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var component = parent.transform.GetChild(i).GetComponent<T>();
            if (component != null)
                output.Add(component);
        }
        if (output.Count > 0)
            return output.ToArray();

        return null;
    }

    /// <summary>
    /// Gets the components only in children, recursively for children of children.
    /// </summary>
    /// <returns>The components only in children of calling parent.</returns>
    /// <param name="parent">Parent.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    static T[] GetComponentsOnlyInChildren_Recursive<T>(this MonoBehaviour parent) where T : class
    {
        if (parent.transform.childCount <= 0) return null;

        var transforms = new HashSet<Transform>(parent.GetComponentsInChildren<Transform>());
        transforms.Remove(parent.transform);

        var output = new List<T>();
        foreach (var child in transforms)
        {
            var component = child.GetComponent<T>();
            if (component != null)
            {
                output.Add(component);
            }
        }

        if (output.Count > 0)
            return output.ToArray();

        return null;
    }

    /* This is still fastest...
     * 
     foreach (var i in GetComponentsInChildren<INotify>()
     {
        if (i == GetComponent<INotify>()) continue;
        i.Notify();
     }
     * 
     */
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    
    public static T[] GetComponentsOnlyInChildrenByTag_NonRecursive<T>(Transform parent, string tag) where T : class
    {
        if (parent.childCount <= 0) return null;

        List<T> output = new List<T>();
        T component;

        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).CompareTag(tag))
            {
                if (parent.GetChild(i).TryGetComponent<T>(out component) == true)
                {
                    output.Add(component);
                }
            }
        }
        if (output.Count > 0)
            return output.ToArray();

        return null;
    }
    
    public static T GetComponentOnlyInChildrenByTag_NonRecursive<T>(Transform parent, string tag) where T : class
    {
        if (parent.childCount <= 0) return null;

        T output = null;

        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).CompareTag(tag))
            {
                var component = parent.GetChild(i).GetComponent<T>();
                if (component != null)
                    output = component;
            }
        }
        if (output != null)
            return output;

        return null;
    }
}