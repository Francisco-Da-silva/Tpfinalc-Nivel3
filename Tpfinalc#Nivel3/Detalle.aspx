<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="Tpfinalc_Nivel3.Detalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="pnlDetalle" runat="server" Visible="false">

        <div class="row g-4 align-items-start">

            <div class="col-md-5">
                <img id="imgProducto" runat="server"
                     class="detail-image"
                     src="img/no-image.png"
                     onerror="this.src='img/no-image.png'" />
            </div>

            <div class="col-md-7">
                <div class="page-title">
                    <h1><asp:Label ID="lblNombre" runat="server" /></h1>
                    <p class="product-price">$ <asp:Label ID="lblPrecio" runat="server" /></p>
                </div>

                <div class="detail-meta">
                    <div class="detail-meta-item">
                        <span>Codigo</span>
                        <asp:Label ID="lblCodigo" runat="server" />
                    </div>
                    <div class="detail-meta-item">
                        <span>Marca</span>
                        <asp:Label ID="lblMarca" runat="server" />
                    </div>
                    <div class="detail-meta-item">
                        <span>Categoria</span>
                        <asp:Label ID="lblCategoria" runat="server" />
                    </div>
                </div>

                <p class="mt-3">
                    <asp:Label ID="lblDescripcion" runat="server" />
                </p>

                <div class="d-flex gap-2 mt-3 flex-wrap">
                    <asp:Button ID="btnAgregarFavorito" runat="server"
                        Text="Agregar a favoritos"
                        CssClass="btn btn-primary"
                        OnClick="btnAgregarFavorito_Click" />

                    <a href="Home.aspx" class="btn btn-secondary">Volver al catalogo</a>
                </div>

                <asp:Label ID="lblFavoritoMsg" runat="server" CssClass="d-block mt-3"></asp:Label>
            </div>

        </div>

    </asp:Panel>

</asp:Content>
