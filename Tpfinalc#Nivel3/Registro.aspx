<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Tpfinalc_Nivel3.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-md-6">
            <div class="card mt-4">
                <div class="card-body">
                    <h3 class="mb-3">Crear cuenta</h3>

                    <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Apellido</label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Email *</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Contraseña *</label>
                        <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" TextMode="Password" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Repetir contraseña *</label>
                        <asp:TextBox ID="txtPass2" runat="server" CssClass="form-control" TextMode="Password" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">URL Imagen Perfil (opcional)</label>
                        <asp:TextBox ID="txtImg" runat="server" CssClass="form-control" />
                    </div>

                    <asp:Button ID="btnRegistrar" runat="server" Text="Registrarme"
                        CssClass="btn btn-success w-100"
                        OnClick="btnRegistrar_Click" />

                    <div class="mt-3 text-center">
                        <a href="<%= ResolveUrl("~/Login.aspx") %>">Ya tengo cuenta</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
