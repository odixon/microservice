#pragma checksum "/Users/apple/Desktop/Projects/WebApplication/WebApplication/Views/Home/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ee370c10bb9b7211d93908f5db65af6f125087c9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ee370c10bb9b7211d93908f5db65af6f125087c9", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c66e443ba81bfd444e2b1c1ae94c4deedf2b8d44", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<WebApplication.Models.Student>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<h1>\n    Home / Index\n</h1>\n\n<h2>当你看到这行字的时候，说明你已经成功登录了！</h2>\n\n<h3>");
#nullable restore
#line 9 "/Users/apple/Desktop/Projects/WebApplication/WebApplication/Views/Home/Index.cshtml"
Write(Environment.MachineName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>

<div id=""app"">
    <input type=""text"" placeholder=""type keyword"" id=""keyword""/>
    <button onclick=""window.location.href = '/?keyword=' + document.getElementById('keyword').value"">Search</button>
</div>

<table class=""table"">
    <thead>
        <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Gender</th>
            <th>Birthday</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 26 "/Users/apple/Desktop/Projects/WebApplication/WebApplication/Views/Home/Index.cshtml"
         foreach(var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\n                <td>");
#nullable restore
#line 29 "/Users/apple/Desktop/Projects/WebApplication/WebApplication/Views/Home/Index.cshtml"
               Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 30 "/Users/apple/Desktop/Projects/WebApplication/WebApplication/Views/Home/Index.cshtml"
               Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 31 "/Users/apple/Desktop/Projects/WebApplication/WebApplication/Views/Home/Index.cshtml"
               Write(item.Gender);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 32 "/Users/apple/Desktop/Projects/WebApplication/WebApplication/Views/Home/Index.cshtml"
               Write(item.Birthday);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            </tr>\n");
#nullable restore
#line 34 "/Users/apple/Desktop/Projects/WebApplication/WebApplication/Views/Home/Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<WebApplication.Models.Student>> Html { get; private set; }
    }
}
#pragma warning restore 1591
