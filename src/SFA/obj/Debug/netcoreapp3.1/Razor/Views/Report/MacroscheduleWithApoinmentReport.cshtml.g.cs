#pragma checksum "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Report\MacroscheduleWithApoinmentReport.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2249f1b1cf6708516df4703e25e0127c9bd044eb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Report_MacroscheduleWithApoinmentReport), @"mvc.1.0.view", @"/Views/Report/MacroscheduleWithApoinmentReport.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2249f1b1cf6708516df4703e25e0127c9bd044eb", @"/Views/Report/MacroscheduleWithApoinmentReport.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01978648e477dbbf1387b3b505fc4c96303d1348", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Report_MacroscheduleWithApoinmentReport : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/reportsService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/controllers/macroscheduleWiseAppoinmentReportsController.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Report\MacroscheduleWithApoinmentReport.cshtml"
  
    ViewData["Title"] = "Macroschedule Wise Appoinment Report";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div data-ng-controller=\"macroscheduleWiseAppoinmentReportsController\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2249f1b1cf6708516df4703e25e0127c9bd044eb5087", async() => {
                WriteLiteral(@"
    <!--Topbar section-->
<div class=""topbar"">
    <div class=""md-toolbar-tools"" layout=""row"">
        <div class=""topbar-title"" flex>
            <h2>Macroschedule Wise Appoinment Report</h2>
            <i class=""vertical-seperator"" hide-sm hide-xs>&nbsp;</i> <span hide-sm hide-xs><a href=""/home"">Home</a> / <a");
                BeginWriteAttribute("href", " href=\"", 487, "\"", 513, 2);
                WriteAttributeValue("", 494, "/nav/", 494, 5, true);
#nullable restore
#line 11 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Report\MacroscheduleWithApoinmentReport.cshtml"
WriteAttributeValue("", 499, ViewBag.Group, 499, 14, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 11 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Report\MacroscheduleWithApoinmentReport.cshtml"
                                                                                                                                               Write(ViewBag.Group);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</a> / Macroschedule Wise Appoinment Report</span>
        </div>
        <div flex=""none"">
            <md-button class=""add-btn"" aria-label=""Back to home"" href=""/home""> <md-tooltip>Back to home</md-tooltip> <i class=""ti-angle-double-left""></i> </md-button>
        </div>
    </div>
</div>
    <!--//End Topbar section-->
    <!--Filter Section-->
<div class=""container"">
    <div layout=""row"" layout-sm=""column"" layout-xs=""column"">
        <md-input-container class=""md-block"" flex=""25""> <label>Start From Date</label> <md-datepicker name=""fromDate"" ng-model=""reportParams.startFromDate"" md-placeholder=""Start From Date""></md-datepicker> </md-input-container>
        <md-input-container class=""md-block"" flex=""25"">
            <label>Start To Date</label> <md-datepicker name=""fromDate"" ng-model=""reportParams.startToDate"" md-pl aceholder=""Start To Date"" md-min-date=""reportParams.startFromDate""></md-datepicker>
        </md-input-container>
        <md-input-container class=""md-block"" flex=""25""> <label");
                WriteLiteral(@">Appoinment From Date</label> <md-datepicker name=""toDate"" ng-model=""reportParams.eventFromDate"" md-placeholder=""Appoinment From Date""></md-datepicker> </md-input-container>
        <md-input-container class=""md-block"" flex=""25"">
            <label>Appoinment To Date</label> <md-datepicker name=""toDate"" ng-model=""reportParams.eventToDate"" md-placeholder=""Appoinment To Date"" md-min-date=""reportParams.eventFromDate""></md-datepicker>
        </md-input-container>
    </div>
    <div layout=""row"" layout-align=""center center"">
        <md-button class=""save-btn"" aria-label=""Save"" data-ng-click=""search()"" data-ng-disabled=""form.$invalid""> <md-tooltip>Search</md-tooltip> <i class=""ti-search""></i> Search </md-button>
        <md-button class=""cancel-btn"" aria-label=""Cancel""");
                BeginWriteAttribute("href", " href=\"", 2336, "\"", 2362, 2);
                WriteAttributeValue("", 2343, "/nav/", 2343, 5, true);
#nullable restore
#line 33 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Report\MacroscheduleWithApoinmentReport.cshtml"
WriteAttributeValue("", 2348, ViewBag.Group, 2348, 14, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@"> <md-tooltip>Cancel</md-tooltip> <i class=""fas fa-reply""></i> Cancel </md-button>
    </div>
    <div layout=""row"" layout-sm=""column"" layout-xs=""column"">
        <md-button class=""add-btn"" aria-label=""Download"" data-ng-click=""generateExcell()"" data-ng-disabled=""searchDatas.length === 0""> <md-tooltip>Generate Excel</md-tooltip> <i class=""fas fa-download""></i> Generate Excel </md-button>
        <md-button class=""add-btn"" aria-label=""Download"" data-ng-click=""generatePdf()"" data-ng-disabled=""searchDatas.length === 0""> <md-tooltip>Generate PDF</md-tooltip> <i class=""fas fa-download""></i> Generate PDF </md-button>
        <md-button class=""add-btn"" aria-label=""Download"" data-ng-click=""generateWord()"" data-ng-disabled=""searchDatas.length === 0""> <md-tooltip>Generate Word</md-tooltip> <i class=""fas fa-download""></i> Generate Word </md-button>
    </div>
    <table md-table md-progress=""promise"">
        <thead md-head>
            <tr md-row>
                <th md-column class=""center""><span>MacroSchedul");
                WriteLiteral(@"e Description</span></th>
                <th md-column class=""center""><span>Appointment Details</span></th>
            </tr>
        </thead>
        <tbody md-body>
            <tr
                class=""md-row md-row-empty""
                ng-show=""searchDatas.l
			ength === 0""
            >
                <td class=""md-cell"" colspan=""2"">No Data Found</td>
            </tr>
            <tr md-row data-ng-repeat=""item in searchDatas track by $index"">
                <td md-cell class=""center"">{{item.description}}</td>
                <td md-cell class=""center"">
                    <table md-table md-progress=""promise"">
                        <thead md-head>
                            <tr md-row>
                                <th md-column class=""center"">Sl No</th>
                                <th md-column class=""center""><span>Church Name</span></th>
                                <th md-column class=""center""><span>Appoinment Date</span></th>
                                <t");
                WriteLiteral(@"h md-column class=""center""><span>Appoinment Time</span></th>
                                <th md-column class=""center""><span>Service Type</span></th>
                                <th md-column class=""center""><span>Pastor Name</span></th>
                            </tr>
                        </thead>
                        <tbody md-body>
                            <tr class=""md-row md-row-empty"" ng-show=""item.appoinmentDetails.length === 0"">
                                <td class=""md-cell"" colspan=""6"">No Data Found</td>
                            </tr>
                            <tr md-row data-ng-repeat=""detail in item.appoinmentDetails track by $index"">
                                <td md-cell class=""center"">{{$index+1}}</td>
                                <td md-cell class=""center"">{{detail.churchName}}</td>
                                <td md-cell class=""center"">{{detail.appoinmentDate | date: 'dd-MM-yyyy'}}</td>
                                <td md-cell class=""cente");
                WriteLiteral(@"r"">{{detail.time}}</td>
                                <td md-cell class=""center"">
                                    {{detail.serviceType}}
                                </td>
                                <td md-cell class=""center"">{{detail.pastorName}}</td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</div>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2249f1b1cf6708516df4703e25e0127c9bd044eb13869", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 94 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Report\MacroscheduleWithApoinmentReport.cshtml"
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2249f1b1cf6708516df4703e25e0127c9bd044eb15868", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#nullable restore
#line 95 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Report\MacroscheduleWithApoinmentReport.cshtml"
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
                WriteLiteral("\r\n\r\n");
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
