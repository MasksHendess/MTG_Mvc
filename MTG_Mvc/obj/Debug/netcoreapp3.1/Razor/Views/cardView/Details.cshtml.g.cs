#pragma checksum "C:\Users\Max\Desktop\mvc\AspNetWebStack\MTG_Mvc\MTG_Mvc\Views\cardView\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "326dc0a252edd771f002ac92fd3dfcb87a8d5f9b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(MTG_Mvc.Pages.cardView.Views_cardView_Details), @"mvc.1.0.view", @"/Views/cardView/Details.cshtml")]
namespace MTG_Mvc.Pages.cardView
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
#line 1 "C:\Users\Max\Desktop\mvc\AspNetWebStack\MTG_Mvc\MTG_Mvc\Views\_ViewImports.cshtml"
using MTG_Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"326dc0a252edd771f002ac92fd3dfcb87a8d5f9b", @"/Views/cardView/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2f3fc62dc21309a87f44ac8207bfe7d6a29233c3", @"/Views/_ViewImports.cshtml")]
    public class Views_cardView_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MTG_Mvc.Domain.Entities.card>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Max\Desktop\mvc\AspNetWebStack\MTG_Mvc\MTG_Mvc\Views\cardView\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <div class=\"row\">\r\n        <div class=\"col-lg-3\">\r\n           <img");
            BeginWriteAttribute("src", " src=\"", 181, "\"", 202, 1);
#nullable restore
#line 10 "C:\Users\Max\Desktop\mvc\AspNetWebStack\MTG_Mvc\MTG_Mvc\Views\cardView\Details.cshtml"
WriteAttributeValue("", 187, Model.imageUrl, 187, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n        </div>\r\n        <div class=\"col-lg-3\">\r\n            ");
#nullable restore
#line 13 "C:\Users\Max\Desktop\mvc\AspNetWebStack\MTG_Mvc\MTG_Mvc\Views\cardView\Details.cshtml"
       Write(Model.name);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ");
#nullable restore
#line 13 "C:\Users\Max\Desktop\mvc\AspNetWebStack\MTG_Mvc\MTG_Mvc\Views\cardView\Details.cshtml"
                    Write(Model.cmc);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n            <p>\r\n                ");
#nullable restore
#line 17 "C:\Users\Max\Desktop\mvc\AspNetWebStack\MTG_Mvc\MTG_Mvc\Views\cardView\Details.cshtml"
           Write(Model.type);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 18 "C:\Users\Max\Desktop\mvc\AspNetWebStack\MTG_Mvc\MTG_Mvc\Views\cardView\Details.cshtml"
           Write(Model.rarity);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 19 "C:\Users\Max\Desktop\mvc\AspNetWebStack\MTG_Mvc\MTG_Mvc\Views\cardView\Details.cshtml"
           Write(Model.set);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </p> \r\n            <p>\r\n                ");
#nullable restore
#line 22 "C:\Users\Max\Desktop\mvc\AspNetWebStack\MTG_Mvc\MTG_Mvc\Views\cardView\Details.cshtml"
           Write(Model.text);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </p>\r\n            <p>\r\n                <i>  ");
#nullable restore
#line 25 "C:\Users\Max\Desktop\mvc\AspNetWebStack\MTG_Mvc\MTG_Mvc\Views\cardView\Details.cshtml"
                Write(Model.flavourText);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </i>\r\n            </p>\r\n            Artist: ");
#nullable restore
#line 27 "C:\Users\Max\Desktop\mvc\AspNetWebStack\MTG_Mvc\MTG_Mvc\Views\cardView\Details.cshtml"
               Write(Model.artist);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MTG_Mvc.Domain.Entities.card> Html { get; private set; }
    }
}
#pragma warning restore 1591
