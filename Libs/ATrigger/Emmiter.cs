using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
/// <summary>
/// data driver 
/// </summary>
namespace ATrigger
{
    /// <summary>
    /// trigger can any where
    /// </summary>
    public class Emmiter
    {
        public const int InvalidDataType = -1;
        public const int DefaultInvalidIndex = -1;

        public int Type { get; internal set; }

        public Emmiter()
        {
        }
        public void Trigger()
        {
            Dipatch(null);
        }
        public static implicit operator bool(Emmiter dd)
        {
            return dd != null;
        }

        internal void Dipatch(params object[] args)
        {
            if (Type == InvalidDataType)
            {
                Debug.WriteLine("Please use attribute DataCenter.DataEntry,or Use DataCenter.AObject(or AScriptObject) as its parent class!");
                return;
            }

            DataCenter.Emitter(Type, args);
        }
        internal virtual void Set(object obj)
        {

        }
    }
    public class Emmiter<T> : Emmiter
    {
        public void Trigger(T arg)
        {
            Dipatch(arg);
        }
    }
    public class Emmiter<T1, T2> : Emmiter
    {
        public void Trigger(T1 arg1, T2 arg2)
        {
            Dipatch(arg1, arg2);
        }
    }
    public class Emmiter<T1, T2, T3> : Emmiter
    {
        public void Trigger(T1 arg1, T2 arg2, T3 arg3)
        {
            Dipatch(arg1, arg2, arg3);
        }
    }
    /// <summary>
    /// trigger on value changed or need
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataContainer<T> : Emmiter
    {
        public T value
        {
            get
            {
                return (T)mData;
            }
            set
            {
                Set(value);
            }
        }
        public T oldValue
        {
            get
            {
                return (T)mLastData;
            }
        }
        /// <summary>
        /// use when 2 data equal but you need trigger
        /// </summary>
        /// <param name="data"></param>
        /// <param name="trigger"></param>
        /// <param name="force"></param>
        public void Set(T data, bool trigger = true, bool force = false)
        {
            mLastData = mData;
            mData = data;
            bool autoTrigger = mData != null && !mData.Equals(mLastData);
            if (trigger && (force || autoTrigger))
                Trigger();
        }
        internal object mData = default(T);
        internal object mLastData = default(T);
    }

    public class AInt : DataContainer<int>
    {
        int mInvalidValue = DefaultInvalidIndex;

        public int invalidValue
        {
            get
            {
                return mInvalidValue;
            }
            set
            {
                mInvalidValue = value;
            }
        }
        public bool valid
        {
            get
            {
                return value != mInvalidValue;
            }
        }

        public void Invalidate()
        {
            value = mInvalidValue;
        }
        public AInt(int initValue = DefaultInvalidIndex, int validValue = DefaultInvalidIndex)
        {
            mLastData = mData = initValue;
            mInvalidValue = validValue;
        }

        public static implicit operator int(AInt change)
        {
            return (int)change.value;
        }
        public static implicit operator uint(AInt change)
        {
            return (uint)change.value;
        }
        public override string ToString()
        {
            return value.ToString();
        }
    }
    /// <summary>
    /// data drive list
    /// </summary>
    public class AList<T> : DataContainer<List<T>>
    {
        public AList()
        {
            mData = new List<T>();
            mLastData = mData;
        }
        /// <summary>
        /// only valid when add
        /// </summary>
        public T AddItem
        {
            get
            {
                return (T)DataCenter.CurrentParam;
            }
        }
        /// <summary>
        /// only valid when remove
        /// </summary>
        public T RemoveItem
        {
            get
            {
                return (T)DataCenter.CurrentParam;
            }
        }
        public T curItem
        {
            get
            {
                return (T)DataCenter.CurrentParam;
            }
        }
        void Clear()
        {
        }

        public void Add(T item, bool trigger = false)
        {
            T curItem = item;
            value.Add(item);
            if (trigger)
                Trigger();
            Clear();
        }
        public void Remove(T item, bool trigger = false)
        {
            T curItem = item;
            value.Remove(item);
            if (trigger)
                Trigger();
            Clear();
        }

        public void RemoveAt(int index, bool trigger = false)
        {
            T item = value[index];
            Remove(item, trigger);
        }
        public int FindIndex(Predicate<T> match)
        {
            return value.FindIndex(match);
        }

        public int Count
        {
            get
            {
                return value.Count;
            }
        }
        public T this[int index]
        {
            get
            {
                return value[index];
            }
            set
            {
                this.value[index] = value;
            }
        }
    }

    public class AMap<TKey, TValue> : Emmiter
    {
        Dictionary<TKey, TValue> mValue;
        public Dictionary<TKey, TValue> value
        {
            get
            {
                if (mValue == null)
                    mValue = new Dictionary<TKey, TValue>();
                return mValue;
            }
            set
            {
                mValue = value;
            }
        }
    }
}
