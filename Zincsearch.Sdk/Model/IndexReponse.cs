using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zincsearch.Sdk.Model;
/// <summary>
/// 
/// </summary>
public class IndexReponse
{

    /// <summary>
    /// 
    /// </summary>
    public string message { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string _id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string _index { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int _version { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int _seq_no { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int _primary_term { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string result { get; set; }
}

