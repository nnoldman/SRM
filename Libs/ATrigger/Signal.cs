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
    public class Signal
    {
        public const int InvalidDataType = -1;
        public const int DefaultInvalidIndex = -1;

        public int dataType = InvalidDataType;

        public Signal()
        {
        }

        public static implicit operator bool(Signal dd)
        {
            return dd != null;
        }

        public void Trigger(params object[] args)
        {
            if (dataType == InvalidDataType)
            {
                Debug.WriteLine("Please use attribute DataCenter.DataEntry,or Use DataCenter.AObject(or AScriptObject) as its parent class!");
                return;
            }

            DataCenter.Emitter(dataType, args);
        }
        public virtual void Set(object obj)
        {

        }
        public T Arg<T>(int argIndex)
        {
            return DataCenter.GetParamByIndex<T>(argIndex);
        }
        public object[] Args
        {
            get
            {
                return DataCenter.GetParams();
            }
        }
    }
    public class ATrigger<T> : Signal
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
        public void Set(T data, bool trigger = true)
        {
            mLastData = mData;
            mData = data;
            if (mData != null && !mData.Equals(mLastData) && trigger)
                Trigger();
        }
        protected object mData = default(T);
        protected object mLastData = default(T);
    }

    public class AInt : ATrigger<int>
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
    public class AList<T> : ATrigger<List<T>>
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
                Trigger(curItem);
            Clear();
        }
        public void Remove(T item, bool trigger = false)
        {
            T curItem = item;
            value.Remove(item);
            if (trigger)
                Trigger(curItem);
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

    public class AMap<TKey, TValue> : Signal
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
