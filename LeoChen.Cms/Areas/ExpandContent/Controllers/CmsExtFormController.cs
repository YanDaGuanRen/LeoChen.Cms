using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using LeoChen.Cms.Data;
using Microsoft.AspNetCore.Authorization;
using NewLife;
using NewLife.Caching;
using NewLife.Cube;
using NewLife.Cube.Extensions;
using NewLife.Cube.ViewModels;
using NewLife.Http;
using NewLife.Log;
using NewLife.Serialization;
using NewLife.Web;
using XCode.Membership;
using static LeoChen.Cms.Data.CmsExtForm;

namespace LeoChen.Cms.Areas.ExpandContent.Controllers;

/// <summary>自定义表单数据</summary>
[Menu(10, false, Icon = "fa-table")]
[ExpandContentArea]
public class CmsExtFormController : EntityController<CmsExtForm>
{
    static CmsExtFormController()
    {
        ListFields.RemoveCreateField().RemoveRemarkField().RemoveUpdateField();
    }

    private readonly ICacheProvider _cacheProvider;

    public CmsExtFormController(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    /// <summary>
    /// 添加自定义表单数据
    /// </summary>
    /// <param name="form">表单</param>
    /// <returns>Code Message</returns>
    [AllowAnonymous]
    [HttpPost]
    public ActionResult AddExtForm(IFormCollection form)
    {
        var userInputCaptcha = form["captchacode"].FirstOrDefault();
        var pageKey = form["captchapagekey"].FirstOrDefault() ?? "default";
        var formid = form["captchapagekey"].FirstOrDefault() ?? "default";
        var sessionCaptcha = HttpContext.Session.GetString($"Captcha_{pageKey}");
        if (string.IsNullOrEmpty(sessionCaptcha) ||
            !sessionCaptcha.Equals(userInputCaptcha, StringComparison.OrdinalIgnoreCase))
        {
            // ModelState.AddModelError("", "验证码错误");
            return Json(new { Code = -1, Message = "验证码错误" });
        }

        var entity = new CmsExtForm();
        var filteredDict = form.Where(kv => kv.Key.StartsWithIgnoreCase("CmsExp_"))
            .ToDictionary(kv => kv.Key, kv => kv.Value.FirstOrDefault());
        entity.FormValue = JsonHelper.ToJson(filteredDict, false);
        entity.SaveAsync();
        return Json(new { Code = 0, Message = "提交成功" });
    }

    protected override ActionResult IndexView(Pager p)
    {
        if (p == null || p["formid"].IsNullOrEmpty())
        {
            var ex = new ErrorModel();
            ex.Exception = new ArgumentException("未指定访问所需要的参数");
            ex.RequestId = DefaultSpan.Current?.TraceId ?? Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ex.Uri = HttpContext.Request.GetRawUrl();
            return View("Error", ex);
        }

        // 需要总记录数来分页
        p.RetrieveTotalCount = true;

        var list = SearchData(p);

        // 用于显示的列
        ViewBag.Fields = OnGetFields(ViewKinds.List, list);
        ViewBag.SearchFields = OnGetFields(ViewKinds.Search, list);

        // Json输出
        if (IsJsonRequest) return Json(0, null, list, new { page = p });

        return View("List", list);
    }

    public override ActionResult Add()
    {
        var p = ViewBag.Page as Pager;

        // 缓存数据，用于后续导出
        Session[CacheKey] = p;
        p.RetrieveTotalCount = true;

        var list = SearchData(p);

        // 用于显示的列
        ViewBag.Fields = OnGetFields(ViewKinds.List, list);
        ViewBag.SearchFields = OnGetFields(ViewKinds.Search, list);

        // Json输出
        if (IsJsonRequest) return Json(-1, "不能用户添加", list, new { page = p });
        ViewBag.Messages = new List<string> { "不能用户添加", "不能系统用户添加" };
        return View("List", list);
    }

    protected override int OnUpdate(CmsExtForm entity)
    {
        if (Request.HasFormContentType)
        {
            var aaa = Request.Form;
            var filteredDict = Request.Form.Where(kv => kv.Key.StartsWithIgnoreCase("CmsExt_"))
                .ToDictionary(kv => kv.Key.Replace("CmsExt_",""), kv => kv.Value.FirstOrDefault());
            if (filteredDict.Count > 0)
            {
                entity.FormValue = JsonHelper.ToJson(filteredDict, false);
            }
        }
        return entity.Update();
    }


    protected override FieldCollection OnGetFields(ViewKinds kind, object model)
    {
        if (kind == ViewKinds.List)
        {
            var p = ViewBag.Page as Pager;
            if (p == null || p[__.FormID].IsNullOrEmpty())
            {
                return ListFields.Clone();
            }

            ListFields.RecoveryList();
            var formid = p[__.FormID].ToInt(-1);
            var melist = CmsFormField.FindAllByFormID(formid);
            foreach (var cff in melist)
            {
                var df = ListFields.AddListField(cff.Name, null, "");
                df.DisplayName = cff.DisplayName;
                df.TextAlign = TextAligns.Center;
                df.GetValue = e =>
                {
                    if (e is ICmsExtForm extForm)
                    {
                        if (extForm.FormValue.IsNullOrEmpty()) return "";
                        var valuedic = extForm.FormValue.DecodeJson();
                        var dic = new Dictionary<string, object?>(valuedic, StringComparer.OrdinalIgnoreCase);
                        dic.TryGetValue(df.Name, out var value);
                        return value + "";
                    }
                    else
                    {
                        return "";
                    }
                };
            }
        }

        var fields = kind switch
        {
            ViewKinds.List => ListFields,
            ViewKinds.Detail => DetailFields,
            ViewKinds.AddForm => AddFormFields,
            ViewKinds.EditForm => EditFormFields,
            ViewKinds.Search => SearchFields,
            _ => ListFields,
        };
        fields = fields.Clone();
        // else if ((kind == ViewKinds.EditForm || kind == ViewKinds.Detail) && model is CmsExtForm entity2) // 表单嵌入配置字段
        // {
        //     fields.RemoveField(__.FieldValue);
        //     IDictionary<string, object?> dic = new Dictionary<string, object?>();
        //     if (!entity2.FieldValue.IsNullOrEmpty())
        //     {
        //         dic = entity2.FieldValue.DecodeJson();
        //     }
        //
        //     // if (_cacheProvider.Cache.TryGetValue<FieldCollection>($"{CacheKey}_{entity.FormID}_EditFields",
        //     //         out var value))
        //     // {
        //     //     
        //     // }
        //     // else
        //     // {
        //     //     return value;
        //     // }
        //     var melist = CmsFormField.FindAllByFormID(entity2.FormID);
        //     foreach (var cff  in melist)
        //     {
        //         var df = new FormField();
        //         df.Name = "CmsExp_" + cff.Name;
        //         df.Category = "扩展字段";
        //         df.Length = cff.Length;
        //         df.Nullable = true;
        //         df.PrimaryKey = false;
        //         df.DisplayName = cff.DisplayName;
        //         df.Description = cff.Description;
        //         df.ReadOnly = false;
        //         dic.TryGetValue(df.Name, out var v);
        //         
        //         switch (cff.FieldType)
        //         {
        //             case CmsItemType.单选:
        //                 df.Type = typeof(string);
        //                 if (v == null) v = "";
        //                 fields.Add(df);
        //                 entity2.SetItem(df.Name, v);
        //                 break;
        //             
        //             case CmsItemType.多选:
        //                 df.Type = typeof(string);
        //                 if (v == null) v = "";
        //                 fields.Add(df);
        //                 entity2.SetItem(df.Name, v);
        //                 break;
        //             
        //             case CmsItemType.时间:
        //                 df.Type = typeof(DateTime);
        //                 if (v == null) v = default(DateTime);
        //                 fields.Add(df);
        //                 entity2.SetItem(df.Name, v.ToDateTime());
        //                 break;
        //
        //             case CmsItemType.开关:
        //                 df.Type = typeof(Boolean);
        //                 if (v == null) v = true;
        //                 fields.Add(df);
        //                 entity2.SetItem(df.Name, v.ToBoolean());
        //                 break;
        //             case CmsItemType.附件:
        //
        //             case CmsItemType.图片:
        //
        //             case CmsItemType.单行文本:
        //
        //             case CmsItemType.多行文本:
        //
        //             case CmsItemType.编辑器:
        //                 
        //             default:
        //                 df.Type = typeof(string);
        //                 if (v == null) v = "";
        //                 fields.Add(df);
        //                 entity2.SetItem(df.Name, v.ToString());
        //                 break;
        //         }
        //     }
        // }

        return fields;
    }

    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsExtForm> Search(Pager p)
    {
        var formId = p["formID"].ToInt(-1);
        var formId2 = p.Params["formID"].ToInt(-1);

        return CmsExtForm.Search(formId, p["Q"], p);
    }
}