using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 씬을 이동해도 유지되는 싱글톤
public abstract class Singleton<T> : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static T _instance;

    // 싱글톤 인스턴스 반환형
    public static T instance
    {
        get
        {
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        // 싱글톤 인스턴스 생성
        if (null == _instance)
        {
            _instance = GetComponent<T>();
            // 씬 이동시 유
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
}

// 씬을 이동하면 삭제되는 싱글톤
public abstract class SingletonOnly<T> : MonoBehaviour where T : Component
{
    // 싱글톤 인스턴스
    private static T _instance;

    // 싱글톤 인스턴스 반환
    public static T instance
    {
        get
        {
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        // 싱글톤 생성
        if (null == _instance)
        {
            _instance = GetComponent<T>();
        }
        else
            Destroy(gameObject);
    }

    // 씬 이동시 싱글톤 삭제
    private void OnDestroy()
    {
        _instance = null;
    }
}
