using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Serialization;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;
using XCode.Shards;

namespace LeoChen.Cms.Data;

public partial class CmsLabel 
{
//     
//     public object GetArgument()
//     {
// // 获取扩展字段列表
//         var list = CmsModelExtfield.FindAllByModelID(1);
//
// // 创建 ExpandoObject
//         dynamic expando = new ExpandoObject();
//         var expandoDict = (IDictionary<string, object>)expando;
//
// // 解析 CmsLabel.Value 中的 JSON
//         var jsonDict =Value?.DecodeJson() ?? new Dictionary<string, object>();
//
// // 根据列表中的 NAME 定义属性
//         foreach (var field in list)
//         {
//             var fieldName = field.Name;
//             // 从 JSON 中获取对应的值，如果没有则使用默认值
//             var fieldValue = jsonDict.ContainsKey(fieldName) ? jsonDict[fieldName] : null;
//     
//             expandoDict[fieldName] = fieldValue;
//         }
//        
//
//     } 
//     
//     public object SetArgument()
//     {
//         
//     }
    

}
