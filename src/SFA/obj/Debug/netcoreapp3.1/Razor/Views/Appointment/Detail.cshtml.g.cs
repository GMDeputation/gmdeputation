#pragma checksum "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Appointment\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "42361ff979ae94a922f13764edbca21ca5cca653"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Appointment_Detail), @"mvc.1.0.view", @"/Views/Appointment/Detail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42361ff979ae94a922f13764edbca21ca5cca653", @"/Views/Appointment/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01978648e477dbbf1387b3b505fc4c96303d1348", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Appointment_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/macroScheduleService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/churchServiceTimeService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/churchService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/services/appointmentService.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/controllers/appointmentDetailController.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Appointment\Detail.cshtml"
  
    ViewData["Title"] = (ViewBag.Id == 0 ? "Add" : "Edit") + " Appointment";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div data-ng-controller=\"appointmentDetailController\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "42361ff979ae94a922f13764edbca21ca5cca6535911", async() => {
                WriteLiteral(@"
        <div class=""topbar"">
            <div class=""md-toolbar-tools"" layout=""row"">
                <div class=""topbar-title"" flex>
                    <h2>Details</h2>
                    <i class=""vertical-seperator"" hide-xs>&nbsp;</i>
                    <span hide-xs><a href=""/home"">Home</a> / <a");
                BeginWriteAttribute("href", " href=\"", 472, "\"", 498, 2);
                WriteAttributeValue("", 479, "/nav/", 479, 5, true);
#nullable restore
#line 11 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Appointment\Detail.cshtml"
WriteAttributeValue("", 484, ViewBag.Group, 484, 14, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 11 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Appointment\Detail.cshtml"
                                                                                      Write(ViewBag.Group);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</a> / <a href=""/appointments"">Appointments</a> / Details</span>
                </div>submu
                <div flex=""none"">
                    <md-button class=""add-btn"" aria-label=""Back to List"" data-ng-click=""backToList()"">
                        <md-tooltip>Back to List</md-tooltip>
                        <i class=""ti-angle-double-left""></i> Back to List
                    </md-button>
                </div>
            </div>
        </div>
        <div class=""container"">
            <div layout=""row"" layout-xs=""column"">
                <md-input-container class=""md-block"" flex>
                    <label>Appointment Date</label>
                    <md-datepicker name=""eventDate"" ng-model=""appointment.eventDate"" md-placeholder=""Date of Appointment"" md-min-date=""minDate"" md-max-date=""maxDate"" required ng-change=""dateChange()"" ng-disabled=""appointment.isSubmit""></md-datepicker>
                    <div ng-messages=""form.eventDate.$error"" md-auto-hide=""false"">
                        ");
                WriteLiteral(@"<div ng-message=""required"">Appointment Date is required</div>
                    </div>
                </md-input-container>
                <md-autocomplete flex class=""padding-wrap""
                                 md-no-cache=""true""
                                 md-selected-item=""church""
                                 md-selected-item-change=""selectedChurchName(church)""
                                 md-search-text=""churchSearch""
                                 md-items=""church in searchChurch(churchSearch)""
                                 md-item-text=""church.churchName""
                                 md-min-length=""0""
                                 md-floating-label=""Church Name"" required ng-readonly=""appointment.isSubmit"">
                    <md-item-template>
                        <span md-highlight-text=""ChurchSearch"" md-highlight-flags=""gi"">{{church.churchName}}</span>
                    </md-item-template>
                    <md-not-found>
                        ");
                WriteLiteral(@"No Data Found .
                    </md-not-found>
                </md-autocomplete>
                <md-input-container class=""md-block"" flex>
                    <label>Appointment Time</label>
                    <md-select name=""eventTime"" ng-model=""appointment.eventTime"" required ng-disabled=""appointment.isSubmit"">
                        <md-option ng-repeat=""time in times"" ng-value=""time.serviceTime"">{{time.timeString}}</md-option>
                    </md-select>
                    <div ng-messages=""form.eventTime.$error"" md-auto-hide=""false"">
                        <div ng-message=""required"">Appointment is required</div>
                    </div>
                </md-input-container>
            </div>
");
                WriteLiteral(@"            <div layout=""row"" layout-xs=""column"">
                <md-input-container class=""md-block"" flex>
                    <label>Description</label>
                    <input name=""description"" ng-model=""appointment.description"" ng-required=""appointment.accessCode =='P' || appointment.accessCode =='M'"" ng-readonly=""(appointment.isAcceptByPastor && appointment.accessCode =='P') || (appointment.isAcceptMissionary && appointment.accessCode =='M') || (appointment.accessCode !='A' && appointment.accessCode !='M'&& appointment.accessCode !='P') || appointment.isAcceptMissionary"" maxlength=""100"">
                    <div ng-messages=""form.description.$error"" md-auto-hide=""false"">
                        <div ng-message=""required"">Description is required</div>
                    </div>
                </md-input-container>
                <md-input-container class=""md-block"" flex ng-if=""appointment.isSubmit"">
                    <label>Amount</label>
                    <input name=""pimAmount"" type");
                WriteLiteral(@"=""number"" ng-model=""appointment.pimAmount"" ng-required=""appointment.accessCode =='P' || appointment.accessCode =='M'"" ng-readonly=""(appointment.isAcceptByPastor && appointment.accessCode =='P') || (appointment.isAcceptMissionary && appointment.accessCode =='M') || (appointment.accessCode !='A' && appointment.accessCode !='M'&& appointment.accessCode !='P') ||appointment.isAcceptMissionary"">
                    <div ng-messages=""form.pimAmount.$error"" md-auto-hide=""false"">
                        <div ng-message=""required"">Amount is required</div>
                    </div>
                </md-input-container>
                <md-input-container class=""md-block"" flex ng-if=""appointment.isSubmit"">
                    <label>Offering</label>
                    <input name=""offering"" ng-model=""appointment.offering"" ng-required=""appointment.accessCode =='P' || appointment.accessCode =='M'"" maxlength=""100"" ng-readonly=""(appointment.isAcceptByPastor && appointment.accessCode =='P') || (appointment.isAcceptM");
                WriteLiteral(@"issionary && appointment.accessCode =='M') || (appointment.accessCode !='A' && appointment.accessCode !='M'&& appointment.accessCode !='P') ||appointment.isAcceptMissionary"">
                    <div ng-messages=""form.offering.$error"" md-auto-hide=""false"">
                        <div ng-message=""required"">Offering is required</div>
                    </div>
                </md-input-container>
            </div>
            <div layout=""row"" layout-xs=""column"" ng-if=""appointment.isSubmit"">
                <md-input-container class=""md-block"" flex>
                    <label>Notes</label>
                    <input ng-model=""appointment.notes"" maxlength=""100"" ng-readonly=""(appointment.isAcceptByPastor && appointment.accessCode =='P') || (appointment.isAcceptMissionary && appointment.accessCode =='M') || (appointment.accessCode !='A' && appointment.accessCode !='M'&& appointment.accessCode !='P') ||appointment.isAcceptMissionary"">
                </md-input-container>
            </div>
         ");
                WriteLiteral(@"   <div layout=""row"" layout-xs=""column"" ng-if=""appointment.isSubmit"">
                <md-input-container class=""md-block"" flex>
                    <label>Pastor Remarks</label>
                    <input ng-model=""appointment.acceptByPastorRemarks"" maxlength=""100"" ng-required=""appointment.accessCode =='P'"" ng-readonly=""appointment.isAcceptByPastor"">
                </md-input-container>
            </div>
            <div layout=""row"" layout-xs=""column"" ng-if=""(appointment.isForwardForMissionary && appointment.accessCode !='P')"">
                <md-input-container class=""md-block"" flex>
                    <label>Missionary Remarks</label>
                    <input ng-model=""appointment.acceptMissionaryRemarks"" maxlength=""100"" ng-required=""appointment.accessCode =='M'"" ng-readonly=""appointment.isAcceptMissionary || (appointment.accessCode !='M' && appointment.accessCode !='A')"" >
                </md-input-container>
            </div>
            <div layout=""row"" layout-align=""center center""");
                WriteLiteral(@">
                <md-button class=""save-btn"" aria-label=""Save"" data-ng-click=""save()"" data-ng-disabled=""form.$invalid || isDiabled"" ng-if=""!appointment.isSubmit || (appointment.isSubmit && !appointment.isAcceptByPastor && appointment.accessCode =='P') || (appointment.isSubmit && !appointment.isAcceptMissionary && (appointment.accessCode =='M' || appointment.accessCode =='A'))"">
                    <md-tooltip>Save As Draft</md-tooltip>
                    <i class=""far fa-save""></i> Save As Draft
                </md-button>
                <md-button class=""add-btn"" aria-label=""Save"" data-ng-click=""submit()"" data-ng-disabled=""form.$invalid || isDiabled"" ng-if=""!appointment.isSubmit"">
                    <md-tooltip>Submit</md-tooltip>
                    <i class=""far fa-save""></i> Submit
                </md-button>
                <md-button class=""add-btn"" aria-label=""Save"" data-ng-click=""acceptPastor()"" data-ng-disabled=""form.$invalid || isDiabled"" ng-if=""appointment.isSubmit && !appointment.is");
                WriteLiteral(@"AcceptByPastor && appointment.accessCode =='P' || appointment.isSubmit && !appointment.isAcceptByPastor && appointment.accessCode =='A'"">
                    <md-tooltip>Accept</md-tooltip>
                    <i class=""far fa-save""></i> Accept
                </md-button>
                <md-button class=""add-btn"" aria-label=""Save"" data-ng-click=""forwardMissionary()"" data-ng-disabled=""form.$invalid || isDiabled"" ng-if=""appointment.isSubmit && appointment.isAcceptByPastor && !appointment.isForwardForMissionary &&(appointment.accessCode =='D' || appointment.accessCode =='S' || appointment.accessCode =='A')"">
                    <md-tooltip>Send To Missionary</md-tooltip>
                    <i class=""far fa-save""></i> Send To Missionary
                </md-button>
                <md-button class=""add-btn"" aria-label=""Save"" data-ng-click=""acceptMissionary()"" data-ng-disabled=""form.$invalid || isDiabled"" ng-if=""(appointment.isForwardForMissionary && !appointment.isAcceptMissionary && appointment.access");
                WriteLiteral(@"Code =='M') || (appointment.isForwardForMissionary && !appointment.isAcceptMissionary && appointment.accessCode =='A')"">
                    <md-tooltip>Accept</md-tooltip>
                    <i class=""far fa-save""></i> Accept
                </md-button>
                <md-button class=""cancel-btn"" aria-label=""Cancel"" data-ng-click=""backToList()"">
                    <md-tooltip>Cancel</md-tooltip>
                    <i class=""fas fa-reply""></i> Cancel
                </md-button>
            </div>
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "42361ff979ae94a922f13764edbca21ca5cca65318488", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 157 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Appointment\Detail.cshtml"
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "42361ff979ae94a922f13764edbca21ca5cca65320462", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#nullable restore
#line 158 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Appointment\Detail.cshtml"
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "42361ff979ae94a922f13764edbca21ca5cca65322436", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#nullable restore
#line 159 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Appointment\Detail.cshtml"
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "42361ff979ae94a922f13764edbca21ca5cca65324410", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
#nullable restore
#line 160 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Appointment\Detail.cshtml"
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "42361ff979ae94a922f13764edbca21ca5cca65326384", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
#nullable restore
#line 161 "C:\Users\SEANR\OneDrive\Documents\GitHub\gmdeputation\src\SFA\Views\Appointment\Detail.cshtml"
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
