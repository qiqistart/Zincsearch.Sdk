using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zincsearch.Sdk.RequestModel;

public class IndexRequestModel<T>
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Data"></param>
    public IndexRequestModel(T Data)
    {
        this.Data = Data;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Data"></param>
    /// <param name="IndexName"></param>
    public IndexRequestModel(T Data, string IndexName)
    {
        this.Data = Data;
        this.IndexName = IndexName;
    }
    /// <summary>
    /// 
    /// </summary>
    public string IndexName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public T Data { get; set; }
}

