#pragma checksum "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\ChurchServiceTime\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5187bd2f9a315a090becbec5afec69d6dd0dc031"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ChurchServiceTime_Index), @"mvc.1.0.view", @"/Views/ChurchServiceTime/Index.cshtml")]
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
#line 1 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\_ViewImports.cshtml"
using SFA;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\_ViewImports.cshtml"
using SFA.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5187bd2f9a315a090becbec5afec69d6dd0dc031", @"/Views/ChurchServiceTime/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01978648e477dbbf1387b3b505fc4c96303d1348", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ChurchServiceTime_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/churchService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/serviceTypeService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/churchServiceTimeService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/controllers/churchServiceTimesController.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\ChurchServiceTime\Index.cshtml"
  
    ViewData["Title"] = "List of Church Service Time";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div data-ng-controller=""churchServiceTimesController"">
    <div class=""topbar"">
        <div class=""md-toolbar-tools"" layout=""row"">
            <div class=""topbar-title"" flex>
                <h2>List of Church Service Times</h2>
                <i class=""vertical-seperator"" hide-xs>&nbsp;</i>
                <span hide-xs><a href=""/home"">Home</a> / <a");
            BeginWriteAttribute("href", " href=\"", 424, "\"", 450, 2);
            WriteAttributeValue("", 431, "/nav/", 431, 5, true);
#nullable restore
#line 10 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\ChurchServiceTime\Index.cshtml"
WriteAttributeValue("", 436, ViewBag.Group, 436, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 10 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\ChurchServiceTime\Index.cshtml"
                                                                                  Write(ViewBag.Group);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a> / Church Service Times</span>
            </div>
            <div flex=""none"">
                <md-button class=""filter-btn"" ng-click=""filter()"" ng-disabled=""selected.length>0"">
                    <md-tooltip>Filter</md-tooltip>
                    <i class=""ti-filter""></i> <span hide-xs>Filter</span>
                </md-button>
");
#nullable restore
#line 17 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\ChurchServiceTime\Index.cshtml"
                 if (ViewBag.Access == 7 || ViewBag.Access == 3)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <md-button class=\"add-btn\" ng-click=\"detail()\" ng-disabled=\"selected.length>0\">\r\n                        <md-tooltip>Add New</md-tooltip>\r\n                        <i class=\"ti-plus\"></i> Add New\r\n                    </md-button>\r\n");
