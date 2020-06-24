#pragma checksum "D:\Projects\VisitorManagement\Main\Views\Account\Export.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "add3523424ef4466e6a48a082e7c4aebe68e849b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Export), @"mvc.1.0.view", @"/Views/Account/Export.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/Export.cshtml", typeof(AspNetCore.Views_Account_Export))]
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
#line 1 "D:\Projects\VisitorManagement\Main\Views\_ViewImports.cshtml"
using Main;

#line default
#line hidden
#line 2 "D:\Projects\VisitorManagement\Main\Views\_ViewImports.cshtml"
using Main.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"add3523424ef4466e6a48a082e7c4aebe68e849b", @"/Views/Account/Export.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"47ef2196b1c97b7bcbe90974f3ecbec0560f8422", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Export : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Account", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ImportExcel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\Projects\VisitorManagement\Main\Views\Account\Export.cshtml"
  
    ViewData["Title"] = "Export Page";

#line default
#line hidden
            BeginContext(47, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(50, 23, false);
#line 5 "D:\Projects\VisitorManagement\Main\Views\Account\Export.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(73, 2590, true);
            WriteLiteral(@"
<script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js""></script>
<script src=""https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.3.1.min.js"">
</script>
<script type=""text/javascript"">
    $(function () {
        debugger;
        $('#btnupload').on('click', function () {
            var fileExtension = ['xls', 'xlsx'];
            var filename = $('#fileupload').val();
            if (filename.length == 0) {
                alert(""Please select a file."");
                return false;
            }
            else {
                var extension = filename.replace(/^.*\./, '');
                if ($.inArray(extension, fileExtension) == -1) {
                    alert(""Please select only excel files."");
                    return false;
                }
            }
            var fdata = new FormData();
            var fileUpload = $(""#fileupload"").get(0);
            var files = fileUpload.files;
            fdata.append(files[0].name, files[0]);
           ");
            WriteLiteral(@" $.ajax({
                type: ""POST"",
                url: ""/Account/OnPostSaveExcel"",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader(""XSRF-TOKEN"",
                        $('input:hidden[name=""__RequestVerificationToken""]').val());
                },
                data: fdata,
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response) {
                        $('#divPrint').html(response.FailedUsers + "" <br />   "" );
                        $('#divSuccessCount').html(response.successcount);
                        $('#divFailureCount').html( response.failurecount);
                    }
                    // alert('Some error occured while uploading');
                    //else {
                    //    alert('Uploaded successfully ... !!');
                    //  //  $('#divPrint').html(response);
                    //}
                },
     ");
            WriteLiteral(@"           error: function (e) {
                    $('#divPrint').html(e.responseText);
                }
            });
        })
        $('#btnExport').on('click', function () {
            var fileExtension = ['xls', 'xlsx'];
            var filename = $('#fileupload').val();
            if (filename.length == 0) {
                alert(""Please select a file then Import"");
                return false;
            }
        });
    });

</script>

<div class=""right_col"" role=""main"">
    <div class="""">
        ");
            EndContext();
            BeginContext(2663, 1208, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "24e3af3eca65492e85c8c989c50b2c11", async() => {
                BeginContext(2733, 450, true);
                WriteLiteral(@"
            <div class=""container"" style=""margin-top:10%"">
                <div class=""row"">
                    <div class=""col-md-6"">
                        <input type=""file"" id=""fileupload"" name=""files"" class=""form-control"" />
                    </div>
                    <div class=""col-md-3"">
                        <input type=""button"" name=""Upload"" value=""Upload"" id=""btnupload"" class=""btn btn-dark"" />
                        <a");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 3183, "\"", 3224, 1);
#line 79 "D:\Projects\VisitorManagement\Main\Views\Account\Export.cshtml"
WriteAttributeValue("", 3190, Url.Action("Download", "Account"), 3190, 34, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3225, 639, true);
                WriteLiteral(@">Get Template Here</a>
                    </div>
                    <br /><br /><br />                  
                </div>
                <div class=""clearfix"">&nbsp;</div>
                <div class=""row"">
                    <div id=""divSuccessCount"" class=""text-success""></div>
                </div>
                <div class=""row"">

                    <div id=""divFailureCount"" class=""text-danger""></div>
                </div>
                <div class=""row"">
                    <div id=""divPrint"" class=""text-danger""></div>
                    </div>
                </div>

            </div>
        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3871, 24, true);
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
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
