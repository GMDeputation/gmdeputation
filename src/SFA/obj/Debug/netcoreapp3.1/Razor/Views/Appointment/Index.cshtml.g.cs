#pragma checksum "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3a2d4ed8a12de89875aa645858ae605c86184210"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Appointment_Index), @"mvc.1.0.view", @"/Views/Appointment/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3a2d4ed8a12de89875aa645858ae605c86184210", @"/Views/Appointment/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01978648e477dbbf1387b3b505fc4c96303d1348", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Appointment_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/appointmentService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/controllers/appointmentsController.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
  
    ViewData["Title"] = "List of Appointments";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div data-ng-controller=""appointmentsController"">
    <div class=""topbar"">
        <div class=""md-toolbar-tools"" layout=""row"">
            <div class=""topbar-title"" flex>
                <h2>List of Appointments</h2>
                <i class=""vertical-seperator"" hide-xs>&nbsp;</i>
                <span hide-xs><a href=""/home"">Home</a> / <a");
            BeginWriteAttribute("href", " href=\"", 403, "\"", 429, 2);
            WriteAttributeValue("", 410, "/nav/", 410, 5, true);
#nullable restore
#line 10 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
WriteAttributeValue("", 415, ViewBag.Group, 415, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 10 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
                                                                                  Write(ViewBag.Group);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a> / Appointments</span>
            </div>
            <div flex=""none"">
                <md-button class=""filter-btn"" ng-click=""filter()"" ng-disabled=""selected.length>0"">
                    <md-tooltip>Filter</md-tooltip>
                    <i class=""ti-filter""></i> <span hide-xs>Filter</span>
                </md-button>
");
#nullable restore
#line 17 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
                 if (ViewBag.Access == 7)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <md-button class=""add-btn"" ng-click=""add()"" ng-disabled=""selected.length>0"">
                        <md-tooltip>Add New</md-tooltip>
                        <i class=""ti-plus""></i> Add New
                    </md-button>
                    <md-button class=""add-btn"" ng-click=""viewOnCalender()"" ng-disabled=""selected.length>0"">
                        <md-tooltip>View On Calender</md-tooltip>
                        <i class=""ti-calendar""></i> <span hide-xs>View On Calender</span>
                    </md-button>
");
#nullable restore
#line 27 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
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
                <md-input-container class=""md-block"" flex>
                    <label>Search by  Church Name</label>
                    <input data-ng-model=""query.filter"" data-ng-model-options=""filter.options"">
                </md-input-container>
            </div>
            <div flex>
                <md-input-container class=""md-block"" flex=""none"">
                    <label>From Date</label>
                    <md-datepicker ng-model=""query.fromDate"" md-placeholder=""From Date""></md-datepicker>
                </md-input-container>
            </div>
            <div flex>
                <md-input-container class=""md-block"" flex=""none"">
                    <label>To Date</label>");
            WriteLiteral(@"
                    <md-datepicker ng-model=""query.toDate"" md-placeholder=""To Date""></md-datepicker>
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
");
#nullable restore
#line 62 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
     if (ViewBag.AccessCode == "P" || ViewBag.AccessCode == "M")
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <md-table-container>
            <table md-table md-row-select ng-model=""selectApointment"" multiple md-progress=""promise"">
                <thead md-head md-order=""query.order"" md-on-reorder=""search"">
                    <tr md-row>
                        <th md-column class=""center"">Sl No</th>
                        <th md-column md-order-by=""distName""><span class=""pad-l-8"">District Name</span></th>
                        <th md-column md-order-by=""macroDesc""><span class=""pad-l-8"">Macroschedule Desc</span></th>
                        <th md-column md-order-by=""name""><span class=""pad-l-8"">Church Name</span></th>
                        <th md-column><span class=""pad-l-8"">Missionaries User</span></th>
                        <th md-column md-order-by=""eventDate""><span class=""pad-l-8"">Appointment Date</span></th>
                        <th md-column md-order-by=""eventTime""><span class=""pad-l-8"">Appointment Time</span></th>
                        <th md-column><span class=""pad-l-8"">Amoun");
            WriteLiteral(@"t</span></th>
                        <th md-column><span class=""pad-l-8"">Description</span></th>
                        <th md-column class=""center""><span>Status</span></th>
                        <th md-column class=""center"" style=""width:160px;""><span class=""pad-l-8"">Actions</span></th>
                    </tr>
                </thead>
                <tbody md-body>
                    <tr class=""md-row md-row-empty"" ng-show=""appointments.length === 0"">
                        <td class=""md-cell"" colspan=""11"">No Data Found</td>
                    </tr>
                    <tr md-row md-select=""appointment.id"" md-select-id=""id"" ng-disabled=""(appointment.accessCode === 'P' && appointment.isAcceptByPastor) || (appointment.accessCode === 'M' && appointment.isAcceptMissionary)"" md-auto-select data-ng-repeat=""appointment in appointments track by $index"">
                        <td md-cell class=""center"">{{((query.page - 1) * query.limit) + ($index+1)}}</td>
                        <td md-cell cl");
            WriteLiteral(@"ass=""pad-l-8"">{{appointment.districtName}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.macroScheduleDesc}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.churchName}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.missionaryUser}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.eventDate | date: 'dd/MM/yyyy'}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.timeString}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.pimAmount}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.description}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.status}}</td>
                        <td md-cell class=""center"">
");
#nullable restore
#line 97 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
                             if (ViewBag.Access == 7 || ViewBag.Access == 3)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <md-button class=""md-icon-button edit-btn"" ng-click=""detail(appointment.id)"">
                                    <md-tooltip ng-if=""!appointment.isSubmit"">Edit</md-tooltip>
                                    <md-tooltip ng-if=""appointment.isSubmit"">View</md-tooltip>
                                    <i class=""ti-marker-alt"" ng-if=""!appointment.isSubmit""></i>
                                    <i class=""ti-eye"" ng-if=""appointment.isSubmit""></i>
                                </md-button>
");
#nullable restore
#line 105 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </td>
                    </tr>
                </tbody>
            </table>
        </md-table-container>
        <md-table-pagination md-limit=""query.limit"" md-limit-options=""[20, 50, 100]"" md-page=""query.page"" md-total=""{{count}}"" md-on-paginate=""search"" md-page-select></md-table-pagination>
        <div class=""container"" ng-if=""appointments.length > 0"">
            <div layout=""row"" layout-align=""center center"">
                <md-button class=""save-btn"" aria-label=""Approved"" data-ng-click=""approved()"">
                    <md-tooltip>Accept</md-tooltip>
                    <i class=""far fa-save""></i> Accept
                </md-button>
            </div>
        </div>
");
#nullable restore
#line 120 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <md-table-container>
            <table md-table md-progress=""promise"">
                <thead md-head md-order=""query.order"" md-on-reorder=""search"">
                    <tr md-row>
                        <th md-column class=""center"">Sl No</th>
                        <th md-column md-order-by=""distName""><span class=""pad-l-8"">District Name</span></th>
                        <th md-column md-order-by=""macroDesc""><span class=""pad-l-8"">Macroschedule Desc</span></th>
                        <th md-column md-order-by=""name""><span class=""pad-l-8"">Church Name</span></th>
                        <th md-column><span class=""pad-l-8"">Missionaries User</span></th>
                        <th md-column md-order-by=""eventDate""><span class=""pad-l-8"">Appointment Date</span></th>
                        <th md-column md-order-by=""eventTime""><span class=""pad-l-8"">Appointment Time</span></th>
                        <th md-column><span class=""pad-l-8"">Amount</span></th>
                        <th md-colum");
            WriteLiteral(@"n><span class=""pad-l-8"">Description</span></th>
                        <th md-column class=""center""><span>Status</span></th>
                        <th md-column class=""center"" style=""width:160px;""><span class=""pad-l-8"">Actions</span></th>
                    </tr>
                </thead>
                <tbody md-body>
                    <tr class=""md-row md-row-empty"" ng-show=""appointments.length === 0"">
                        <td class=""md-cell"" colspan=""11"">No Data Found</td>
                    </tr>
                    <tr md-row data-ng-repeat=""appointment in appointments track by $index"">
                        <td md-cell class=""center"">{{((query.page - 1) * query.limit) + ($index+1)}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.districtName}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.macroScheduleDesc}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.churchName}}</td>
                        <td md-cel");
            WriteLiteral(@"l class=""pad-l-8"">{{appointment.missionaryUser}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.eventDate | date: 'dd/MM/yyyy'}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.timeString}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.pimAmount}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.description}}</td>
                        <td md-cell class=""pad-l-8"">{{appointment.status}}</td>
                        <td md-cell class=""center"">
");
#nullable restore
#line 156 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
                             if (ViewBag.Access == 7 || ViewBag.Access == 3)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <md-button class=""md-icon-button edit-btn"" ng-click=""detail(appointment.id)"">
                                    <md-tooltip ng-if=""!appointment.isSubmit"">Edit</md-tooltip>
                                    <md-tooltip ng-if=""appointment.isSubmit"">View</md-tooltip>
                                    <i class=""ti-marker-alt"" ng-if=""!appointment.isSubmit""></i>
                                    <i class=""ti-eye"" ng-if=""appointment.isSubmit""></i>
                                </md-button>
");
#nullable restore
#line 164 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </td>
                    </tr>
                </tbody>
            </table>
        </md-table-container>
        <md-table-pagination md-limit=""query.limit"" md-limit-options=""[20, 50, 100]"" md-page=""query.page"" md-total=""{{count}}"" md-on-paginate=""search"" md-page-select></md-table-pagination>
");
#nullable restore
#line 171 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3a2d4ed8a12de89875aa645858ae605c8618421018230", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 174 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3a2d4ed8a12de89875aa645858ae605c8618421020208", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 175 "C:\Users\nghia\Documents\source\gmdeputation1\gmdeputation\src\SFA\Views\Appointment\Index.cshtml"
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
