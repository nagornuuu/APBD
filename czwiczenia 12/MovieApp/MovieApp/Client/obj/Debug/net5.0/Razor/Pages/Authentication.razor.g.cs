#pragma checksum "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\Pages\Authentication.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9341234df052a244d4017fc70605210595670063"
// <auto-generated/>
#pragma warning disable 1591
namespace MovieApp.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using MovieApp.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using MovieApp.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using MovieApp.Shared.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using MovieApp.Client.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using MovieApp.Client.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\_Imports.razor"
using MovieApp.Client.Repositories;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\Pages\Authentication.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/authentication/{action}")]
    public partial class Authentication : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<global::Microsoft.AspNetCore.Components.WebAssembly.Authentication.RemoteAuthenticatorView>(0);
            __builder.AddAttribute(1, "Action", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.String>(
#nullable restore
#line 3 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\Pages\Authentication.razor"
                                  Action

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 5 "C:\Users\andrii\source\APBD\czwiczenia 12\MovieApp\MovieApp\Client\Pages\Authentication.razor"
       
    [Parameter] public string Action { get; set; }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
