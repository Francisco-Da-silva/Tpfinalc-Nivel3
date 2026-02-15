<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="Tpfinalc_Nivel3.Detalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="pnlDetalle" runat="server" Visible="false">

        <div class="row">

            <div class="col-md-5">
                <img id="imgProducto" runat="server"
                     class="img-fluid rounded"
                     src="img/no-image.png"
                     onerror="this.src='img/no-image.png'" />
            </div>

            <div class="col-md-7">
                <h2><asp:Label ID="lblNombre" runat="server" /></h2>
                <h4 class="text-success">$ <asp:Label ID="lblPrecio" runat="server" /></h4>

                <p><strong>Código:</strong> <asp:Label ID="lblCodigo" runat="server" /></p>
                <p><strong>Marca:</strong> <asp:Label ID="lblMarca" runat="server" /></p>
                <p><strong>Categoría:</strong> <asp:Label ID="lblCategoria" runat="server" /></p>
                <p><strong>Categoría:</strong> <asp:Label ID="lblDescripcion" runat="server" /></p>

               <asp:Button ID="btnFavorito" runat="server"
                   Text="Agregar a Favoritos"
                     CssClass="btn btn-warning mt-3"
                     OnClick="btnFavorito_Click" />

                 <asp:Label ID="lblMsg" runat="server"
                     CssClass="text-success mt-2 d-block"></asp:Label>

                <a href="Home.aspx" class="btn btn-secondary mt-3">Volver</a>
            </div>

        </div>

    </asp:Panel>

</asp:Content>
