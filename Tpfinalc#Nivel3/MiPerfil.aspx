<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="Tpfinalc_Nivel3.MiPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="pnlInvitado" runat="server" Visible="false" CssClass="alert alert-warning mt-3">
         <h4 class="mb-2">Todavía no tenés un perfil</h4>
         <p class="mb-3">Parece que no estás registrado o no iniciaste sesión. ¿Querés crearte una cuenta?</p>

         <a href="<%= ResolveUrl("~/Registro.aspx") %>" class="btn btn-success me-2">Registrarme</a>
         <a href="<%= ResolveUrl("~/Login.aspx") %>" class="btn btn-outline-secondary">Ya tengo cuenta</a>
    </asp:Panel>

    <asp:Panel ID="pnlPerfil" runat="server" Visible="false">

<h2>Mi Perfil</h2>

<div class="row">

    <div class="col-md-4">
        <img id="imgPerfil" runat="server"
             class="img-fluid rounded"
             src="img/no-image.png"
             onerror="this.src='img/no-image.png'" />
    </div>

    <div class="col-md-6">

        <div class="mb-3">
            <label>Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>Apellido</label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>URL Imagen</label>
            <asp:TextBox ID="txtImagen" runat="server" CssClass="form-control" />
        </div>

        <asp:Button ID="btnGuardar" runat="server"
            Text="Guardar cambios"
            CssClass="btn btn-primary"
            OnClick="btnGuardar_Click" />

    </div>

</div>

        </asp:Panel>


</asp:Content>
