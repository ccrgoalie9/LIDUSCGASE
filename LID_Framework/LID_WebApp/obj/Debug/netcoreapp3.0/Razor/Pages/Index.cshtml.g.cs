#pragma checksum "C:\Users\Thomas Hardy\Desktop\USCGA\Academia\2019 Fall\Software Engineering\Github\Projects\LIDUSCGASE\LID_Framework\LID_WebApp\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b0621693cf5d899e0857bc6455e23cab06791814"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(LID_WebApp.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace LID_WebApp.Pages
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
#line 1 "C:\Users\Thomas Hardy\Desktop\USCGA\Academia\2019 Fall\Software Engineering\Github\Projects\LIDUSCGASE\LID_Framework\LID_WebApp\Pages\_ViewImports.cshtml"
using LID_WebApp;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b0621693cf5d899e0857bc6455e23cab06791814", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"71ba05a1a8ba189de45e416872fa5301302edf79", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Thomas Hardy\Desktop\USCGA\Academia\2019 Fall\Software Engineering\Github\Projects\LIDUSCGASE\LID_Framework\LID_WebApp\Pages\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""text-center"">

    <img src=""https://upload.wikimedia.org/wikipedia/commons/thumb/5/57/Seal_of_the_United_States_Coast_Guard.svg/1200px-Seal_of_the_United_States_Coast_Guard.svg.png"" width=""50"" height=""50"" />
    <span style=""font-size:200%;"">   LID   </span>
    <img src=""https://upload.wikimedia.org/wikipedia/commons/thumb/5/57/Seal_of_the_United_States_Coast_Guard.svg/1200px-Seal_of_the_United_States_Coast_Guard.svg.png"" width=""50"" height=""50"" />

    <p>Welcome</p>
    <!--<img src=""https://octavianreport.com/wp-content/uploads/2017/08/iceberg.jpg"" width=""200"" height=""200"" />-->
    <!--<p>https://octavianreport.com/wp-content/uploads/2017/08/iceberg.jpg<br /></p>-->

    <p>Today's <a href=""https://www.navcen.uscg.gov/?pageName=iipB12Out"">bulletin</a>. Today's <a href=""https://www.navcen.uscg.gov/?pageName=iipCharts&Current"">chart</a>.</p>
    <p>The LID project is a system for the daily conversion of data released<br />by the IIP and Canadian Ice Service for personal or commerci");
            WriteLiteral("al use.</p>\r\n    <iframe src=\"https://www.google.com/maps/d/embed?mid=1sgpyQbS7_qVKUzXmX2XdcwfMMxgJhSF6\" width=\"640\" height=\"480\"></iframe>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
