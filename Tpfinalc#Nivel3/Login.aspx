<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Tpfinalc_Nivel3.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-md-5">
            <div class="card mt-4">
                <div class="card-body">
                    <h3 class="mb-3">Iniciar sesión</h3>

                    <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>

                    <div class="mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control" />
                    </div>

                    <asp:Button ID="btnLogin" runat="server" Text="Ingresar"
                        CssClass="btn btn-primary w-100"
                        OnClick="btnLogin_Click" />
                    </div>
                  <div class="mt-3 text-center">
                     <a href="<%= ResolveUrl("~/ResetManual.aspx") %>">
                         ¿Olvidaste tu contraseña?
                     </a>
                   </div>
                <asp:HyperLink ID="lnkDebug" runat="server" Visible="false" CssClass="d-block mt-3" />

            </div>
        </div>
    </div>

</asp:Content>