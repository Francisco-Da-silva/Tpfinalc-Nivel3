<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecuperarContraseña.aspx.cs" Inherits="Tpfinalc_Nivel3.RecuperarContraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="auth-card">
        <div class="card">
            <div class="card-body">
                <div class="page-title">
                    <h1>Recuperar contrasena</h1>
                    <p>Te enviaremos un link seguro para cambiarla.</p>
                </div>

                <div class="mb-3">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server"
                        CssClass="form-control"
                        placeholder="Ingresa tu email" />
                </div>

                <div class="d-grid">
                    <asp:Button ID="btnEnviar" runat="server"
                        Text="Enviar link de recuperacion"
                        CssClass="btn btn-primary"
                        OnClick="btnEnviar_Click" />
                </div>

                <div class="mt-3 text-center">
                    <asp:Label ID="lblMsg" runat="server" />
                </div>

                <div class="mt-3 text-center">
                    <a href="<%= ResolveUrl("~/Login.aspx") %>">Volver al login</a>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
