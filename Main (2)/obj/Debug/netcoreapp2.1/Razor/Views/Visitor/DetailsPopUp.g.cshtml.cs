#pragma checksum "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0b5acc4b9217d69bf6468bcfef46a4b365b586d0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Visitor_DetailsPopUp), @"mvc.1.0.view", @"/Views/Visitor/DetailsPopUp.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Visitor/DetailsPopUp.cshtml", typeof(AspNetCore.Views_Visitor_DetailsPopUp))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0b5acc4b9217d69bf6468bcfef46a4b365b586d0", @"/Views/Visitor/DetailsPopUp.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"47ef2196b1c97b7bcbe90974f3ecbec0560f8422", @"/Views/_ViewImports.cshtml")]
    public class Views_Visitor_DetailsPopUp : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<VisitorManagement.DataAccess.ViewModel.ListVisitModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(63, 95, true);
            WriteLiteral("\r\n<style>\r\n    th, td {\r\n        text-align: left;\r\n        padding: 10px;\r\n    }\r\n</style>\r\n\r\n");
            EndContext();
            BeginContext(325, 22, true);
            WriteLiteral("\r\n<div>\r\n    <div>\r\n\r\n");
            EndContext();
#line 20 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
          string photo = "data: image/jpeg;base64," + Model.visit.PhotoUrl;

#line default
#line hidden
            BeginContext(425, 100, true);
            WriteLiteral("        <img style=\"width:100px;height:100px;border-radius:20px; margin-left:45%;\" class=\"mt-2 mb-2\"");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 525, "\"", 537, 1);
