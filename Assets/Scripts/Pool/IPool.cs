using System;

public interface IPool<T>
{
    T Create(Enum type);
    void Return(Enum type, T item);
    void PreInstantiate(int num);
}
