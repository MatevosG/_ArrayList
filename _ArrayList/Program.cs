using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace _ArrayList
{
    class Program
    {
        static void Main(string[] args)
        {
            _ArrayList arrayList = new _ArrayList();
            arrayList.Add(45);
            arrayList.Add("hjdgchd");
            for (int i = 0; i < arrayList.Count; i++)
            {
                Console.WriteLine(arrayList[i]);
            }

        }
    }
    public class _ArrayList
    {
        public _ArrayList(int capacity)
        {
            if (capacity < 0)
                Contract.EndContractBlock();

            if (capacity == 0)
                _items = emptyArray;
            else
                _items = new Object[capacity];
        }
        public _ArrayList()
        {
            _items = emptyArray;
        }
        internal _ArrayList(bool trash)
        {
        }
        private Object[] _items;
        private int _size;
        private int _version;
        private Object _syncRoot;
        private const int _defaultCapacity = 4;
        private static readonly Object[] emptyArray = EmptyArray<Object>.Value;
        public virtual int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return _size;
            }
        }
        public virtual int Capacity
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= Count);
                return _items.Length;
            }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                Contract.Ensures(Capacity >= 0);
                Contract.EndContractBlock();
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        Object[] newItems = new Object[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = new Object[_defaultCapacity];
                    }
                }
            }
        }
        public virtual Object this[int index]
        {
            get
            {
                if (index < 0 || index >= _size) throw new ArgumentOutOfRangeException();

                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _size) throw new ArgumentOutOfRangeException();

                _items[index] = value;
                _version++;
            }
        }
        public virtual int Add(Object value)
        {
            Contract.Ensures(Contract.Result<int>() >= 0);
            if (_size == _items.Length) EnsureCapacity(_size + 1);
            _items[_size] = value;
            _version++;
            return _size++;
        }
        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? _defaultCapacity : _items.Length * 2;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }
        public virtual void Reverse(int index, int count)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException();
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            if (_size - index < count)
                throw new ArgumentException();
            Contract.EndContractBlock();
            Array.Reverse(_items, index, count);
            _version++;
        }
        public virtual void Sort(int index, int count, IComparer comparer)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException();
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            if (_size - index < count)
                throw new ArgumentException();
            Contract.EndContractBlock();

            Array.Sort(_items, index, count, comparer);
            _version++;
        }
        public virtual void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
                _size = 0;
            }
            _version++;
        }

    }
    internal static class EmptyArray<T>
    {
        public static readonly T[] Value = new T[0];
    }
}