using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stack<T> where T : IStack<T>
{
    readonly T[] dobjects;
    int counter;

    public Stack(int size)
    {
        dobjects = new T[size];
    }

    public void Add(T dobject)
    {
        dobject.Index = counter;
        dobjects[counter] = dobject;
        Sort(dobject);
        counter++;
    }

    void Sort (T dobject)
    {
        int rIndex = (dobject.Index - 1) / 2;

        while (true)
        {
            T root = dobjects[rIndex];
            if (dobject.CompareTo(root) > 0)
            {
                Swap(dobject, root);
            }
            else
            {
                break;
            }
            rIndex = (dobject.Index - 1) / 2;
        }
    }

    void Sort2(T dobject)
    {
        while (true)
        {
            int leftNode = dobject.Index * 2 + 1;
            int rightNode = dobject.Index * 2 + 2;
            int change = 0;

            if (leftNode < counter)
            {
                change = leftNode;

                if (rightNode < counter)
                {
                    if (dobjects[leftNode].CompareTo(dobjects[rightNode]) < 0)
                    {
                        change = rightNode;
                    }
                }

                if (dobject.CompareTo(dobjects[change]) < 0)
                {
                    Swap(dobject, dobjects[change]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    public T Delete()
    {
        T first = dobjects[0];
        counter -= 1;
        dobjects[0] = dobjects[counter];
        dobjects[0].Index = 0;
        Sort2(dobjects[0]);
        return first;
    }

    void Swap(T dobjectA, T dobjectB)
    {
        dobjects[dobjectA.Index] = dobjectB;
        dobjects[dobjectB.Index] = dobjectA;

        int IndexA = dobjectA.Index;
        dobjectA.Index = dobjectB.Index;
        dobjectB.Index = IndexA;
    }

    public bool Includes(T dobject)
    {
        return Equals(dobjects[dobject.Index], dobject);
    }

    public int Count
    {
        get
        {
            return counter;
        }
    }

    public void Update(T dobject)
    {
        Sort(dobject);
    }
}

public interface IStack<T> : IComparable<T>
{
    int Index
    {
        get;
        set;
  
    }
}