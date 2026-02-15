<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Tpfinalc_Nivel3.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mb-4">Catálogo de productos</h2>

    
<%--         BÚSQUEDA + FILTROS --%>
    
    <div class="card mb-4 shadow-sm">
        <div class="card-body">

            <!-- BÚSQUEDA -->
            <div class="mb-3">
                <div class="d-flex justify-content-between align-items-center mb-2">
                    <h6 class="m-0 text-uppercase text-muted">Búsqueda</h6>
                </div>

                <div class="row g-2 align-items-end">
                    <div class="col-md-10">
                        <asp:TextBox ID="txtBuscar" runat="server"
                            CssClass="form-control"
                            Placeholder="Buscar por nombre del producto..." />
                    </div>

                    <div class="col-md-2">
                        <asp:Button ID="btnFiltrar" runat="server"
                            CssClass="btn btn-primary w-100"
                            Text="Buscar"
                            OnClick="btnFiltrar_Click" />
                    </div>
                </div>
            </div>

            <hr class="my-3" />

            <!-- FILTROS -->
            <div>
                <h6 class="mb-2 text-uppercase text-muted">Filtros</h6>

                <div class="row g-2">
                    <div class="col-md-6">
                        <label class="form-label">Marca</label>
                        <asp:DropDownList ID="ddlMarca" runat="server"
                            CssClass="form-select" />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label">Categoría</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server"
                            CssClass="form-select" />
                    </div>
                </div>

                <small class="text-muted d-block mt-2">
                    * La búsqueda se combina con los filtros seleccionados.
                </small>
            </div>

        </div>
    </div>

    <!-- MENSAJE SIN RESULTADOS -->
    <asp:Panel ID="pnlSinResultados" runat="server" Visible="false"
        CssClass="alert alert-warning">
        <asp:Label ID="lblSinResultados" runat="server"></asp:Label>
    </asp:Panel>

    <!-- CATÁLOGO -->
    <div class="row">
        <asp:Repeater ID="repArticulos" runat="server">
            <ItemTemplate>
                <div class="col-md-3 mb-4">
                    <div class="card h-100">

                        <img src='<%# Eval("ImagenUrl") %>'
                             class="card-img-top"
                             style="height:200px; object-fit:cover;" />

                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <p class="card-text fw-bold">$ <%# Eval("Precio") %></p>
                        </div>
                        <a href='Detalle.aspx?id=<%# Eval("Id") %>' class="stretched-link"></a>


                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
