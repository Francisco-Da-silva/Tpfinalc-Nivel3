<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetManual.aspx.cs" Inherits="Tpfinalc_Nivel3.ResetManual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-6">

                <div class="card shadow-sm">
                    <div class="card-body">
                        <h3 class="mb-3">Restablecer contraseña (Manual)</h3>

                        <p class="text-muted">
                            Si no pudimos enviarte el email, podés cambiar la contraseña manualmente con un código de soporte.
                        </p>

                        <asp:Label ID="lblMsg" runat="server" />

                        <div class="mb-3">
                            <label class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Código de soporte</label>
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" TextMode="Password" />
                            <div class="form-text">
                                Este código te lo da el administrador/soporte.
                            </div>
                        </div>

                        <hr />

                        <div class="mb-3">
                            <label class="form-label">Nueva contraseña</label>
                            <asp:TextBox ID="txtPass1" runat="server" CssClass="form-control" TextMode="Password" />
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Repetir contraseña</label>
                            <asp:TextBox ID="txtPass2" runat="server" CssClass="form-control" TextMode="Password" />
                        </div>

                        <asp:Button ID="btnCambiar" runat="server" CssClass="btn btn-warning w-100"
                            Text="Cambiar contraseña" OnClick="btnCambiar_Click" />

                        <div class="mt-3 text-center">
                            <asp:HyperLink ID="lnkVolver" runat="server" NavigateUrl="~/Login.aspx">
                                Volver al login
                            </asp:HyperLink>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>