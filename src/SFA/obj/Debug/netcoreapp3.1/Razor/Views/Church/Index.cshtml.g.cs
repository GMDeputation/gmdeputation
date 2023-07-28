#pragma checksum "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Church\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b4c068ebbe2e1f1ce228bdfe11a07e9dfb7fe587"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Church_Index), @"mvc.1.0.view", @"/Views/Church/Index.cshtml")]
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
#line 1 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\_ViewImports.cshtml"
using SFA;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\_ViewImports.cshtml"
using SFA.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b4c068ebbe2e1f1ce228bdfe11a07e9dfb7fe587", @"/Views/Church/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01978648e477dbbf1387b3b505fc4c96303d1348", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Church_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/districtService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/sectionService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/churchService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/controllers/churchesController.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Church\Index.cshtml"
  
    ViewData["Title"] = "List of Churches";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div data-ng-controller=""churchesController"">
    <!--Topbar section-->
    <div class=""topbar"">
        <div class=""md-toolbar-tools"" layout=""row"">
            <div class=""topbar-title"" flex>
                <h2>List of Churches</h2>
                <i class=""vertical-seperator"" hide-xs>&nbsp;</i>
                <span hide-xs><a href=""/home"">Home</a> / <a");
            BeginWriteAttribute("href", " href=\"", 418, "\"", 444, 2);
            WriteAttributeValue("", 425, "/nav/", 425, 5, true);
