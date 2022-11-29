#pragma checksum "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "181e15913c481126f147daf242cce01b41c519fe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MacroSchedule_Index), @"mvc.1.0.view", @"/Views/MacroSchedule/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/MacroSchedule/Index.cshtml", typeof(AspNetCore.Views_MacroSchedule_Index))]
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
#line 1 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\_ViewImports.cshtml"
using SFA;

#line default
#line hidden
#line 2 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\_ViewImports.cshtml"
using SFA.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"181e15913c481126f147daf242cce01b41c519fe", @"/Views/MacroSchedule/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01978648e477dbbf1387b3b505fc4c96303d1348", @"/Views/_ViewImports.cshtml")]
    public class Views_MacroSchedule_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/macroScheduleService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/controllers/macroSchedulesController.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
  
    ViewData["Title"] = "Macro Schedules";

#line default
#line hidden
            BeginContext(51, 395, true);
            WriteLiteral(@"<div data-ng-controller=""macroSchedulesController"">
    <!--Topbar section-->
    <div class=""topbar"">
        <div class=""md-toolbar-tools"" layout=""row"">
            <div class=""topbar-title"" flex>
                <h2>List of Macro Schedules</h2>
                <i class=""vertical-seperator"" hide-sm hide-xs>&nbsp;</i>
                <span hide-sm hide-xs><a href=""/home"">Home</a> / <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 446, "\"", 472, 2);
            WriteAttributeValue("", 453, "/nav/", 453, 5, true);