#line 21 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
WriteAttributeValue("", 531, photo, 531, 6, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(538, 528, true);
            WriteLiteral(@" />
        <div>
            <table class=""table "" style=""margin-right:50%;"">

                <tr style=""background-color:rgba(52,73,94,0.94); color:white; font-size:15px; height:18px; "">
                    <td class=""headings"">
                        <b> Visitor Info </b>
                    </td>
                    <td></td>
                    <td></td>

                    <td></td>

                </tr>
                <tr>
                    <td><b>Visitor Name</b></td>
                    <td>");
            EndContext();
            BeginContext(1067, 23, false);
#line 37 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                   Write(Model.visit.VisitorName);

#line default
#line hidden
            EndContext();
            BeginContext(1090, 81, true);
            WriteLiteral(" </td>\r\n                    <td><b>Designation</b></td>\r\n                    <td>");
            EndContext();
            BeginContext(1172, 23, false);
#line 39 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                   Write(Model.visit.Designation);

#line default
#line hidden
            EndContext();
            BeginContext(1195, 144, true);
            WriteLiteral("</td>\r\n                </tr>\r\n\r\n                <tr style=\"padding:10px\">\r\n                    <td><b>Company</b></td>\r\n                    <td>");
            EndContext();
            BeginContext(1340, 19, false);
#line 44 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                   Write(Model.visit.Company);

#line default
#line hidden
            EndContext();
            BeginContext(1359, 75, true);
            WriteLiteral("</td>\r\n                    <td><b>Email</b></td>\r\n                    <td> ");
            EndContext();
            BeginContext(1435, 24, false);
#line 46 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                    Write(Model.visit.EmailAddress);

#line default
#line hidden
            EndContext();
            BeginContext(1459, 140, true);
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr style=\"padding:10px\">\r\n                    <td><b>Phone</b></td>\r\n                    <td>");
            EndContext();
            BeginContext(1600, 17, false);
#line 50 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                   Write(Model.visit.Phone);

#line default
#line hidden
            EndContext();
            BeginContext(1617, 80, true);
            WriteLiteral("</td>\r\n                    <td><b>Create Date</b></td>\r\n                    <td>");
            EndContext();
            BeginContext(1698, 23, false);
#line 52 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                   Write(Model.visit.CreatedDate);

#line default
#line hidden
            EndContext();
            BeginContext(1721, 588, true);
            WriteLiteral(@"</td>
                </tr>


            </table>

        </div>

    </div>







    <div class=""body"">


        <table class=""table "">
            <tr class=""col-offset-2"" style="" background-color: rgba(52,73,94,0.94); color: white; font-size: 15px; height: 18px;"">
                <td class="" headings "">
                    <b> Visit Details </b>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>

            <tr>
                <td><b>Start Time </b></td>
                <td> ");
            EndContext();
            BeginContext(2310, 53, false);
#line 83 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                Write(string.Format("{0:hh:mm tt}", Model.detail.StartTime));

#line default
#line hidden
            EndContext();
            BeginContext(2363, 70, true);
            WriteLiteral("</td>\r\n                <td><b>End Time</b></td>\r\n                <td> ");
            EndContext();
            BeginContext(2434, 51, false);
#line 85 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                Write(string.Format("{0:hh:mm tt}", Model.detail.EndTime));

#line default
#line hidden
            EndContext();
            BeginContext(2485, 116, true);
            WriteLiteral("</td>\r\n            </tr>\r\n\r\n            <tr>\r\n                <td><b>Actual Time In</b></td>\r\n                <td>\r\n");
            EndContext();
#line 91 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                     if (Model.detail.ActualTimeIn == null)
                    {
                        

#line default
#line hidden
            BeginContext(2710, 22, false);
#line 93 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                   Write(string.Format("--:--"));

#line default
#line hidden
            EndContext();
#line 93 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                                               

                    }
                    else
                    {
                        

#line default
#line hidden
            BeginContext(2833, 56, false);
#line 98 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                   Write(string.Format("{0:hh:mm tt}", Model.detail.ActualTimeIn));

#line default
#line hidden
            EndContext();
#line 98 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                                                                                 
                    }

#line default
#line hidden
            BeginContext(2914, 98, true);
            WriteLiteral("                </td>\r\n\r\n\r\n                <td><b>Actual Time Out</b></td>\r\n                <td>\r\n");
            EndContext();
#line 105 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                     if (Model.detail.ActualTimeOut == null)
                    {
                        

#line default
#line hidden
            BeginContext(3122, 22, false);
#line 107 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                   Write(string.Format("--:--"));

#line default
#line hidden
            EndContext();
#line 107 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                                               

                    }
                    else
                    {
                        

#line default
#line hidden
            BeginContext(3245, 57, false);
#line 112 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                   Write(string.Format("{0:hh:mm tt}", Model.detail.ActualTimeOut));

#line default
#line hidden
            EndContext();
#line 112 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                                                                                  
                    }

#line default
#line hidden
            BeginContext(3327, 86, true);
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n        </table>\r\n\r\n    </div>\r\n\r\n\r\n\r\n\r\n\r\n");
            EndContext();
#line 125 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
     if (Model.participant != null)
    {



#line default
#line hidden
            BeginContext(3461, 581, true);
            WriteLiteral(@"        <div class=""body"">


            <table class=""table"">

                <tr class=""col-offset-2"" style="" background-color: rgba(52,73,94,0.94);color: white;font-size: 15px; height: 18px;"">
                    <td class="" headings "">
                        <b>   Visit Arrangement </b>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <th>Name </th>
                    <th>Description</th>
                    <th>Delegate Name</th>
                </tr>
");
            EndContext();
#line 146 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                 for (int i = 0; i < Model.arrangementsList.Count; i++)
                {

#line default
#line hidden
            BeginContext(4134, 54, true);
            WriteLiteral("                    <tr>\r\n                        <td>");
            EndContext();
            BeginContext(4189, 30, false);
#line 149 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                       Write(Model.arrangementsList[i].Name);

#line default
#line hidden
            EndContext();
            BeginContext(4219, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(4255, 42, false);
#line 150 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                       Write(Model.VisitArrangementsList[i].Description);

#line default
#line hidden
            EndContext();
            BeginContext(4297, 34, true);
            WriteLiteral("</td>\r\n                        <td");
            EndContext();
            BeginWriteAttribute("title", " title=\"", 4331, "\"", 4383, 1);
#line 151 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
WriteAttributeValue("", 4339, Model.VisitArrangementsList[i].EmailAddress, 4339, 44, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(4384, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(4386, 40, false);
#line 151 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                                                                            Write(Model.VisitArrangementsList[i].FirstName);

#line default
#line hidden
            EndContext();
            BeginContext(4426, 2, true);
            WriteLiteral("  ");
            EndContext();
            BeginContext(4429, 39, false);
#line 151 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                                                                                                                       Write(Model.VisitArrangementsList[i].LastName);

#line default
#line hidden
            EndContext();
            BeginContext(4468, 36, true);
            WriteLiteral("</td>\r\n\r\n                    </tr>\r\n");
            EndContext();
#line 154 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                }

#line default
#line hidden
            BeginContext(4523, 38, true);
            WriteLiteral("            </table>\r\n        </div>\r\n");
            EndContext();
#line 157 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"



    }

#line default
#line hidden
            BeginContext(4574, 6, true);
            WriteLiteral("\r\n\r\n\r\n");
            EndContext();
#line 164 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
     if (Model.participant != null)
    {




#line default
#line hidden
            BeginContext(4630, 507, true);
            WriteLiteral(@"        <div class=""body"">

            <table class=""table "">
                <tr class=""col-offset-2"" style=""background-color: rgba(52,73,94,0.94); color: white;font-size: 15px;height: 18px;"">
                    <td class=""headings"">
                        <b>  Participants Details </b>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <th>Participant Name </th>
                    <th>Type</th>
                </tr>
");
            EndContext();
#line 182 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                 for (int i = 0; i < Model.participant.Count; i++)
                {

#line default
#line hidden
            BeginContext(5224, 53, true);
            WriteLiteral("                    <tr>\r\n                        <td");
            EndContext();
            BeginWriteAttribute("title", " title=\"", 5277, "\"", 5319, 1);
#line 185 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
WriteAttributeValue("", 5285, Model.participant[i].EmailAddress, 5285, 34, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(5320, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(5322, 30, false);
#line 185 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                                                                  Write(Model.participant[i].FirstName);

#line default
#line hidden
            EndContext();
            BeginContext(5352, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(5354, 29, false);
#line 185 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                                                                                                  Write(Model.participant[i].LastName);

#line default
#line hidden
            EndContext();
            BeginContext(5383, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 186 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                         if (Model.participant[i].IsPrimary == true)
                        {

#line default
#line hidden
            BeginContext(5487, 58, true);
            WriteLiteral("                            <td>Primary Participant</td>\r\n");
            EndContext();
#line 189 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                        }

#line default
#line hidden
            BeginContext(5572, 24, true);
            WriteLiteral("                        ");
            EndContext();
#line 190 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                         if (Model.participant[i].IsPrimary == false)
                        {

#line default
#line hidden
            BeginContext(5670, 60, true);
            WriteLiteral("                            <td>Secondary Participant</td>\r\n");
            EndContext();
#line 193 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                        }

#line default
#line hidden
            BeginContext(5757, 27, true);
            WriteLiteral("                    </tr>\r\n");
            EndContext();
#line 195 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"
                }

#line default
#line hidden
            BeginContext(5803, 40, true);
            WriteLiteral("            </table>\r\n\r\n        </div>\r\n");
            EndContext();
#line 199 "D:\Projects\VisitorManagement\Main\Views\Visitor\DetailsPopUp.cshtml"



    }

#line default
#line hidden
            BeginContext(5856, 26, true);
            WriteLiteral("\r\n\r\n\r\n</div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<VisitorManagement.DataAccess.ViewModel.ListVisitModel> Html { get; private set; }
    }
}
#pragma warning restore 1591