#pragma checksum "C:\Users\Thomas Hardy\Desktop\USCGA\Academia\2019 Fall\Software Engineering\Github\Projects\LIDUSCGASE\LID_Framework\LID_WebApp\Pages\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6adb088353fbc61882b62cae6f453b1abf3ba598"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(LID_WebApp.Pages.Pages_About), @"mvc.1.0.razor-page", @"/Pages/About.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6adb088353fbc61882b62cae6f453b1abf3ba598", @"/Pages/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"71ba05a1a8ba189de45e416872fa5301302edf79", @"/Pages/_ViewImports.cshtml")]
    public class Pages_About : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Thomas Hardy\Desktop\USCGA\Academia\2019 Fall\Software Engineering\Github\Projects\LIDUSCGASE\LID_Framework\LID_WebApp\Pages\About.cshtml"
  
    ViewData["Title"] = "About";
    Layout = "~/Pages/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""text-center"">
    <h1 class=""display-4"">ABOUT</h1>
    <p>FOR RESEARCH AND EDUCATIONAL PURPOSES ONLY</p>
    <div class=""text-left"">
        <p>Developed By:<br />2/c Hayden Carter<br />2/c Luke Arsenault<br />2/c Maylis Yepez<br />2/c Chrisopher Rosselot<br />2/c Thomas Hardy</p>
    </div>
</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LID_WebApp.Pages.AboutModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LID_WebApp.Pages.AboutModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LID_WebApp.Pages.AboutModel>)PageContext?.ViewData;
        public LID_WebApp.Pages.AboutModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
