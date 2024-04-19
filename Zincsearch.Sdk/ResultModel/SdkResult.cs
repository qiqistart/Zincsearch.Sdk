using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zincsearch.Sdk.ResultModel;

public class SdkResult
{
    public SdkResult()
    {
    }

    public SdkResult(bool IsSuccess, string Msg)
    {
        this.IsSuccess = IsSuccess;
        this.Msg = Msg;
    }
    /// <summary>
    /// 
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Msg { get; set; } = "Error";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static SdkResult Fail(string msg)
    {
        return new SdkResult(false, msg);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static SdkResult Ok()
    {
        return new SdkResult(true, "Success");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static SdkResult<T> Ok<T>(T data)
    {
        return new SdkResult<T>(data, true, "Success");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static SdkResult<T> Fail<T>(string msg)
    {
        return new SdkResult<T>(default, false, msg);
    }

}
public class SdkResult<T> : SdkResult
{
    /// <summary>
    /// 
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Data"></param>
    /// <param name="IsSuccess"></param>
    /// <param name="msg"></param>
    protected internal SdkResult(T Data, bool IsSuccess, string msg)
        : base(IsSuccess, msg)
    {
        this.Data = Data;
    }
}
