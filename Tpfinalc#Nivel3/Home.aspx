<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Tpfinalc_Nivel3.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mb-4">Catálogo de productos</h2>

    <!-- FILTROS -->
    <div class="row mb-4">
        <div class="col-md-4">
            <asp:TextBox ID="txtBuscar" runat="server"
                CssClass="form-control"
                Placeholder="Buscar producto..." />
        </div>

        <div class="col-md-3">
            <asp:DropDownList ID="ddlMarca" runat="server"
                CssClass="form-select" />
        </div>

        <div class="col-md-3">
            <asp:DropDownList ID="ddlCategoria" runat="server"
                CssClass="form-select" />
        </div>

        <div class="col-md-2">
            <asp:Button ID="btnFiltrar" runat="server"
                CssClass="btn btn-primary w-100"
                Text="Filtrar"
                OnClick="btnFiltrar_Click" />
        </div>
    </div>

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

                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