#nullable restore
#line 11 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Church\Index.cshtml"
WriteAttributeValue("", 430, ViewBag.Group, 430, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 11 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Church\Index.cshtml"
                                                                                  Write(ViewBag.Group);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a> / Churches</span>
            </div>
            <div flex=""none"">
                <md-button class=""filter-btn"" ng-click=""filter()"" ng-disabled=""selected.length>0"">
                    <md-tooltip>Filter</md-tooltip>
                    <i class=""ti-filter""></i> <span hide-xs>Filter</span>
                </md-button>
");
#nullable restore
#line 18 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Church\Index.cshtml"
                 if (ViewBag.Access == 7 || ViewBag.Access == 3)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <md-button class=""add-btn"" ng-click=""add()"" ng-disabled=""selected.length>0"">
                        <md-tooltip>Add New</md-tooltip>
                        <i class=""ti-plus""></i> <span hide-xs>Add New</span>
                    </md-button>
                    <md-button class=""add-btn"" ng-click=""viewOnMap()"" ng-disabled=""selected.length>0"">
                        <md-tooltip>View On Map</md-tooltip>
                        <i class=""ti-calendar""></i> <span hide-xs>View On Map</span>
                    </md-button>
                    <md-button class=""add-btn"" ng-click=""export()"" ng-disabled=""selected.length>0"">
                        <md-tooltip>Import</md-tooltip>
                        <i class=""ti-export""></i> <span hide-xs>Import</span>
                    </md-button>
");
#nullable restore
#line 32 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Church\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>
        </div>
    </div>
    <!--//End Topbar section-->
    <!--Filter Section-->
    <div class=""filter-grid"" data-ng-show=""showFilter"">
        <div layout=""row"" layout-xs=""column"">
            <div class=""filter-title"" flex=""none"">
                <i class=""ti-filter""></i> <span>Filter By</span>
            </div>
            <div flex>
                <md-input-container class=""md-block"">
                    <label>District</label>
                    <md-select ng-model=""query.districtId"">
                        <md-option");
            BeginWriteAttribute("value", " value=\"", 2291, "\"", 2299, 0);
            EndWriteAttribute();
            WriteLiteral(@"><em>All Districts </em></md-option>
                        <md-option ng-repeat=""district in districts track by $index"" ng-value=""district.id"">{{district.name}}</md-option>
                    </md-select>
                </md-input-container>
            </div>
            <div flex>
                <md-input-container class=""md-block"">
                    <label>Section</label>
                    <md-select ng-model=""query.sectionId"">
                        <md-option");
            BeginWriteAttribute("value", " value=\"", 2787, "\"", 2795, 0);
            EndWriteAttribute();
            WriteLiteral(@"><em>All Sections </em></md-option>
                        <md-option ng-repeat=""section in sections track by $index"" ng-value=""section.id"">{{section.name}}</md-option>
                    </md-select>
                </md-input-container>
            </div>
            <div flex>
                <md-input-container class=""md-block"">
                    <label>Church Name</label>
                    <input data-ng-model=""query.filter"" data-ng-model-options=""filter.options"">
                </md-input-container>
            </div>
            <div flex>
                <md-input-container class=""md-block"">
                    <label>Email</label>
                    <input data-ng-model=""query.email"">
                </md-input-container>
            </div>
            <div flex=""none"">
                <md-button class=""close-btn"" ng-click=""close()"" ng-disabled=""selected.length>0"">
                    <md-tooltip>Close</md-tooltip>
                    <i class=""ti-close""></i> Close
      ");
            WriteLiteral(@"          </md-button>
            </div>
        </div>
    </div>
    <!--//End Filter Section-->
    <md-table-container>
        <table md-table md-progress=""promise"">
            <thead md-head md-order=""query.order"" md-on-reorder=""search"">
                <tr md-row>
                    <th md-column class=""center"">Sl No</th>
                    <th md-column md-order-by=""name""><span class=""pad-l-8"">Church Name</span></th>
                    <th md-column><span class=""pad-l-8"">District Name</span></th>
                    <th md-column><span class=""pad-l-8"">Section Name</span></th>
                    <th md-column><span class=""pad-l-8"">Mailing Address</span></th>
                    <th md-column><span class=""pad-l-8"">Pastors</span></th>
                    <th md-column><span class=""pad-l-8"">Phone</span></th>
                    <th md-column><span class=""pad-l-8"">Email + Website</span></th>
                    <th md-column class=""center"" style=""width:160px;""><span class=""pad-l-8"">A");
            WriteLiteral(@"ctions</span></th>
                </tr>
            </thead>
            <tbody md-body>
                <tr class=""md-row md-row-empty"" ng-show=""churchs.length === 0"">
                    <td class=""md-cell"" colspan=""9"">No Data Found</td>
                </tr>
                <tr md-row data-ng-repeat=""church in churchs track by $index"">
                    <td md-cell class=""center"">{{((query.page - 1) * query.limit) + ($index+1)}}</td>
                    <td md-cell class=""pad-l-8"">{{church.churchName}}</td>
                    <td md-cell class=""pad-l-8"">{{church.districtName}}</td>
                    <td md-cell class=""pad-l-8"">{{church.sectionName}}</td>
                    <td md-cell class=""pad-l-8"">{{church.mailAddress}}</td>
                    <td md-cell class=""pad-l-8"">{{church.pastor}}</td>
                    <td md-cell class=""pad-l-8"">{{church.phone}}</td>
                    <td md-cell class=""pad-l-8"">{{church.email}},{{church.webSite}}</td>
                    <td md-cel");
            WriteLiteral("l class=\"center\">\r\n");
#nullable restore
#line 111 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Church\Index.cshtml"
                         if (ViewBag.Access == 7 || ViewBag.Access == 3)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <md-button class=""md-icon-button edit-btn"" ng-click=""edit(church.id)"">
                                <md-tooltip>Edit</md-tooltip>
                                <i class=""ti-marker-alt""></i>
                            </md-button>
                            <md-button class=""md-icon-button delete-btn"" ng-click=""delete(church.id)"">
                                <md-tooltip>Delete</md-tooltip>
                                <i class=""ti-trash""></i>
                            </md-button>
");
#nullable restore
#line 121 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Church\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    </td>
                </tr>
            </tbody>
        </table>
    </md-table-container>
    <md-table-pagination md-limit=""query.limit"" md-limit-options=""[20, 50, 100]"" md-page=""query.page"" md-total=""{{count}}"" md-on-paginate=""search"" md-page-select></md-table-pagination>
</div>
");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b4c068ebbe2e1f1ce228bdfe11a07e9dfb7fe58713760", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 130 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Church\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b4c068ebbe2e1f1ce228bdfe11a07e9dfb7fe58715728", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 131 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Church\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b4c068ebbe2e1f1ce228bdfe11a07e9dfb7fe58717696", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#nullable restore
#line 132 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Church\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b4c068ebbe2e1f1ce228bdfe11a07e9dfb7fe58719664", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#nullable restore
#line 133 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Church\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
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
