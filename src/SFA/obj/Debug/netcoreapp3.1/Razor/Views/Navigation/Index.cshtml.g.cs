#pragma checksum "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f447675709637610d486721dc17d87782551ae54"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Navigation_Index), @"mvc.1.0.view", @"/Views/Navigation/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\_ViewImports.cshtml"
using SFA;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml"
using SFA.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml"
using SFA.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f447675709637610d486721dc17d87782551ae54", @"/Views/Navigation/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01978648e477dbbf1387b3b505fc4c96303d1348", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Navigation_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml"
  
    ViewData["Title"] = ViewBag.Category;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml"
  
    var loggedinUser = Context.Session.Get<User>("SESSIONSFAUSER");
    var accessMenus = loggedinUser.Permissions.ToList();
    var displayGroups = accessMenus.Select(m => m.MenuGroupId).Distinct().ToList();
    var accessGroups = loggedinUser.Groups.Where(m => displayGroups.Contains(m.GroupId) && m.DefaultCategory == ViewBag.Category).OrderBy(m => m.GroupPosition).ToList();

#line default
#line hidden
#nullable disable
            WriteLiteral("<div>\r\n    <md-grid-list md-cols-xs=\"1\" md-cols-sm=\"2\" md-cols-md=\"4\" md-cols-gt-md=\"4\"\r\n                  md-row-height-gt-md=\"1:1\" md-row-height=\"4:3\"\r\n                  md-gutter=\"12px\" md-gutter-gt-sm=\"8px\">\r\n\r\n");
#nullable restore
#line 17 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml"
         foreach (var accessGroup in accessGroups)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <md-grid-tile md-rowspan=\"1\" md-colspan=\"1\" md-colspan-sm=\"1\" md-colspan-xs=\"1\">\r\n                <div layout=\"column\" style=\"padding:0 12px;position: absolute;top: 0px;width: 100%;\" flex>\r\n                    <div class=\"nav-main\" flex><h3>");
#nullable restore
#line 21 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml"
                                              Write(accessGroup.GroupName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3></div>\r\n                    <div class=\"nav-main-content\" layout=\"column\" flex>\r\n");
#nullable restore
#line 23 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml"
                         foreach (var accessMenu in accessMenus.Where(m => m.MenuGroupId == accessGroup.GroupId).OrderBy(m => m.MenuPosition))
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div layout=\"row\" style=\"padding:4px;\">\r\n                                <i");
            BeginWriteAttribute("class", " class=\"", 1397, "\"", 1427, 1);
#nullable restore
#line 26 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml"
WriteAttributeValue("", 1405, accessMenu.MenuIcon, 1405, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></i>\r\n                                <div style=\"margin-left:8px;\">\r\n                                    <a");
            BeginWriteAttribute("href", " href=\"", 1537, "\"", 1568, 1);
#nullable restore
#line 28 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml"
WriteAttributeValue("", 1544, accessMenu.MenuTarget, 1544, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 28 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml"
                                                                  Write(accessMenu.MenuName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                                </div>\r\n                            </div>\r\n");
#nullable restore
#line 31 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </md-grid-tile>\r\n");
#nullable restore
#line 35 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Navigation\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </md-grid-list>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
