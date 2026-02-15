<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecuperarContraseña.aspx.cs" Inherits="Tpfinalc_Nivel3.RecuperarContraseña" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center mt-5">
        <div class="col-md-5">

            <div class="card shadow">
                <div class="card-body">

                    <h3 class="text-center mb-4">Recuperar contraseña</h3>

                    <div class="mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server"
                            CssClass="form-control"
                            placeholder="Ingresá tu email" />
                    </div>

                    <div class="d-grid">
                        <asp:Button ID="btnEnviar" runat="server"
                            Text="Enviar link de recuperación"
                            CssClass="btn btn-primary"
                            OnClick="btnEnviar_Click" />
                    </div>

                    <div class="mt-3 text-center">
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
                    <asp:HyperLink ID="lnkManual" runat="server" Visible="false" CssClass="btn btn-warning mt-2" />

                    <div class="mt-3 text-center">
                        <asp:HyperLink ID="lnkDebug" runat="server" Visible="false" />
                    </div>

                    <div class="mt-3 text-center">
                        <a href="<%= ResolveUrl("~/Login.aspx") %>">Volver al login</a>
                    </div>



                </div>
            </div>

        </div>
    </div>

</asp:Content>