#line 11 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
WriteAttributeValue("", 458, ViewBag.Group, 458, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(473, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(475, 13, false);
#line 11 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
                                                                                          Write(ViewBag.Group);

#line default
#line hidden
            EndContext();
            BeginContext(488, 308, true);
            WriteLiteral(@"</a> / Macro Schedules</span>
            </div>
            <div flex=""none"">
                <md-button class=""filter-btn"" ng-click=""filter()"">
                    <md-tooltip>Search</md-tooltip>
                    <i class=""ti-filter""></i> <span hide-xs>Filter</span>
                </md-button>
");
            EndContext();
#line 18 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
                 if (ViewBag.Access == 7)
                {

#line default
#line hidden
            BeginContext(858, 812, true);
            WriteLiteral(@"                    <md-button class=""add-btn"" ng-click=""detail()"">
                        <md-tooltip>Add New</md-tooltip>
                        <i class=""ti-plus""></i> <span hide-xs>Add New</span>
                    </md-button>
                    <md-button class=""add-btn"" ng-click=""addFromCalender()"" ng-disabled=""selected.length>0"">
                        <md-tooltip>View On Calender</md-tooltip>
                        <i class=""ti-calendar""></i> <span hide-xs>View On Calender</span>
                    </md-button>
                    <md-button class=""add-btn"" ng-click=""import()"" ng-disabled=""selected.length>0"">
                        <md-tooltip>Import</md-tooltip>
                        <i class=""ti-export""></i> <span hide-xs>Import</span>
                    </md-button>
");
            EndContext();
#line 32 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
                }

#line default
#line hidden
            BeginContext(1689, 2390, true);
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
                    <label>Search</label>
                    <input data-ng-model=""query.filter"" placeholder=""Search By Macro Schedule Description Or District Name Or Missionaries User"" data-ng-model-options=""filter.options"">
                </md-input-container>
            </div>
            <div flex=""none"">
                <md-button class=""close-btn"" ng-click=""close()"" ng-disabled=""selected.length>0"">
                    <md-tooltip>Close</md-tooltip>
                    <i class=""ti-close""></i> Close
                </md-button>
            </div>
        </div>
        ");
            WriteLiteral(@"<div layout=""row"" layout-xs=""column"">
            <div flex>
                <md-input-container class=""md-block"" flex=""none"">
                    <label>From Date</label>
                    <md-datepicker ng-model=""query.fromDate"" md-placeholder=""From Date""></md-datepicker>
                </md-input-container>
            </div>
            <div flex>
                <md-input-container class=""md-block"" flex=""none"">
                    <label>To Date</label>
                    <md-datepicker ng-model=""query.toDate"" md-placeholder=""To Date""></md-datepicker>
                </md-input-container>
            </div>
            <div flex>
                <md-input-container class=""md-block"" flex=""none"">
                    <label>From Entry Date</label>
                    <md-datepicker ng-model=""query.fromEntryDate"" md-placeholder=""From Entry Date""></md-datepicker>
                </md-input-container>
            </div>
            <div flex>
                <md-input-container class=""m");
            WriteLiteral(@"d-block"" flex=""none"">
                    <label>To Entry Date</label>
                    <md-datepicker ng-model=""query.toEntryDate"" md-placeholder=""To Entry
                                   Date""></md-datepicker>
                </md-input-container>
            </div>
        </div>
    </div>
    <!--//End Filter Section-->
");
            EndContext();
#line 85 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
     if (ViewBag.AccessCode == "D" || ViewBag.AccessCode == "S" || ViewBag.AccessCode == "A")
    {

#line default
#line hidden
            BeginContext(4181, 2628, true);
            WriteLiteral(@"        <md-table-container>
            <table md-table md-row-select ng-model=""selectSchedule"" multiple md-progress=""promise"">
                <thead md-head md-order=""query.order"" md-on-reorder=""search"">
                    <tr md-row>
                        <th md-column class=""center"">Sl No</th>
                        <th md-column md-order-by=""entryDate""><span class=""pad-l-8"">Entry Date</span></th>
                        <th md-column md-order-by=""startDate""><span class=""pad-l-8"">Start Date</span></th>
                        <th md-column md-order-by=""endDate""><span class=""pad-l-8"">End Date</span></th>
                        <th md-column md-order-by=""name""><span class=""pad-l-8"">District</span></th>
                        <th md-column><span class=""pad-l-8"">Missionaries User</span></th>
                        <th md-column><span class=""pad-l-8"">Description</span></th>
                        <th md-column><span class=""pad-l-8"">Approved Or Reject By</span></th>
                        ");
            WriteLiteral(@"<th md-column><span class=""pad-l-8"">Status</span></th>
                        <th md-column class=""center"" style=""width:160px;""><span class=""pad-l-8"">Actions</span></th>
                    </tr>
                </thead>
                <tbody md-body>
                    <tr class=""md-row md-row-empty"" ng-show=""macroSchedules.length === 0"">
                        <td class=""md-cell"" colspan=""10"">No Data Found</td>
                    </tr>
                    <tr md-row md-select=""macroSchedule.id"" md-select-id=""id"" ng-disabled=""macroSchedule.isApproved || macroSchedule.isRejected"" md-auto-select data-ng-repeat=""macroSchedule in macroSchedules track by $index"">
                        <td md-cell class=""center"">{{((query.page - 1) * query.limit) + ($index+1)}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.entryDate | date:'dd/MM/yyyy'}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.startDate | date:'dd/MM/yyyy'}}</td>
                       ");
            WriteLiteral(@" <td md-cell class=""pad-l-8"">{{macroSchedule.endDate | date:'dd/MM/yyyy'}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.districtName}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.userName}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.macroScheduleDesc}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.approvedRejectUser}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.status}}</td>
                        <td md-cell class=""center"">
");
            EndContext();
#line 118 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
                             if (ViewBag.Access == 7 || ViewBag.Access == 3)
                            {

#line default
#line hidden
            BeginContext(6918, 413, true);
            WriteLiteral(@"                                <md-button class=""md-icon-button edit-btn"" ng-click=""detail(macroSchedule.macroScheduleId)"" ng-if=""macroSchedule.accessCode == 'A' && !macroSchedule.isApproved && !macroSchedule.isRejected"">
                                    <md-tooltip>Edit Schedule</md-tooltip>
                                    <i class=""ti-marker-alt""></i>
                                </md-button>
");
            EndContext();
            BeginContext(7333, 493, true);
            WriteLiteral(@"                                <md-button class=""md-icon-button edit-btn"" ng-click=""edit(macroSchedule.id)"">
                                    <md-tooltip ng-if=""!macroSchedule.isApproved && !macroSchedule.isRejected"">Edit Schedule District</md-tooltip>
                                    <md-tooltip ng-if=""macroSchedule.isApproved || macroSchedule.isRejected"">View</md-tooltip>
                                    <i class=""ti-eye""></i>
                                </md-button>
");
            EndContext();
#line 130 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
                            }

#line default
#line hidden
            BeginContext(7857, 960, true);
            WriteLiteral(@"                        </td>
                    </tr>
                </tbody>
            </table>
        </md-table-container>
        <md-table-pagination md-limit=""query.limit"" md-limit-options=""[20, 50, 100]"" md-page=""query.page"" md-total=""{{count}}"" md-on-paginate=""search"" md-page-select></md-table-pagination>
        <div class=""container"" ng-if=""macroSchedules.length > 0"">
            <div layout=""row"" layout-align=""center center"">
                <md-button class=""save-btn"" aria-label=""Approved"" data-ng-click=""approved()"">
                    <md-tooltip>Approved</md-tooltip>
                    <i class=""far fa-save""></i> Approved
                </md-button>
                <md-button class=""cancel-btn"" aria-label=""Reject"" data-ng-click=""rejected()"">
                    <md-tooltip>Reject</md-tooltip>
                    <i class=""ti-close""></i> Reject
                </md-button>
            </div>
        </div>
");
            EndContext();
#line 149 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
    }
    else
    {

#line default
#line hidden
            BeginContext(8841, 2465, true);
            WriteLiteral(@"        <md-table-container>
            <table md-table md-progress=""promise"">
                <thead md-head md-order=""query.order"" md-on-reorder=""search"">
                    <tr md-row>
                        <th md-column class=""center"">Sl No</th>
                        <th md-column md-order-by=""entryDate""><span class=""pad-l-8"">Entry Date</span></th>
                        <th md-column md-order-by=""startDate""><span class=""pad-l-8"">Start Date</span></th>
                        <th md-column md-order-by=""endDate""><span class=""pad-l-8"">End Date</span></th>
                        <th md-column md-order-by=""name""><span class=""pad-l-8"">District</span></th>
                        <th md-column><span class=""pad-l-8"">Missionaries User</span></th>
                        <th md-column><span class=""pad-l-8"">Description</span></th>
                        <th md-column><span class=""pad-l-8"">Approved Or Reject By</span></th>
                        <th md-column><span class=""pad-l-8"">Status</span>");
            WriteLiteral(@"</th>
                        <th md-column class=""center"" style=""width:160px;""><span class=""pad-l-8"">Actions</span></th>
                    </tr>
                </thead>
                <tbody md-body>
                    <tr class=""md-row md-row-empty"" ng-show=""macroSchedules.length === 0"">
                        <td class=""md-cell"" colspan=""10"">No Data Found</td>
                    </tr>
                    <tr md-row md-auto-select data-ng-repeat=""macroSchedule in macroSchedules track by $index"">
                        <td md-cell class=""center"">{{((query.page - 1) * query.limit) + ($index+1)}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.entryDate | date:'dd/MM/yyyy'}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.startDate | date:'dd/MM/yyyy'}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.endDate | date:'dd/MM/yyyy'}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.districtName}}");
            WriteLiteral(@"</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.userName}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.macroScheduleDesc}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.approvedRejectUser}}</td>
                        <td md-cell class=""pad-l-8"">{{macroSchedule.status}}</td>
                        <td md-cell class=""center"">
");
            EndContext();
#line 183 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
                             if (ViewBag.Access == 7 || ViewBag.Access == 3)
                            {

#line default
#line hidden
            BeginContext(11415, 413, true);
            WriteLiteral(@"                                <md-button class=""md-icon-button edit-btn"" ng-click=""detail(macroSchedule.macroScheduleId)"" ng-if=""macroSchedule.accessCode == 'A' && !macroSchedule.isApproved && !macroSchedule.isRejected"">
                                    <md-tooltip>Edit Schedule</md-tooltip>
                                    <i class=""ti-marker-alt""></i>
                                </md-button>
");
            EndContext();
            BeginContext(11830, 493, true);
            WriteLiteral(@"                                <md-button class=""md-icon-button edit-btn"" ng-click=""edit(macroSchedule.id)"">
                                    <md-tooltip ng-if=""!macroSchedule.isApproved && !macroSchedule.isRejected"">Edit Schedule District</md-tooltip>
                                    <md-tooltip ng-if=""macroSchedule.isApproved || macroSchedule.isRejected"">View</md-tooltip>
                                    <i class=""ti-eye""></i>
                                </md-button>
");
            EndContext();
#line 195 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
                            }

#line default
#line hidden
            BeginContext(12354, 327, true);
            WriteLiteral(@"                        </td>
                    </tr>
                </tbody>
            </table>
        </md-table-container>
        <md-table-pagination md-limit=""query.limit"" md-limit-options=""[20, 50, 100]"" md-page=""query.page"" md-total=""{{count}}"" md-on-paginate=""search"" md-page-select></md-table-pagination>
");
            EndContext();
#line 202 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
    }

#line default
#line hidden
            BeginContext(12688, 10, true);
            WriteLiteral("</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(12715, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(12721, 92, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "181e15913c481126f147daf242cce01b41c519fe20681", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 206 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(12813, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(12819, 99, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "181e15913c481126f147daf242cce01b41c519fe22760", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 207 "C:\Users\SEANR\source\repos\SFAMigration3\SFA\Views\MacroSchedule\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(12918, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
