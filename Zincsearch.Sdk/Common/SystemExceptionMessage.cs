namespace Zincsearch.Sdk.Common;
public readonly struct SystemExceptionMessage
{
    public const string TypeCannotInstantiate = "类型无法初始化";

    public const string HttpRequestFail = "Http请求失败";


    public const string NoValuesAvailable = "可空类型没有可用的值";

    public const string GetHttpResultsFail = "获取Http请求结果失败";

    public const string ChangeTypeDefaultValTypeWrong = "转换类型时默认值类型错误";

    public const string ChangeTypeDefaultValIsNull = "类型转换的时候默认值为空";

    public const string ChangeTypeWrong = "类型转换错误";

    public const string GetIHttpContextAccessorFailed = "获取请求上下文失败";

    public const string SerializationFailure = "序列化失败";
}

