<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Tpfinalc_Nivel3.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="auth-card">
        <div class="card">
            <div class="card-body">
                <div class="page-title">
                    <h1>Iniciar sesion</h1>
                    <p>Accede a tu cuenta para administrar tus datos y favoritos.</p>
                </div>

                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>

                <div class="mb-3">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Contrasena</label>
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control" />
                </div>

                <asp:Button ID="btnLogin" runat="server" Text="Ingresar"
                    CssClass="btn btn-primary w-100"
                    OnClick="btnLogin_Click" />

                <div class="mt-3 text-center">
                    <a href="<%= ResolveUrl("~/RecuperarContrasena.aspx") %>">Olvide mi contrasena</a>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
