#pragma checksum "C:\ProjetosGit\LetsGo\LetsGo\Presentation\LetsGo.Web.UI\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1cdfb42f55f875f0f91579d6e562a1173a2a88b8"
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
#nullable restore
#line 1 "C:\ProjetosGit\LetsGo\LetsGo\Presentation\LetsGo.Web.UI\Views\_ViewImports.cshtml"
using LetsGo.Web.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\ProjetosGit\LetsGo\LetsGo\Presentation\LetsGo.Web.UI\Views\_ViewImports.cshtml"
using LetsGo.Web.UI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cdfb42f55f875f0f91579d6e562a1173a2a88b8", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"64106f958a9012fdf5fda72ceac523a55688d8dc", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LetsGo.Domain.Entities.Restaurante>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/index.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\ProjetosGit\LetsGo\LetsGo\Presentation\LetsGo.Web.UI\Views\Home\Index.cshtml"
  
    ViewBag.Title = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<!-- <header>
     <meta charset=""UTF-8"">
     <link rel=""stylesheet"" href=""~/css/site.css"">

         var id = setInterval(function() {
     //call $.ajax here
 }, 30000); // 30 seconds

         https://jsfiddle.net/mayoung/Ctg8C/
    </header>  -->
    <section>

        <div class=""caption left-align"">
            <p class=""username"">Olá, ");
#nullable restore
#line 20 "C:\ProjetosGit\LetsGo\LetsGo\Presentation\LetsGo.Web.UI\Views\Home\Index.cshtml"
                                Write(User.FindFirst("given_name")?.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <p class=\"nomeRestaurante\">");
#nullable restore
#line 21 "C:\ProjetosGit\LetsGo\LetsGo\Presentation\LetsGo.Web.UI\Views\Home\Index.cshtml"
                                  Write(Model.Nome);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
        </div>

        <!-- partial:index.partial.html -->
        <div class=""container-fluid text-center"">
            <canvas id=""canvas"" width=""812"" height=""812""></canvas>
        </div>

        <div class=""modal fade"" id=""modal"" tabindex=""-1"" role=""dialog"">
            <div class=""modal-dialog"" role=""document"">
                <div class=""modal-content"">
                    <div class=""modal-body text-center"">
                        <p id=""modal-table-id""></p>
                    </div>
                    <div class=""modal-footer"">
                        <button type=""button"" class=""btn btn-default"" data-dismiss=""modal"">OK</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- partial -->
        <script src='https://cdnjs.cloudflare.com/ajax/libs/fabric.js/1.7.11/fabric.min.js'></script>
        <script src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js'></script>
        <script type=""text/javascript");
            WriteLiteral(@""" src=""https://code.jquery.com/jquery-2.1.1.min.js""></script>
        <script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js'></script>
        <script src='https://cdn.rawgit.com/leongersen/noUiSlider/master/distribute/nouislider.min.js'></script>

        <script>
        var restaurante = ");
#nullable restore
#line 49 "C:\ProjetosGit\LetsGo\LetsGo\Presentation\LetsGo.Web.UI\Views\Home\Index.cshtml"
                     Write(Json.Serialize(Model));

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n        </script>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1cdfb42f55f875f0f91579d6e562a1173a2a88b86334", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LetsGo.Domain.Entities.Restaurante> Html { get; private set; }
    }
}
#pragma warning restore 1591
