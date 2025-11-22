using System.Diagnostics.CodeAnalysis;
using LeoChen.Cms.Data;
using NewLife.Cube.Entity;

namespace LeoChen.Cms.TemplateEngine;

public interface ITemplateEngine
{
    string ParseTemplate(string templatePath,string url,CmsArea cmsArea,CmsSite cmsSite,CmsCompany cmsCompany,CmsContent cmsContent = null);
}