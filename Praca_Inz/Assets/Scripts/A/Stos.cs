using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stos<T> where T : IStosik<T>
{
    readonly T[] obiekty;
    int licznik;

    public Stos(int rozmiar)
    {
        obiekty = new T[rozmiar];
    }

    public void Dodaj(T obiekt)
    {
        obiekt.Indeks = licznik;
        obiekty[licznik] = obiekt;
        Sort(obiekt);
        licznik++;
    }

    void Sort (T obiekt)
    {
        int pIndeks = (obiekt.Indeks - 1) / 2;

        while (true)
        {
            T poprzednik = obiekty[pIndeks];
            if (obiekt.CompareTo(poprzednik) > 0)
            {
                Zamien(obiekt, poprzednik);
            }
            else
            {
                break;
            }
            pIndeks = (obiekt.Indeks - 1) / 2;
        }
    }

    void Sort2(T obiekt)
    {
        while (true)
        {
            int leweDziecko = obiekt.Indeks * 2 + 1;
            int praweDziecko = obiekt.Indeks * 2 + 2;
            int zamien = 0;

            if (praweDziecko < licznik)
            {
                zamien = leweDziecko;
                if (obiekty[leweDziecko].CompareTo(obiekty[praweDziecko]) < 0)
                {
                    zamien = praweDziecko;
                }


                if (obiekt.CompareTo(obiekty[zamien]) < 0)
                {
                    Zamien(obiekt, obiekty[zamien]);
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

    public T Usun()
    {
        T pierwszy = obiekty[0];
        licznik = licznik - 1;
        obiekty[0] = obiekty[licznik];
        obiekty[0].Indeks = 0;
        Sort2(obiekty[0]);
        return pierwszy;
    }

    void Zamien(T obiektA, T obiektB)
    {
        obiekty[obiektA.Indeks] = obiektB;
        obiekty[obiektB.Indeks] = obiektA;

        int indeksA = obiektA.Indeks;
        obiektA.Indeks = obiektB.Indeks;
        obiektB.Indeks = indeksA;
    }

    public bool Zawiera(T obiekt)
    {
        return Equals(obiekty[obiekt.Indeks], obiekt);
    }

    public int Licz
    {
        get
        {
            return licznik;
        }
    }

    public void Aktualizuj(T obiekt)
    {
        Sort(obiekt);
    }
}

public interface IStosik<T> : IComparable<T>
{
    int Indeks
    {
        get;
        set;
  
    }
}