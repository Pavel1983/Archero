using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExt
{
    public static List<T> GetAllExcept<T>(this List<T> list, T exceptionElement) where T : class
    {
        return list.Where(el => el != exceptionElement).ToList();
    }
}
