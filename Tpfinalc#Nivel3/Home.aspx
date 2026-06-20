<%@ Page Title="Catalogo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Tpfinalc_Nivel3.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="catalog-page"

        <aside class="catalog-sidebar">
            <h3>Filtros</h3>

            <div class="mb-3">
                <label class="form-label">Busqueda</label>
                <asp:TextBox ID="txtBuscar" runat="server"
                    CssClass="form-control"
                    Placeholder="Buscar producto..." />
            </div>

            <div class="mb-3">
                <label class="form-label">Marca</label>
                <asp:DropDownList ID="ddlMarca" runat="server"
                    CssClass="form-select" />
            </div>

            <div class="mb-3">
                <label class="form-label">Categoría</label>
                <asp:DropDownList ID="ddlCategoria" runat="server"
                    CssClass="form-select" />
            </div>

            <asp:Button ID="btnFiltrar" runat="server"
                CssClass="btn btn-primary w-100"
                Text="Aplicar filtros"
                OnClick="btnFiltrar_Click" />
        </aside>

        <main class="catalog-main">

            <div class="page-title">
                <h1>Catalogo de productos</h1>
                <p>Busca, filtra y explora los articulos disponibles.</p>
            </div>

            <asp:Panel ID="pnlSinResultados" runat="server" Visible="false"
                CssClass="alert alert-warning">
                <asp:Label ID="lblSinResultados" runat="server"></asp:Label>
            </asp:Panel>

            <div class="row product-grid">
                <asp:Repeater ID="repArticulos" runat="server">
                    <ItemTemplate>
                        <div class="col-sm-6 col-md-4 col-xl-3 mb-4">
                            <div class="card product-card h-100">

                                <img src='<%# GetImagenProducto(Eval("ImagenUrl")) %>'
                                     class="card-img-top product-image"
                                     onerror="this.onerror=null;this.src='img/no-image.png';" />

                                <div class="card-body">
                                    <h5 class="product-name"><%# Eval("Nombre") %></h5>
                                    <p class="product-price">$ <%# Eval("Precio") %></p>
                                </div>

                                <a href='Detalle.aspx?id=<%# Eval("Id") %>' class="stretched-link"></a>

                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

        </main>

    </div>



</asp:Content>