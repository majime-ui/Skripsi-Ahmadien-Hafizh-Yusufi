using System;
using Majime.ObjectPoolSystem;
using UnityEngine;
using UnityEngine.Pool;

namespace Majime.Interfaces
{
    public interface IObjectPoolItem
    {
        void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component;

        void Release();
    }
}