#nullable restore
#line 26 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\ChurchServiceTime\Index.cshtml"
                                      
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
                <md-autocomplete flex class=""padding-wrap""
                                 md-no-cache=""true""
                                 md-selected-item=""church""
                                 md-selected-item-change=""selectedChurchName(church)""
                                 md-search-text=""churchSearch""
                                 md-items=""church in searchChurch(churchSearch)""
                                 md-item-text=""church.churchName""
                                 md-min-length=""0""
                                 md-floating-label=""Church Name"">
                    <md-item-template>
                        <span md-highlight-text=""ChurchSearch"" md-highlight-fl");
            WriteLiteral(@"ags=""gi"">{{church.churchName}}</span>
                    </md-item-template>
                    <md-not-found>
                        No Data Found .
                    </md-not-found>
                </md-autocomplete>
            </div>
            <div flex>
                <md-input-container class=""md-block"" flex>
                    <label>Service Type</label>
                    <md-select name=""serviceType"" ng-model=""query.serviceTypeId"" multiple>
");
            WriteLiteral(@"                        <md-option ng-repeat=""serviceType in serviceTypes"" ng-value=""serviceType.id"">{{serviceType.name}}</md-option>
                    </md-select>
                </md-input-container>
            </div>
            <div flex>
                <md-input-container class=""md-block"" flex>
                    <label>Week Day</label>
                    <md-select name=""weekDay"" ng-model=""query.weekDay"" multiple>
                        <md-option ng-repeat=""day in days"" ng-value=""day"">{{day}}</md-option>
                    </md-select>
                </md-input-container>
            </div>
            <div flex=""none"">
                <md-button class=""close-btn"" ng-click=""close()"" ng-disabled=""selected.length>0"">
                    <md-tooltip>Close</md-tooltip>
                    <i class=""ti-close""></i> Close
                </md-button>
            </div>
        </div>
        <div layout=""row"" layout-xs=""column"">
            <div flex>
                <md-time-pi");
            WriteLiteral(@"cker ng-model=""query.startTime"" no-meridiem message=""message"" flex=""none""></md-time-picker>
            </div>
            <div flex>
                <md-time-picker ng-model=""query.endTime"" no-meridiem message=""message"" flex=""none""></md-time-picker>
            </div>
            <div flex>
                <md-input-container class=""md-block"">
                    <label>Search</label>
                    <input data-ng-model=""query.filter"" placeholder=""Preferencelevel"" data-ng-model-options=""filter.options"">
                </md-input-container>
            </div>
        </div>
    </div>
    <md-table-container>
        <table md-table md-progress=""promise"">
            <thead md-head md-order=""query.order"" md-on-reorder=""search"">
                <tr md-row>
                    <th md-column class=""center"">Sl No</th>
                    <th md-column md-order-by=""name""><span class=""pad-l-8"">Church Name</span></th>
                    <th md-column md-order-by=""serviceType""><span class=""p");
            WriteLiteral(@"ad-l-8"">ServiceType Name</span></th>
                    <th md-column md-order-by=""day""><span class=""pad-l-8"">Day</span></th>
                    <th md-column md-order-by=""serviceTime""><span class=""pad-l-8"">Service Time</span></th>
                    <th md-column md-order-by=""level""><span class=""pad-l-8"">Preferencelevel</span></th>
                    <th md-column class=""center"" style=""width:160px;""><span class=""pad-l-8"">Actions</span></th>
                </tr>
            </thead>
            <tbody md-body>
                <tr class=""md-row md-row-empty"" ng-show=""churchServiceTimes.length === 0"">
                    <td class=""md-cell"" colspan=""7"">No Data Found</td>
                </tr>
                <tr md-row data-ng-repeat=""churchServiceTime in churchServiceTimes track by $index"">
                    <td md-cell class=""center"">{{((query.page - 1) * query.limit) + ($index+1)}}</td>
                    <td md-cell class=""pad-l-8"">{{churchServiceTime.churchName}}</td>
                ");
            WriteLiteral(@"    <td md-cell class=""pad-l-8"">{{churchServiceTime.serviceTypeName}}</td>
                    <td md-cell class=""pad-l-8"">{{churchServiceTime.weekDay}}</td>
                    <td md-cell class=""pad-l-8"">{{churchServiceTime.timeString}}</td>
                    <td md-cell class=""pad-l-8"">{{churchServiceTime.preferencelevel}}</td>
                    <td md-cell class=""center"">
");
#nullable restore
#line 118 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\ChurchServiceTime\Index.cshtml"
                         if (ViewBag.Access == 7 || ViewBag.Access == 3)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <md-button class=""md-icon-button edit-btn"" ng-click=""detail(churchServiceTime.id)"">
                                <md-tooltip>Edit</md-tooltip>
                                <i class=""ti-marker-alt""></i>
                            </md-button>
");
#nullable restore
#line 127 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\ChurchServiceTime\Index.cshtml"
                                              
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5187bd2f9a315a090becbec5afec69d6dd0dc03113854", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 137 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\ChurchServiceTime\Index.cshtml"
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5187bd2f9a315a090becbec5afec69d6dd0dc03115838", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 138 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\ChurchServiceTime\Index.cshtml"
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5187bd2f9a315a090becbec5afec69d6dd0dc03117822", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#nullable restore
#line 139 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\ChurchServiceTime\Index.cshtml"
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5187bd2f9a315a090becbec5afec69d6dd0dc03119806", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#nullable restore
#line 140 "C:\Users\nghia\Documents\source\gmdeputation2\gmdeputation\src\SFA\Views\ChurchServiceTime\Index.cshtml"
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
