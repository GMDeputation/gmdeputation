#pragma checksum "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Country\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d6e8ae095abcd2d0e9c28b9ba488135717964ef6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Country_Index), @"mvc.1.0.view", @"/Views/Country/Index.cshtml")]
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
#line 2 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\_ViewImports.cshtml"
using SFA.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d6e8ae095abcd2d0e9c28b9ba488135717964ef6", @"/Views/Country/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01978648e477dbbf1387b3b505fc4c96303d1348", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Country_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/countryService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/controllers/countriesController.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Country\Index.cshtml"
  
    ViewData["Title"] = "List of Countries";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div data-ng-controller=""countriesController"">
    <div class=""topbar"">
        <div class=""md-toolbar-tools"" layout=""row"">
            <div class=""topbar-title"" flex>
                <h2>List of Countries</h2>
                <i class=""vertical-seperator"" hide-xs>&nbsp;</i>
                <span hide-xs><a href=""/home"">Home</a> / <a");
            BeginWriteAttribute("href", " href=\"", 394, "\"", 420, 2);
            WriteAttributeValue("", 401, "/nav/", 401, 5, true);
#nullable restore
#line 10 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Country\Index.cshtml"
WriteAttributeValue("", 406, ViewBag.Group, 406, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 10 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Country\Index.cshtml"
                                                                                  Write(ViewBag.Group);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a> / Countries</span>
            </div>
            <div flex=""none"">
                <md-button class=""filter-btn"" ng-click=""filter()"" ng-disabled=""selected.length>0"">
                    <md-tooltip>Filter</md-tooltip>
                    <i class=""ti-filter""></i> <span hide-xs>Filter</span>
                </md-button>
");
#nullable restore
#line 17 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Country\Index.cshtml"
                 if (ViewBag.Access == 7 || ViewBag.Access == 3)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <md-button class=""add-btn"" ng-click=""add()"" ng-disabled=""selected.length>0"">
                        <md-tooltip>Add New</md-tooltip>
                        <i class=""ti-plus""></i> Add New
                    </md-button>
                    <md-button class=""add-btn"" ng-click=""export()"" ng-disabled=""selected.length>0"">
                        <md-tooltip>Import</md-tooltip>
                        <i class=""ti-export""></i> <span hide-xs>Import</span>
                    </md-button>
");
#nullable restore
#line 27 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Country\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>
        </div>
    </div>
    <div class=""filter-grid"" data-ng-show=""showFilter"">
        <div layout=""row"" layout-xs=""column"">
            <div class=""filter-title"" flex=""none"">
                <i class=""ti-filter""></i> <span>Filter By</span>
            </div>
            <div flex>
                <md-input-container class=""md-block"">
                    <label>Name/Alpha Cde</label>
                    <input data-ng-model=""query.filter"" data-ng-model-options=""filter.options"">
                </md-input-container>
            </div>
            <div flex=""none"">
                <md-button class=""close-btn"" ng-click=""close()"" ng-disabled=""selected.length>0"">
                    <md-tooltip>Close</md-tooltip>
                    <i class=""ti-close""></i> Close
                </md-button>
            </div>
        </div>
    </div>
    <md-table-container>
        <table md-table md-progress=""promise"">
            <thead md-head md-order=""query.order"" md-on-reorder");
            WriteLiteral(@"=""search"">
                <tr md-row>
                    <th md-column class=""center"">Sl No</th>
                    <th md-column><span class=""pad-l-8"">Id</span></th>
                    <th md-column md-order-by=""name""><span class=""pad-l-8"">Name</span></th>
                    <th md-column md-order-by=""name""><span class=""pad-l-8"">French Name</span></th>
                    <th md-column md-order-by=""name""><span class=""pad-l-8"">Alpha 2 Code</span></th>
                    <th md-column md-order-by=""name""><span class=""pad-l-8"">Alpha 3 Code</span></th>
                    <th md-column class=""center"" style=""width:160px;""><span class=""pad-l-8"">Actions</span></th>
                </tr>
            </thead>
            <tbody md-body>
                <tr class=""md-row md-row-empty"" ng-show=""countries.length === 0"">
                    <td class=""md-cell"" colspan=""4"">No Data Found</td>
                </tr>
                <tr md-row data-ng-repeat=""country in countries track by $index"">
       ");
            WriteLiteral(@"             <td md-cell class=""center"">{{((query.page - 1) * query.limit) + ($index+1)}}</td>
                    <td md-cell class=""pad-l-8"">{{country.code}}</td>
                    <td md-cell class=""pad-l-8"">{{country.name}}</td>
                    <td md-cell class=""pad-l-8"">{{country.frenchName}}</td>
                    <td md-cell class=""pad-l-8"">{{country.alpha2Code}}</td>
                    <td md-cell class=""pad-l-8"">{{country.alpha3Code}}</td>
                    <td md-cell class=""center"">
");
#nullable restore
#line 75 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Country\Index.cshtml"
                         if (ViewBag.Access == 7 || ViewBag.Access == 3)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        <md-button class=""md-icon-button edit-btn"" ng-click=""edit(country.id)"">
                            <md-tooltip>Edit</md-tooltip>
                            <i class=""ti-marker-alt""></i>
                        </md-button>
                        <md-button class=""md-icon-button delete-btn"" ng-click=""delete(country.id)"">
                            <md-tooltip>Delete</md-tooltip>
                            <i class=""ti-trash""></i>
                        </md-button>
");
#nullable restore
#line 85 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Country\Index.cshtml"
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d6e8ae095abcd2d0e9c28b9ba488135717964ef610883", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 94 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Country\Index.cshtml"
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d6e8ae095abcd2d0e9c28b9ba488135717964ef612856", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 95 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Country\Index.cshtml"
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
