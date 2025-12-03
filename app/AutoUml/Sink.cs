using System;
using System.Collections.Generic;

namespace AutoUml;

public sealed class Sink<T>
{
    public Sink(int count)
    {
        Count  = count;
        _items = new T[count];
    }

    public void Add(T item)
    {
        _items[_index++] = item;
    }

    public IReadOnlyList<T> ToArray()
    {
        if (_index != Count)
            throw new Exception("Invalid items count");
        return _items;
    }

    public int Count { get; }
    private readonly T[] _items;
    private int _index;
}