// AspNetCore.Views_AccomodationBook_Detail
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

[RazorSourceChecksum("SHA1", "013f96011d553631b86079c42581b587c1591280", "/Views/AccomodationBook/Detail.cshtml")]
[RazorSourceChecksum("SHA1", "01978648e477dbbf1387b3b505fc4c96303d1348", "/Views/_ViewImports.cshtml")]
public class Views_AccomodationBook_Detail : RazorPage<object>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("name", new HtmlString("form"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("src", "~/scripts/services/districtService.js", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("src", "~/scripts/services/churchService.js", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("src", "~/scripts/services/accommodationService.js", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("src", "~/scripts/services/accomodationBookService.js", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("src", "~/scripts/controllers/accommodationBookController.js", HtmlAttributeValueStyle.DoubleQuotes);

	private string __tagHelperStringValueBuffer;

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

	private UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;

	private ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;

	private TagHelperScopeManager __tagHelperScopeManager
	{
		get
		{
			if (__backed__tagHelperScopeManager == null)
			{
				__backed__tagHelperScopeManager = new TagHelperScopeManager(base.StartTagHelperWritingScope, base.EndTagHelperWritingScope);
			}
			return __backed__tagHelperScopeManager;
		}
	}

	[RazorInject]
	public IModelExpressionProvider ModelExpressionProvider { get; private set; }

	[RazorInject]
	public IUrlHelper Url { get; private set; }

	[RazorInject]
	public IViewComponentHelper Component { get; private set; }

	[RazorInject]
	public IJsonHelper Json { get; private set; }

	[RazorInject]
	public IHtmlHelper<dynamic> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = ((base.ViewBag.Id == Guid.Empty) ? "Add" : "Edit") + " Accommodation Booking";
		BeginContext(104, 60, isLiteral: true);
		WriteLiteral("<div data-ng-controller=\"accommodationBookController\">\r\n    ");
		EndContext();
		BeginContext(164, 7291, isLiteral: false);
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "6e49de733f064100bb182a59a48f89b0", async delegate
		{
			BeginContext(182, 325, isLiteral: true);
			WriteLiteral("\r\n        <div class=\"topbar\">\r\n            <div class=\"md-toolbar-tools\" layout=\"row\">\r\n                <div class=\"topbar-title\" flex>\r\n                    <h2>Details</h2>\r\n                    <i class=\"vertical-seperator\" hide-sm hide-xs>&nbsp;</i>\r\n                    <span hide-sm hide-xs><a href=\"/home\">Home</a> / <a");
			EndContext();
			BeginWriteAttribute("href", " href=\"", 507, "\"", 533, 2);
			WriteAttributeValue("", 514, "/nav/", 514, 5, isLiteral: true);
			WriteAttributeValue("", 519, base.ViewBag.Group, 519, 14, false);
			EndWriteAttribute();
			BeginContext(534, 1, isLiteral: true);
			WriteLiteral(">");
			EndContext();
			BeginContext(536, 13, isLiteral: false);
			Write(base.ViewBag.Group);
			EndContext();
			BeginContext(549, 2617, isLiteral: true);
			WriteLiteral("</a> / <a href=\"/accomodation-booking\">Accommodation Bookings</a> / Details</span>\r\n                </div>\r\n                <div flex=\"none\">\r\n                    <md-button class=\"add-btn\" aria-label=\"Back to List\" data-ng-click=\"backToList()\">\r\n                        <md-tooltip>Back to List</md-tooltip>\r\n                        <i class=\"ti-angle-double-left\"></i>\r\n                    </md-button>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"container\">\r\n            <div layout=\"row\" layout-sm=\"column\" layout-xs=\"column\">\r\n                <md-autocomplete flex class=\"padding-wrap\"\r\n                                 md-no-cache=\"true\"\r\n                                 md-selected-item=\"accommodationBook.district\"\r\n                                 md-selected-item-change=\"selectedDistrictName(district)\"\r\n                                 md-search-text=\"districtSearch\"\r\n                                 md-items=\"district in searchDistrict(districtSearch)\"\r\n                 ");
			WriteLiteral("                md-item-text=\"district.name\"\r\n                                 md-min-length=\"0\"\r\n                                 md-floating-label=\"District Name\" required>\r\n                    <md-item-template>\r\n                        <span md-highlight-text=\"districtSearch\" md-highlight-flags=\"gi\">{{district.name}}</span>\r\n                    </md-item-template>\r\n                    <md-not-found>\r\n                        No Data Found .\r\n                    </md-not-found>\r\n                </md-autocomplete>\r\n\r\n                <md-autocomplete flex class=\"padding-wrap\"\r\n                                 md-no-cache=\"true\"\r\n                                 md-selected-item=\"accommodationBook.church\"\r\n                                 md-selected-item-change=\"selectedChurchName(church)\"\r\n                                 md-search-text=\"churchSearch\"\r\n                                 md-items=\"church in searchChurch(churchSearch)\"\r\n                                 md-item-text=\"church.churchName\"\r\n         ");
			WriteLiteral("                        md-min-length=\"0\"\r\n                                 md-floating-label=\"Church Name\" required>\r\n                    <md-item-template>\r\n                        <span md-highlight-text=\"ChurchSearch\" md-highlight-flags=\"gi\">{{church.churchName}}</span>\r\n                    </md-item-template>\r\n                    <md-not-found>\r\n                        No Data Found .\r\n                    </md-not-found>\r\n                </md-autocomplete>\r\n\r\n                <md-input-container class=\"md-block\" flex>\r\n                    <input type=\"hidden\"");
			EndContext();
			BeginWriteAttribute("value", " value=\"", 3166, "\"", 3185, 1);
			WriteAttributeValue("", 3174, base.ViewBag.Id, 3174, 11, false);
			EndWriteAttribute();
			BeginContext(3186, 2967, isLiteral: true);
			WriteLiteral(" ng-model=\"id\" />\r\n                    <label>Accommodation Type</label>\r\n                    <md-select name=\"type\" ng-model=\"accommodationBook.accomodationId\" required>\r\n                        <md-option ng-repeat=\"accommodation in accommodations\" ng-value=\"accommodation.id\">{{accommodation.accomType}}</md-option>\r\n                    </md-select>\r\n                    <div ng-messages=\"form.type.$error\" md-auto-hide=\"false\">\r\n                        <div ng-message=\"required\">Accommodation Type is required</div>\r\n                    </div>\r\n                </md-input-container>\r\n            </div>\r\n            <div layout=\"row\" layout-sm=\"column\" layout-xs=\"column\">\r\n                <md-input-container class=\"md-block\" flex>\r\n                    <label>Adult No</label>\r\n                    <input type=\"number\" ng-model=\"accommodationBook.adultNo\">\r\n                </md-input-container>\r\n\r\n                <md-input-container class=\"md-block\" flex>\r\n                    <label>Child No</label>\r\n              ");
			WriteLiteral("      <input type=\"number\" ng-model=\"accommodationBook.childNo\">\r\n                </md-input-container>\r\n\r\n                <md-input-container class=\"md-block\" flex>\r\n                    <label>CheckIn Date</label>\r\n                    <md-datepicker name=\"CheckinDate\" ng-model=\"accommodationBook.checkinDate\" md-placeholder=\"Date of ChechIn\" md-min-date=\"minDate\" required></md-datepicker>\r\n                    <div ng-messages=\"form.CheckinDate.$error\" md-auto-hide=\"false\">\r\n                        <div ng-message=\"required\">CheckIn Date is required</div>\r\n                    </div>\r\n                </md-input-container>\r\n            </div>\r\n            <div layout=\"row\" layout-sm=\"column\" layout-xs=\"column\">\r\n\r\n                <md-input-container class=\"md-block\" flex>\r\n                    <label>CheckOut Date</label>\r\n                    <md-datepicker name=\"checkoutDate\" ng-model=\"accommodationBook.checkoutDate\" md-placeholder=\"Date of CheckOut\" md-min-date=\"accommodationBook.checkinDate\" required></md-date");
			WriteLiteral("picker>\r\n                    <div ng-messages=\"form.checkoutDate.$error\" md-auto-hide=\"false\">\r\n                        <div ng-message=\"required\">CheckOut Date is required</div>\r\n                    </div>\r\n                </md-input-container>\r\n\r\n                <md-time-picker ng-model=\"accommodationBook.arrivalTime\" no-meridiem message=\"message\" required flex></md-time-picker>\r\n\r\n                <md-time-picker ng-model=\"accommodationBook.departureTime\" no-meridiem message=\"message\" required flex></md-time-picker>\r\n            </div>\r\n            <div layout=\"row\" layout-sm=\"column\" layout-xs=\"column\">\r\n                <md-input-container class=\"md-block\" flex>\r\n                    <label>Reason</label>\r\n                    <input ng-model=\"accommodationBook.reason\" maxlength=\"100\">\r\n                </md-input-container>\r\n            </div>\r\n            <div layout=\"row\" layout-align=\"center center\">\r\n");
			EndContext();
			if (base.ViewBag.Id == Guid.Empty)
			{
				BeginContext(6220, 285, isLiteral: true);
				WriteLiteral("                    <md-button class=\"save-btn\" aria-label=\"Save\" data-ng-click=\"save()\" data-ng-disabled=\"form.$invalid || isDisabled\">\r\n                        <md-tooltip>Save</md-tooltip>\r\n                        <i class=\"far fa-save\"></i> Save\r\n                    </md-button>\r\n");
				EndContext();
			}
			else
			{
				BeginContext(6567, 289, isLiteral: true);
				WriteLiteral("                    <md-button class=\"save-btn\" aria-label=\"Save\" data-ng-click=\"save()\" data-ng-disabled=\"form.$invalid || isDisabled\">\r\n                        <md-tooltip>Update</md-tooltip>\r\n                        <i class=\"far fa-save\"></i> Update\r\n                    </md-button>\r\n");
				EndContext();
				BeginContext(6858, 292, isLiteral: true);
				WriteLiteral("                    <md-button class=\"add-btn\" aria-label=\"Submit\" data-ng-click=\"submit()\" data-ng-disabled=\"form.$invalid || isDisabled\">\r\n                        <md-tooltip>Submit</md-tooltip>\r\n                        <i class=\"far fa-save\"></i> Submit\r\n                    </md-button>\r\n");
				EndContext();
			}
			BeginContext(7169, 279, isLiteral: true);
			WriteLiteral("\r\n                <md-button class=\"cancel-btn\" aria-label=\"Cancel\" data-ng-click=\"backToList()\">\r\n                    <md-tooltip>Cancel</md-tooltip>\r\n                    <i class=\"fas fa-reply\"></i> Cancel\r\n                </md-button>\r\n            </div>\r\n        </div>\r\n    ");
			EndContext();
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		EndContext();
		BeginContext(7455, 12, isLiteral: true);
		WriteLiteral("\r\n</div>\r\n\r\n");
		EndContext();
		DefineSection("Scripts", (RenderAsyncDelegate)async delegate
		{
			BeginContext(7484, 6, isLiteral: true);
			WriteLiteral("\r\n    ");
			EndContext();
			BeginContext(7490, 87, isLiteral: false);
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", TagMode.StartTagAndEndTag, "fb9ebc5dc4164508a7110c2bf5e8309a", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<ScriptTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			EndContext();
			BeginContext(7577, 6, isLiteral: true);
			WriteLiteral("\r\n    ");
			EndContext();
			BeginContext(7583, 85, isLiteral: false);
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", TagMode.StartTagAndEndTag, "a6531892eb0b4bd3ad77bd6c168a2530", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<ScriptTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			EndContext();
			BeginContext(7668, 6, isLiteral: true);
			WriteLiteral("\r\n    ");
			EndContext();
			BeginContext(7674, 92, isLiteral: false);
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", TagMode.StartTagAndEndTag, "9fca7622b2d84dd293acb062064c094f", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<ScriptTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_3.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			EndContext();
			BeginContext(7766, 6, isLiteral: true);
			WriteLiteral("\r\n    ");
			EndContext();
			BeginContext(7772, 95, isLiteral: false);
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", TagMode.StartTagAndEndTag, "6f1f9d713a504b9181d882299c3ef884", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<ScriptTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_4.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			EndContext();
			BeginContext(7867, 6, isLiteral: true);
			WriteLiteral("\r\n    ");
			EndContext();
			BeginContext(7873, 102, isLiteral: false);
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", TagMode.StartTagAndEndTag, "770010bfc8184a0996980e152d8de2b6", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<ScriptTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_5.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			EndContext();
			BeginContext(7975, 2, isLiteral: true);
			WriteLiteral("\r\n");
			EndContext();
		});
	}
}
