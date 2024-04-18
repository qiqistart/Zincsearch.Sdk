using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zincsearch.Sdk.Common;

public static class ObjectExtension
{

    /// <summary>
    /// 是否是可空值类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsNullableType(this Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

    /// <summary>
    /// 获取可空值类型的根类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Type GetNullableRootType(this Type type) =>
        type.IsNullableType() ? type.GetGenericArguments().First() : type;
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="defaultVal"></param>
    /// <param name="isDefaultNullThrowException"></param>
    /// <param name="isExceptionReturnDefault"></param>
    /// <returns></returns>
    public static T? ChangeType<T>(
       this object? data,
       object? defaultVal = null,
       bool isDefaultNullThrowException = false,
       bool isExceptionReturnDefault = true
   )
    {
        var res = ChangeType(
            data,
            typeof(T),
            defaultVal,
            isDefaultNullThrowException,
            isExceptionReturnDefault
        );
        if (res != null)
            return (T?)res;
        return default;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="type"></param>
    /// <param name="defaultVal"></param>
    /// <param name="isDefaultNullThrowException"></param>
    /// <param name="isExceptionReturnDefault"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static object? ChangeType(
         this object? data,
         Type type,
         object? defaultVal = null,
         bool isDefaultNullThrowException = false,
         bool isExceptionReturnDefault = true
     )
    {
        object? GetDefault()
        {
            if (defaultVal != null)
            {
                try
                {
                    var defaultValByTyoe = ChangeType(defaultVal, type, null, true, false);
                    return defaultValByTyoe;
                }
                catch
                {
                    throw new Exception(SystemExceptionMessage.ChangeTypeDefaultValTypeWrong);
                }
            }

            if (isDefaultNullThrowException)
                throw new Exception(SystemExceptionMessage.ChangeTypeDefaultValIsNull);
            return null;
        }

        if (data == null)
            return GetDefault();
        if (data.GetType() == type || type.IsInstanceOfType(data))
            return data;
        try
        {
            return type == typeof(Enum)
                ? Enum.Parse(type, data.ToString()!)
                : Convert.ChangeType(
                    data,
                    type.IsNullableType() ? type.GetNullableRootType() : type
                );
        }
        catch
        {
            if (isExceptionReturnDefault)
                return GetDefault();
            throw new Exception(SystemExceptionMessage.ChangeTypeWrong);
        }
    }
}

